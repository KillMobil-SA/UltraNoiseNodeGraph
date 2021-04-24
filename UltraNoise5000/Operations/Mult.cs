using NoiseUltra.Nodes;

namespace NoiseUltra.Operations
{
    public class Mult : NodeDoubleInputOutput
    {
        protected override float ExecuteOperation(float strengthenedA, float strengthenedB)
        {
            return strengthenedA * strengthenedB;
        }
    }
}