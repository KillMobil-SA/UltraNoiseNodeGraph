using NoiseUltra.Nodes;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Generators
{
    [NodeTint(NodeProprieties.NodeTintGreen)]
    public class Generator : NodeOutput
    {
        [SerializeField] private Attributes attributes = new Attributes();

        private FractalNoise _fractal;

        private FractalNoise fractal
        {
            get
            {
                if (_fractal == null)
                    CreateFractal();
                return _fractal;
            }
        }

        [Button]
        private void New()
        {
            attributes.RandomizeSeed();
            CreateFractal();
        }

        private void CreateFractal()
        {
            attributes.SetGenerator(this);
            var seed = attributes.Seed;
            var noiseType = attributes.NoiseType;
            var frequency = attributes.FrequencyOver100;
            var octaves = attributes.Octaves;
            var amplitude = attributes.Amplitude;
            var offset = attributes.Offset;
            var noise = NoiseFactory.CreateBaseNoise(seed, frequency, noiseType, amplitude);
            _fractal = NoiseFactory.CreateFractal(noise, octaves, attributes.FrequencyOver100);
            _fractal.Offset = offset;

            Update();
        }

        public override float Sample1D(float x)
        {
            return fractal.Sample1D(x);
        }

        public override float Sample2D(float x, float y)
        {
            return fractal.Sample2D(x, y);
        }

        public override float Sample3D(float x, float y, float z)
        {
            return fractal.Sample3D(x, y, z);
        }
    }
}