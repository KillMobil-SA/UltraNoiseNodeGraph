using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace NoiseUltra.Generators {
	public class NoiseNodeGenerator : NoiseNodeBase {

		//-------------Noise
		INoise noise;
		FractalNoise fractal;
		[Title ("Noise Generator Settings")]
		[OnValueChanged ("UpdateNoiseType")] //[EnumToggleButtons]
		public NOISE_TYPE noiseType = NOISE_TYPE.PERLIN;
		[OnValueChanged ("UpdateValues")]
		public int octaves = 4;
		[OnValueChanged ("UpdateValues")]
		public float frequency = 1.0f;
		[OnValueChanged ("UpdateValues")]
		public Vector3 offset;
		[OnValueChanged ("UpdateValues")]
		public float amplitude = 1.0f;
		[OnValueChanged ("UpdateValues")]
		public float lacunarity = 2.0f;

		public override void NoiseInit () {
			CreateNoise ();
		}

		public void CreateNoise () {
			noise = GetNoise (seed, noiseType);
			myFractalNoise = new FractalNoise (noise, octaves, frequency / 100f);
			myFractalNoise.Offset = offset;
		}

		[Button]
		public void UpdateNoiseType () {
			noise = null;
			myFractalNoise = null;
			seed = Random.Range(0 , 999999);
			CreateNoise ();
			UpdateValues ();
		}

		[Button]
		public override void UpdateValues () {

			ResetDebugValues ();
			myFractalNoise.Octaves = octaves;
			myFractalNoise.Frequency = frequency / 100f;
			myFractalNoise.Offset = offset;
			myFractalNoise.Amplitude = amplitude;
			myFractalNoise.Lacunarity = lacunarity;
			myFractalNoise.UpdateTable ();
			UpdatePreview ();
		}

		public override float Sample1D (float x) {
			float v = myFractalNoise.Sample1D (x);
			return DebugValues (v);
		}

		public override float Sample2D (float x, float y) {
			float v = myFractalNoise.Sample2D (x, y);
			return DebugValues (v);
		}

		public override float Sample3D (float x, float y, float z) {
			float v = myFractalNoise.Sample3D (x, y, z);
			return DebugValues (v);
		}

		private FractalNoise _myFractalNoise;
		private FractalNoise myFractalNoise {
			get {
				if (_myFractalNoise == null) {
					CreateNoise ();
				}
				return _myFractalNoise;
			}
			set => _myFractalNoise = value;
		}
		private INoise GetNoise (int seed, NOISE_TYPE noiseType) {
			switch (noiseType) {
				case NOISE_TYPE.PERLIN:
					return new PerlinNoise (seed, 20);

				case NOISE_TYPE.VALUE:
					return new ValueNoise (seed, 20);

				case NOISE_TYPE.SIMPLEX:
					return new SimplexNoise (seed, 20);

				case NOISE_TYPE.VORONOI:
					return new VoronoiNoise (seed, 20);

				case NOISE_TYPE.WORLEY:
					return new WorleyNoise (seed, 20, 1.0f);

				default:
					return new PerlinNoise (seed, 20);
			}
		}

		[Output] public NoiseNodeBase noiseOutPut;
		public override object GetValue (NodePort port) {
			return this;
		}

	}
}