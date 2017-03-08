using System.Collections.Generic;

namespace func.brainfuck
{

    public class BrainfuckLoopCommands
    {
        private static Dictionary<int, int> loopStartEndDict;
        private static Dictionary<int, int> loopEndStartDict;

        private static void RegisterLoops(IVirtualMachine vm)
        {
            loopStartEndDict = new Dictionary<int, int>();
            loopEndStartDict = new Dictionary<int, int>();

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

        public static void RegisterTo(IVirtualMachine vm)
        {
            RegisterLoops(vm);
            vm.RegisterCommand('[', b =>
            {
                if (b.Memory[b.MemoryPointer] == 0)
                    b.InstructionPointer = loopStartEndDict[b.InstructionPointer];
            });
            vm.RegisterCommand(']', b =>
            {
                if (b.Memory[b.MemoryPointer] != 0)
                    b.InstructionPointer = loopEndStartDict[b.InstructionPointer];
            });
        }

    }

}