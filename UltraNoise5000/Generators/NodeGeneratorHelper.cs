using ProceduralNoiseProject;
using UnityEngine;

namespace NoiseUltra.Generators
{
    public static class NodeGeneratorHelper
    {
        public static FractalNoise CreateFractal(Attributes attributes)
        {
            INoise noise = CreateBaseNoise(attributes);
            int octaves = attributes.Octaves;
            float frequency = attributes.FrequencyOver100;
            FractalNoise fractal = new FractalNoise(noise, octaves, frequency);
            Vector3 offset = attributes.Offset;
            fractal.Offset = offset;
            return fractal;
        }

        private static INoise CreateBaseNoise(Attributes attributes)
        {
            int seed = attributes.Seed;
            NOISE_TYPE noiseType = attributes.NoiseType;
            float frequency = attributes.FrequencyOver100;
            float amplitude = attributes.Amplitude;
            return CreateBaseNoise(seed, frequency, noiseType, amplitude);
        }

        private static INoise CreateBaseNoise(int seed, float frequency, NOISE_TYPE type, float amplitude)
        {
            INoise noise = type switch
            {
                NOISE_TYPE.PERLIN => new PerlinNoise(seed, frequency),
                NOISE_TYPE.VALUE => new ValueNoise(seed, frequency),
                NOISE_TYPE.SIMPLEX => new SimplexNoise(seed, frequency),
                NOISE_TYPE.VORONOI => new VoronoiNoise(seed, frequency),
                NOISE_TYPE.WORLEY => new WorleyNoise(seed, frequency, jitter: 1.0f),
                _ => new PerlinNoise(seed, frequency)
            };

            noise.Amplitude = amplitude;
            return noise;
        }
    }
}