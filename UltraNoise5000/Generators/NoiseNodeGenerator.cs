using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace NoiseUltra.Generators
{
	public class NoiseNodeGenerator : NoiseNodeBase
	{
		[SerializeField] 
		private NoiseProprieties proprieties = new NoiseProprieties();
		private FractalNoise _fractal;
		
		
		protected override void Init()
		{
			InitializeFractal();
		}

		private void InitializeFractal()
		{
			proprieties.RandomizeSeed();
			var seed = proprieties.seed;
			var noiseType = proprieties.noiseType;
			var frequency = proprieties.FrequencyOver100;
			var octaves = proprieties.octaves;
			var offset = proprieties.offset;
			var noise = NoiseFactory.CreateBaseNoise(seed, frequency, noiseType);
			
			_fractal = NoiseFactory.CreateFractal(noise, octaves, proprieties.FrequencyOver100); 
			_fractal.Offset = offset;
		}

		[Button]
		public void New()
		{
			InitializeFractal();
			Update();
		}
		
		[Button]
		public override void Update()
		{
			UpdateFractal();
			UpdatePreview();
		}  

		private void UpdateFractal()
		{
			_fractal.Octaves = proprieties.octaves;
			_fractal.Frequency = proprieties.FrequencyOver100;
			_fractal.Offset = proprieties.offset;
			_fractal.Amplitude = proprieties.amplitude;
			_fractal.Lacunarity = proprieties.lacunarity;
			_fractal.UpdateTable();
		}

		public override float Sample1D(float x)
		{
			float v = _fractal.Sample1D(x);
			return v;
		}

		public override float Sample2D(float x, float y)
		{
			float v = _fractal.Sample2D(x, y);
			return v;
		}

		public override float Sample3D(float x, float y, float z)
		{
			float v = _fractal.Sample3D(x, y, z);
			return v;
		}

		public override object GetValue(NodePort port)
		{
			return this;
		}
	}
}