namespace Profiling
{
    public class ExperimentResult
    {
        public readonly  int Size;
        public readonly double ClassResult;
        public readonly double StructResult;

        public ExperimentResult(int size, double classResult, double structResult)
        {
            this.Size = size;
            this.ClassResult = classResult;
            this.StructResult = structResult;
        }
    }
}
