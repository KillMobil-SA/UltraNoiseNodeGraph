using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Nodes
{
    [Serializable]
    public struct Bound
    {
        [Range(NodeProprieties.MIN_VALUE, NodeProprieties.MAX_VALUE), ReadOnly] 
        public float min;

        [Range(NodeProprieties.MIN_VALUE, NodeProprieties.MAX_VALUE), ReadOnly]
        public float max;

        private readonly PreviewImage m_Preview;

        public Bound(PreviewImage image)
        {
            min = NodeProprieties.MAX_VALUE;
            max = NodeProprieties.MIN_VALUE;
            m_Preview = image;
        }

        public void Reset()
        {
            min = NodeProprieties.MAX_VALUE;
            max = NodeProprieties.MIN_VALUE;
        }

        [Button]
        private void IdentifyBounds() => m_Preview.IdentifyBounds();

        public void IdentifyBounds(float sample)
        {
            sample = Mathf.Clamp01(sample);
            max = Mathf.Max(max, sample);
            min = Mathf.Min(min, sample);
        }
    }
}