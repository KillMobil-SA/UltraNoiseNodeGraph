using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NoiseUltra.Generators
{
    [Serializable]
    public class NoiseProprieties
    {
        private const int MaxOctaves = 10; 
        private const float MaxFrequency = 20;

        public int seed;
        public NOISE_TYPE noiseType = NOISE_TYPE.PERLIN;
        [Range(0, MaxFrequency)] public float frequency = 1f;
        [Range(0, MaxFrequency)] public float amplitude = 1f;
        [Range(0, MaxFrequency)] public float lacunarity = 2.0f;
        [Range(0, MaxOctaves)] public int octaves = 4;
        public Vector3 offset;

        public void RandomizeSeed() => seed = Random.Range(0, 999999);
        public float FrequencyOver100 => frequency / 100f; //Not sure why
    }
}