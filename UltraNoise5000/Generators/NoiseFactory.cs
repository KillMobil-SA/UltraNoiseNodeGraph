using ProceduralNoiseProject;

namespace NoiseUltra.Generators
{
    public static class NoiseFactory
    {
        public static INoise CreateBaseNoise(int seed, float frequency, NOISE_TYPE type, float amplitude)
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

        public static FractalNoise CreateFractal(INoise noise, int octaves, float frequency)
        {
            return new FractalNoise(noise, octaves, frequency);
        }
    }
}