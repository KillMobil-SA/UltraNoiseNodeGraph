using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NoiseUltra.Generators
{
    [Serializable]
    public sealed class Attributes
    {
        #region Member
        [SerializeField]
        [OnValueChanged(nameof(DrawPreview))]
        private int seed;

        [SerializeField]
        [OnValueChanged(nameof(DrawPreview))]
        private NOISE_TYPE noiseType = NOISE_TYPE.PERLIN;

        [SerializeField]
        [OnValueChanged(nameof(DrawPreview))]
        private float frequency = 20f;

        [SerializeField]
        [OnValueChanged(nameof(DrawPreview))]
        private float amplitude = 1f;

        [SerializeField]
        [OnValueChanged(nameof(DrawPreview))]
        private float lacunarity = 2.0f;

        [SerializeField]
        [OnValueChanged(nameof(DrawPreview))]
        [Range(1, 16)]
        private int octaves = 4;

        [SerializeField]
        [OnValueChanged(nameof(DrawPreview))]
        private Vector3 offset;
        #endregion

        #region Public
        public float FrequencyOver100 => frequency / 100f;

        private NoiseGenerator NoiseGenerator { get; set; }

        public int Seed => seed;
        public NOISE_TYPE NoiseType => noiseType;

        public float Amplitude => amplitude;

        public float Lacunarity => lacunarity;

        public int Octaves => octaves;

        public Vector3 Offset => offset;

        public void RandomizeSeed()
        {
            seed = Random.Range(0, 999999);
        }

        public void SetGenerator(NoiseGenerator noiseGenerator)
        {
            this.NoiseGenerator = noiseGenerator;
        }
        #endregion

        #region Private
        private void DrawPreview()
        {
            if (NoiseGenerator != null)
            {
                NoiseGenerator.SetFractalDirty();
                NoiseGenerator.DrawAsync();
            }
        }
        #endregion
    }
}