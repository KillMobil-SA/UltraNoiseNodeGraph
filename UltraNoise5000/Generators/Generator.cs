using NoiseUltra.Nodes;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace NoiseUltra.Generators
{
	public class Generator : NodeOutput
	{
		[SerializeField] 
		private Attributes attributes = new Attributes();

		private FractalNoise _fractal;

		[Button]
		private void New()
		{
			attributes.RandomizeSeed();
			Update();
		}

		protected override void OnBeforeUpdate()
		{
			var seed = attributes.seed;
			var noiseType = attributes.noiseType;
			var frequency = attributes.FrequencyOver100;
			var octaves = attributes.octaves;
			var offset = attributes.offset;
			var noise = NoiseFactory.CreateBaseNoise(seed, frequency, noiseType);
			_fractal = NoiseFactory.CreateFractal(noise, octaves, attributes.FrequencyOver100); 
			_fractal.Offset = offset;
			
			//Has to happen afterwards
			base.OnBeforeUpdate();
		}

		public override float Sample1D(float x) => _fractal.Sample1D(x);

		public override float Sample2D(float x, float y) => _fractal.Sample2D(x, y);

		public override float Sample3D(float x, float y, float z) => _fractal.Sample3D(x, y, z);
	}
}