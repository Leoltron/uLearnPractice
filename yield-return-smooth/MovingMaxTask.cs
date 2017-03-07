using System;
using System.Collections.Generic;
using System.Linq;

namespace yield
{

	public static class MovingMaxTask
	{
		public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
		{
            var prevQueue = new MaxHolderLimitedQueue(windowWidth);
		    foreach (var dataPoint in data)
		    {
		        prevQueue.Enqueue(dataPoint.OriginalY);
                dataPoint.MaxY = prevQueue.Max;
		        yield return dataPoint;
		    }
		}


	    public static void PushWithMax(this Stack<Element> stack, double value)
        {
            var max = stack.Count == 0 ? value : Math.Max(value, stack.Peek().Max);
            stack.Push(new Element(value, max));
        }

    }

    public class MaxHolderLimitedQueue
    {
        private readonly Stack<Element> enqueueStack;
        private readonly Stack<Element> dequeueStack;

        private readonly int maxCapacity;

        public MaxHolderLimitedQueue(int maxCapacity)
        {
            this.maxCapacity = maxCapacity;
            enqueueStack = new Stack<Element>();
            dequeueStack = new Stack<Element>();
        }

        public void Enqueue(double value)
        {
            enqueueStack.PushWithMax(value);
            if (enqueueStack.Count + dequeueStack.Count > maxCapacity)
                Dequeue();
        }

        private void Dequeue()
        {
            if (dequeueStack.Count == 0)
                while (enqueueStack.Count > 0)
                    dequeueStack.PushWithMax(enqueueStack.Pop().Value);
            dequeueStack.Pop();
        }

        public double Max
        {
            get
            {
                if (dequeueStack.Count == 0)
                    return enqueueStack.Peek().Max;
                if (enqueueStack.Count == 0)
                    return dequeueStack.Peek().Max;
                return Math.Max(enqueueStack.Peek().Max, dequeueStack.Peek().Max);
            }
        }
    }

    public class Element
    {
        public readonly double Value;
        public readonly double Max;

        public Element(double value, double max)
        {
            Value = value;
            Max = max;
        }
    }
    
}