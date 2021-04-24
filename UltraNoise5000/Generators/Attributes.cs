using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NoiseUltra.Generators
{
    [Serializable]
    public class Attributes
    {
        private const int MaxOctaves = 8; 
        private const float MaxFrequency = 0.2f;
        private const float MaxLacunarity = 2f;
        private const float MaxAmplitude = 2f;

        public int seed;
        public NOISE_TYPE noiseType = NOISE_TYPE.PERLIN;
        [SerializeField] private float frequency = 20f;
        [Range(0, 2)] public float amplitude = 1f;
        [Range(0, MaxLacunarity)] public float lacunarity = 2.0f;
        [Range(0, MaxOctaves)] public int octaves = 4;
        public Vector3 offset;

        public void RandomizeSeed() => seed = Random.Range(0, 999999);
        public float FrequencyOver100 => frequency / 100f;
    }
}