using System;

namespace func.brainfuck
{
	
	public class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			vm.RegisterCommand('<', b => { b.MemoryPointer = (b.MemoryPointer - 1 + b.Memory.Length) % b.Memory.Length; });
			vm.RegisterCommand('>', b => { b.MemoryPointer = (b.MemoryPointer + 1) % b.Memory.Length; });
			vm.RegisterCommand('+', b => { b.Memory[b.MemoryPointer] = (char) ((b.Memory[b.MemoryPointer] + 1) % 256); });
			vm.RegisterCommand('-', b => { b.Memory[b.MemoryPointer] = (char) ((b.Memory[b.MemoryPointer] - 1 + 256) % 256); });
			vm.RegisterCommand('.', b => { write(b.Memory[b.MemoryPointer]); });
			vm.RegisterCommand(',', b => { b.Memory[b.MemoryPointer] = (char) read(); });

		    for (var i = '0'; i <= '9'; i++)
		        RegisterConstant(vm, i);
		    for (var i = 'a'; i <= 'z'; i++)
		        RegisterConstant(vm, i);
		    for (var i = 'A'; i <= 'Z'; i++)
		        RegisterConstant(vm, i);
		}

	    private static void RegisterConstant(IVirtualMachine vm, char c)
	    {
	        vm.RegisterCommand(c, b => b.Memory[b.MemoryPointer] = c);
	    }
	}
	
}