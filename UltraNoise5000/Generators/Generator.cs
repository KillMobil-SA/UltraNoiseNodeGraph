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
        private bool IsDirty => _fractal == null;

        [Button]
        private void New()
        {
            attributes.RandomizeSeed();
            SetFractalDirty();
            DrawPreview();
        }

        public void SetFractalDirty()
        {
            _fractal = null;
        }

        protected override void OnBeforeDrawPreview()
        {
            attributes.SetGenerator(this);
            base.OnBeforeDrawPreview();
        }

        private FractalNoise GetFractal()
        {
            if (!IsDirty) 
                return _fractal;

            var seed = attributes.Seed;
            var noiseType = attributes.NoiseType;
            var frequency = attributes.FrequencyOver100;
            var octaves = attributes.Octaves;
            var amplitude = attributes.Amplitude;
            var offset = attributes.Offset;
            var noise = NoiseFactory.CreateBaseNoise(seed, frequency, noiseType, amplitude);
            _fractal = NoiseFactory.CreateFractal(noise, octaves, attributes.FrequencyOver100);
            _fractal.Offset = offset;
            return _fractal;
        }

        public override float Sample1D(float x)
        {
            return GetFractal().Sample1D(x);
        }

        public override float Sample2D(float x, float y)
        {
            return GetFractal().Sample2D(x, y);
        }

        public override float Sample3D(float x, float y, float z)
        {
            return GetFractal().Sample3D(x, y, z);
        }
    }
}