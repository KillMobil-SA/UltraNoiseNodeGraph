using NoiseUltra.Nodes;

namespace NoiseUltra.Operations
{
	public class OneMinus : NodeModifier
	{
		protected override float ApplyModifier(float sample)
		{
			return 1 - sample;
		}
	}
}