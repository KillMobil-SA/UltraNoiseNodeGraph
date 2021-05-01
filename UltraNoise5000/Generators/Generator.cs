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
        // private FractalNoise _fractal;

        private FractalNoise _fractal;
        private FractalNoise fractal
        {
            get
            {
                if (_fractal == null)
                  CreateFractal();
                return _fractal;
            }
            set { _fractal = value; }
        }

        [Button]
        private void New()
        {
            attributes.RandomizeSeed();
            CreateFractal();
          
        }

        void CreateFractal()
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
        
        protected override void OnBeforeUpdate()
        {
            //Has to happen afterwards
            base.OnBeforeUpdate();
        }

        public override float Sample1D(float x) => fractal.Sample1D(x);

        public override float Sample2D(float x, float y) => fractal.Sample2D(x, y);

        public override float Sample3D(float x, float y, float z) => fractal.Sample3D(x, y, z);
    }
}