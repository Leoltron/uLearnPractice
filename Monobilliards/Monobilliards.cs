using System;
using System.Collections.Generic;

namespace Monobilliards
{
    public class Monobilliards : IMonobilliards
    {
        public bool IsCheater(IList<int> inspectedBalls)
        {
            var inspectedBallsSegments = new LinkedStack<Tuple<int,int>>(); //Понимаю, что можно обойтись и массивом bool
            foreach (var ballNumber in inspectedBalls)
            {
                if (inspectedBallsSegments.IsEmpty() || 
                    inspectedBallsSegments.Peek().Item2 < ballNumber)
                    inspectedBallsSegments.Push(new Tuple<int, int>(ballNumber,ballNumber));
                else
                {
                    var lastValue = inspectedBallsSegments.Peek();
                    if (lastValue.Item1 - 1 != ballNumber)
                        return true;
                    
                    inspectedBallsSegments.Pop();
                    inspectedBallsSegments.Push(new Tuple<int, int>(lastValue.Item1 - 1, lastValue.Item2));
                }
                while (!inspectedBallsSegments.IsEmpty() &&
                    inspectedBallsSegments.Head.Previous != null && 
                    Math.Abs(inspectedBallsSegments.Head.Value.Item1 - 
                    inspectedBallsSegments.Head.Previous.Value.Item2) <= 1)
                    ConnectLastTwoElements(inspectedBallsSegments);
            }
            return false;
        }

        private static void ConnectLastTwoElements<T>(LinkedStack<Tuple<T, T>> tupleStack)
        {
            var last = tupleStack.Pop();
            var prevLast = tupleStack.Pop();
            var connectedTuples = ConcatTuples(prevLast,last);
            tupleStack.Push(connectedTuples);
        }

        private static Tuple<T, T> ConcatTuples<T>(Tuple<T, T> t1, Tuple<T, T> t2)
        {
            return new Tuple<T, T>(t1.Item1,t2.Item2);
        }
    }

    public class LinkedStack<T>
    {
        public LinkedStackItem<T> Head;

        public LinkedStack() { }
        public LinkedStack(LinkedStack<T> stack)
        {
            Head = stack.Head;
        }

        public void Push(T value)
        {
            Head = new LinkedStackItem<T>(value, Head);
        }

        public T Pop()
        {
            var value = Head.Value;
            Head = Head.Previous;
            return value;
        }

        public T Peek()
        {
            return Head.Value;
        }

        public bool IsEmpty()
        {
            return Head == null;
        }
    }

    public class LinkedStackItem<T>
    {
        public readonly T Value;
        public readonly LinkedStackItem<T> Previous;

        public LinkedStackItem(T value, LinkedStackItem<T> previous = null)
        {
            Value = value;
            Previous = previous;
        }
    }
}