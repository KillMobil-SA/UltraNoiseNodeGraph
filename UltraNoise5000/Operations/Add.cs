using NoiseUltra.Nodes;

namespace NoiseUltra.Operations
{
	public class Add : NodeDoubleInputOutput
	{
		protected override float ExecuteOperation(float strengthenedA, float strengthenedB)
		{
			return strengthenedA + strengthenedB;
		}
	}
}