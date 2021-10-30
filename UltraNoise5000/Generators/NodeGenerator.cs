using System;
using NoiseUltra.Nodes;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Generators
{
    [NodeTint(NodeColor.Green)]
    public class NodeGenerator : NodeOutput
    {
        #region Members
        [SerializeField]
        private Attributes attributes = new Attributes();
        private FractalNoise _fractal;
        
        private bool IsDirty => _fractal == null;
        #endregion

        #region Public
        public void SetFractalDirty()
        {
            _fractal = null;
        }

        public override float GetSample(float x)
        {
            var sample = GetFractal().Sample1D(x);
            return sample;
        }

        public override float GetSample(float x, float y)
        {
            var sample = GetFractal().Sample2D(x, y);
            return sample;
        }
        
        public override float GetSample(float x, float y, float z)
        {
            return GetFractal().Sample3D(x, y, z);
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

        private FractalNoise GetFractal()
        {
            return IsDirty ? CreateFractal() : _fractal;
        }

        private FractalNoise CreateFractal()
        {
            attributes.SetGenerator(this);
            return NodeGeneratorHelper.CreateFractal(attributes);
        }

        #endregion
    }
}