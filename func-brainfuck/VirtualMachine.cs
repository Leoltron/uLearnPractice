using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	
	public class VirtualMachine : IVirtualMachine
	{
        public string Instructions { get; }
        public int InstructionPointer { get; set; }

        public char[] Memory { get; }
        public int MemoryPointer { get; set; }

	    private readonly Dictionary<char, Action<IVirtualMachine>> commands;

        public VirtualMachine(string program, int memorySize)
		{
		    Memory = new char[memorySize];
		    Instructions = program;
            commands = new Dictionary<char, Action<IVirtualMachine>>();

        }

	    public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
	    {
	        commands.Add(symbol,execute);
	    }

	    public void Run()
	    {
	        while (InstructionPointer < Instructions.Length)
	        {
	            ExecuteCurrentInstrucion();
	            InstructionPointer++;
	        }
	    }

	    private void ExecuteCurrentInstrucion()
	    {
	        var command = Instructions[InstructionPointer];
	        if (commands.ContainsKey(command))
	            commands[command](this);
	    }
	}
	
}