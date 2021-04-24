using NoiseUltra.Nodes;

namespace NoiseUltra.Operations
{
	public class Reverse : NodeModifier
	{
		protected override float ApplyModifier(float sample)
		{
			return 1 - sample;
		}
	}
}