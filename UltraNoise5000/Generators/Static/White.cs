using NoiseUltra.Nodes;

namespace NoiseUltra.Generators.Static
{
	public class White : NodeOutput
	{
		public override float Sample1D(float x) => 1;
		public override float Sample2D(float x, float y) => 1;
		public override float Sample3D(float x, float y, float z) => 1;
	}
}