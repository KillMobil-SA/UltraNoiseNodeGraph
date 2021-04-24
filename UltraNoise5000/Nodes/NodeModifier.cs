namespace NoiseUltra.Nodes
{
	public abstract class NodeModifier : NodeInputOutput
	{
		public override float Sample1D(float x)
		{
			var sample = base.Sample1D(x);
			return ApplyModifier(sample);
		}

		public override float Sample2D(float x, float y)
		{
			var sample = base.Sample2D(x, y);
			return ApplyModifier(sample);
		}

		public override float Sample3D(float x, float y, float z)
		{
			var sample = base.Sample3D(x, y, z);
			return ApplyModifier(sample);
		}
		
		protected abstract float ApplyModifier(float sample);
	}
}