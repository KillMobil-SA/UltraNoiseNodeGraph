using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NoiseUltra.Generators
{
    [Serializable]
    public class Attributes
    {
        private const int MaxOctaves = 8; 
        private const float MaxLacunarity = 20f;
        private const float MaxAmplitude = 2f;
        
        [SerializeField, OnValueChanged(nameof(UpdateGenerator))] 
        private int seed;
        
        [SerializeField, OnValueChanged(nameof(UpdateGenerator))] 
        private NOISE_TYPE noiseType = NOISE_TYPE.PERLIN;
        
        [SerializeField, OnValueChanged(nameof(UpdateGenerator))] 
        private float frequency = 20f;
        
        [SerializeField, Range(0, MaxAmplitude), OnValueChanged(nameof(UpdateGenerator))] 
        private float amplitude = 1f;
        
        [SerializeField, Range(0, MaxLacunarity), OnValueChanged(nameof(UpdateGenerator))] 
        private float lacunarity = 2.0f;
        
        [SerializeField, Range(0, MaxOctaves), OnValueChanged(nameof(UpdateGenerator))] 
        private int octaves = 4;
        
        [SerializeField, OnValueChanged(nameof(UpdateGenerator))]
        private Vector3 offset;

        public void RandomizeSeed() => seed = Random.Range(0, 999999);
        public float FrequencyOver100 => frequency / 100f;
        
        private Generator Generator { get; set; }

        public int Seed => seed;
        public NOISE_TYPE NoiseType => noiseType;

        public float Amplitude => amplitude;

        public float Lacunarity => lacunarity;

        public int Octaves => octaves;

        public Vector3 Offset => offset;

        public void SetGenerator(Generator generator)
        {
            Generator = generator;
        }

        private void UpdateGenerator()
        {
            Debug.Log($"update generator {Generator}");
            if(Generator)
                Generator.Update();
        }
    }
}