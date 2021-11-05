using NoiseUltra.Nodes;

namespace NoiseUltra.Operations
{
    [NodeTint(NodeColor.OPERATION)]
    public class Add : NodeDoubleInputOutput
    {
        protected override float ExecuteOperation(float strengthenedA, float strengthenedB)
        {
            return strengthenedA + strengthenedB;
        }
    }
}