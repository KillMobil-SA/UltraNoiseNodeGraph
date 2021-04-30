using NoiseUltra.Nodes;

namespace NoiseUltra.Operations
{
    public class Sub : NodeDoubleInputOutput
    {
        protected override float ExecuteOperation(float strengthenedA, float strengthenedB)
        {
            return strengthenedA - strengthenedB;
        }
    }
}