using NUnit.Framework;
// ReSharper disable IsExpressionAlwaysTrue

namespace func.brainfuck
{
	[TestFixture]
	public class VirtualMachineTests
	{
		[Test]
		public void ImplementIVirtualMachine()
		{
			Assert.IsTrue(new VirtualMachine("", 1) is IVirtualMachine);
		}
		[Test]
		public void Initialize()
		{
			var vm = new VirtualMachine("xxx", 12345);
			vm.RegisterCommand('x', b => { });
			Assert.AreEqual(12345, vm.Memory.Length);
			Assert.AreEqual(0, vm.MemoryPointer);
			Assert.AreEqual("xxx", vm.Instructions);
			Assert.AreEqual(0, vm.InstructionPointer);
		}

		[Test]
		public void SetMemorySize()
		{
			var vm = new VirtualMachine("", 42);
			Assert.AreEqual(42, vm.Memory.Length);
		}

		[Test]
		public void IncrementInstructionPointer()
		{
			var res = "";
			var vm = new VirtualMachine("xy", 10);
			vm.RegisterCommand('x', b => { res += "x->" + b.InstructionPointer + ", "; });
			vm.RegisterCommand('y', b => { res += "y->" + b.InstructionPointer; });
			vm.Run();
			Assert.AreEqual("x->0, y->1", res);
		}

		[Test]
		public void ChangeInstructionPointer()
		{
			var res = "";
			var vm = new VirtualMachine("xyz", 10);
			vm.RegisterCommand('x', b => { b.InstructionPointer++; });
			vm.RegisterCommand('y', b => { Assert.Fail(); });
			vm.RegisterCommand('z', b => { res += "z"; });
			vm.Run();
			Assert.AreEqual("z", res);
		}

		[Test]
		public void ChangeMemoryPointer()
		{
			var vm = new VirtualMachine("xy", 10);
			vm.RegisterCommand('x', b => { b.MemoryPointer = b.MemoryPointer + 42; });
			vm.RegisterCommand('y', b => { Assert.AreEqual(42, b.MemoryPointer); });
			vm.Run();
		}

		[Test]
		public void SkipUnknownCommands()
		{
			new VirtualMachine("xyz", 10).Run();
		}

		[Test]
		public void ReadWriteMemory()
		{
			var vm = new VirtualMachine("wr", 10);
			vm.RegisterCommand('w', b => { b.Memory[3] = 'z'; });
			vm.RegisterCommand('r', b => { Assert.AreEqual('z', b.Memory[3]); });
			vm.Run();
		}
	}
}