using ProceduralNoiseProject;

namespace NoiseUltra.Generators
{
    public static class NoiseFactory
    {
        public static INoise CreateBaseNoise(int seed, float frequency, NOISE_TYPE type)
        {
            switch (type)
            {
                case NOISE_TYPE.PERLIN:
                    return new PerlinNoise(seed, frequency);
                case NOISE_TYPE.VALUE:
                    return new ValueNoise(seed, frequency);
                case NOISE_TYPE.SIMPLEX:
                    return new SimplexNoise(seed, frequency);
                case NOISE_TYPE.VORONOI:
                    return new VoronoiNoise(seed, frequency);
                case NOISE_TYPE.WORLEY:
                    return new WorleyNoise(seed, frequency, 1.0f);
                default:
                    return new PerlinNoise(seed, frequency);
            }
        }

        public static FractalNoise CreateFractal(INoise noise, int octaves, float frequency)
        {
            return new FractalNoise(noise, octaves, frequency ); 
        }        
    }
}