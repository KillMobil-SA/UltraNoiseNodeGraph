using NoiseUltra.Nodes;

namespace NoiseUltra.Operations
{
    [NodeTint(NodeColor.OPERATION)]
    public class Div : NodeDoubleInputOutput
    {
        protected override float ExecuteOperation(float strengthenedA, float strengthenedB)
        {
            return strengthenedA / MathUtils.NaNCheck(strengthenedB);
        }
    }
}