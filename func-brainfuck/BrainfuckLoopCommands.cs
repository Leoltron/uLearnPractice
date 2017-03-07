using System.Collections.Generic;

namespace func.brainfuck
{
	
	public class BrainfuckLoopCommands
	{
		public static void RegisterTo(IVirtualMachine vm)
		{
			vm.RegisterCommand('[', b =>
			{
			    if (b.Memory[b.MemoryPointer] == 0)
			        while (b.Instructions[b.InstructionPointer] != ']')
			            b.InstructionPointer++;
			});
			vm.RegisterCommand(']', b =>
			{
                if (b.Memory[b.MemoryPointer] != 0)
                    while (b.Instructions[b.InstructionPointer] != '[')
                        b.InstructionPointer--;
            });
		}
	}
	
}