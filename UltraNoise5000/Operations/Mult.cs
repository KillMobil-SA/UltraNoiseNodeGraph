using NoiseUltra.Nodes;

namespace NoiseUltra.Operations
{
    [NodeTint(NodeColor.OPERATION)]
    public class Mult : NodeDoubleInputOutput
    {
        protected override float ExecuteOperation(float strengthenedA, float strengthenedB)
        {
            return strengthenedA * strengthenedB;
        }
    }
}