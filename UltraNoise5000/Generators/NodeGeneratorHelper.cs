using ProceduralNoiseProject;

namespace NoiseUltra.Generators
{
    public static class NodeGeneratorHelper
    {
        public static FractalNoise CreateFractal(Attributes attributes)
        {
            var noise = CreateBaseNoise(attributes);
            var octaves = attributes.Octaves;
            var frequency = attributes.FrequencyOver100;
            var fractal = new FractalNoise(noise, octaves, frequency);
            var offset = attributes.Offset;
            fractal.Offset = offset;
            return fractal;
        }

        private static INoise CreateBaseNoise(Attributes attributes)
        {
            var seed = attributes.Seed;
            var noiseType = attributes.NoiseType;
            var frequency = attributes.FrequencyOver100;
            var amplitude = attributes.Amplitude;
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
                NOISE_TYPE.WORLEY => new WorleyNoise(seed, frequency, 1.0f),
                _ => new PerlinNoise(seed, frequency)
            };

            noise.Amplitude = amplitude;
            return noise;
        }
    }
}