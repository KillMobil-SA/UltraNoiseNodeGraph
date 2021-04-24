using NoiseUltra.Nodes;

namespace NoiseUltra.Operations
{
    public class Div : NodeDoubleInputOutput
    {
        protected override float ExecuteOperation(float strengthenedA, float strengthenedB)
        {
            return strengthenedA / strengthenedB;
        }
    }
}