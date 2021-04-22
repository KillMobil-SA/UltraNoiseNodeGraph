﻿using System;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;
using Random = UnityEngine.Random;

namespace NoiseUltra.Generators
{
	[Serializable]
	public class NoiseProprieties
	{
		[SerializeField] public float frequency = 1f;
		public NOISE_TYPE noiseType = NOISE_TYPE.PERLIN;
		public int seed;
		public int octaves = 4;
		public float amplitude = 1f;
		public float lacunarity = 2.0f;
		public Vector3 offset;

		public void RandomizeSeed() => seed = Random.Range(0, 999999);
		public float FrequencyOver100 => frequency / 100f; //Not sure why
	}
	
	public class NoiseNodeGenerator : NoiseNodeBase
	{
		[SerializeField] 
		private NoiseProprieties proprieties = new NoiseProprieties();
		private FractalNoise _fractal;
		
		[Output] 
		public NoiseNodeBase noiseOutPut;
		
		protected override void Init()
		{
			InitializeFractal();
		}

		private void InitializeFractal()
		{
			proprieties.RandomizeSeed();
			var seed = proprieties.seed;
			var noiseType = proprieties.noiseType;
			var frequency = proprieties.frequency;
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
			ResetBounds();
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
			return IdentifyBounds(v);
		}

		public override float Sample2D(float x, float y)
		{
			float v = _fractal.Sample2D(x, y);
			return IdentifyBounds(v);
		}

		public override float Sample3D(float x, float y, float z)
		{
			float v = _fractal.Sample3D(x, y, z);
			return IdentifyBounds(v);
		}

		public override object GetValue(NodePort port)
		{
			return this;
		}
	}
}