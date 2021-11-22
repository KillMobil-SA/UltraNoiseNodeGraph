using NoiseUltra.Nodes;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Generators
{
    [NodeTint(NodeColor.GENERATOR_NOISE)]
    public class NoiseGenerator : NodeOutput
    {
        #region Members
        [BoxGroup("Noise Settings"),SerializeField , InlineProperty()  , HideLabel]
        private Attributes attributes = new Attributes();
        private FractalNoise m_Fractal;
        
        private bool IsDirty => m_Fractal == null;
        #endregion

        #region Public
        public void SetFractalDirty()
        {
            m_Fractal = null;
        }

        public override float GetSample(float x)
        {
            float sample = GetFractalLazy().Sample1D(x);
            return sample;
        }

        public override float GetSample(float x, float y)
        {
            float sample = GetFractalLazy().Sample2D(x, y);
            return sample;
        }
        
        public override float GetSample(float x, float y, float z)
        {
            float sample = GetFractalLazy().Sample3D(x, y, z);
            return sample;
        }
        #endregion

        #region Private
        [Button]
        private void New()
        {
            attributes.RandomizeSeed();
            SetFractalDirty();
            DrawAsync();
        }

        private FractalNoise GetFractalLazy()
        {
            return IsDirty ? CreateFractal() : m_Fractal;
        }

        private FractalNoise CreateFractal()
        {
            attributes.SetGenerator(this);
            return NodeGeneratorHelper.CreateFractal(attributes);
        }

        #endregion
    }
}