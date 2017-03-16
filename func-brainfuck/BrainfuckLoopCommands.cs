using System.Collections.Generic;

namespace func.brainfuck
{

    public class BrainfuckLoopCommands
    {
        public static void RegisterTo(IVirtualMachine vm)
        {
            var loops = new LoopBorders(vm);

            vm.RegisterCommand('[', b =>
            {
                if (b.Memory[b.MemoryPointer] == 0)
                    b.InstructionPointer = loops.GetEnd(b.InstructionPointer);
            });
            vm.RegisterCommand(']', b =>
            {
                if (b.Memory[b.MemoryPointer] != 0)
                    b.InstructionPointer = loops.GetStart(b.InstructionPointer);
            });
        }

    }

    class LoopBorders
    {
        private Dictionary<int, int> loopStartEndDict;
        private Dictionary<int, int> loopEndStartDict;

        public int GetEnd(int start)
        {
            return loopStartEndDict[start];
        }

        public int GetStart(int end)
        {
            return loopEndStartDict[end];
        }

        public LoopBorders(IVirtualMachine vm)
        {
            loopStartEndDict = new Dictionary<int, int>();
            loopEndStartDict = new Dictionary<int, int>();

            RegisterLoops(vm);
        }

        private void RegisterLoops(IVirtualMachine vm)
        {
            var loopStartStack = new Stack<int>();
            for (var i = 0; i < vm.Instructions.Length; i++)
            {
                switch (vm.Instructions[i])
                {
                    case '[':
                        loopStartStack.Push(i);
                        break;
                    case ']':
                        var startIndex = loopStartStack.Pop();
                        loopStartEndDict.Add(startIndex, i);
                        loopEndStartDict.Add(i, startIndex);
                        break;
                    default:
                        continue;
                }
            }
        }
    }

}