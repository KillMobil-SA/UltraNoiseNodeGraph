using System;
using Sirenix.OdinInspector;
using UnityEngine;
using NoiseUltra.Nodes;

namespace NoiseUltra.Nodes
{
    [Serializable]
    public struct Bound
    {
        //[Range(NodeProprieties.MIN_VALUE, NodeProprieties.MAX_VALUE), ReadOnly] 
        [HorizontalGroup(NodeLabels.MinMax)]
        [VerticalGroup(NodeLabels.MinMaxLEFTGroup)]
        [BoxGroup(NodeLabels.MinMaxLEFTGroupMIN) , HideLabel]
        public float min;

        //[Range(NodeProprieties.MIN_VALUE, NodeProprieties.MAX_VALUE), ReadOnly]
        [VerticalGroup(NodeLabels.MinMaxRIGHTGroup)]
        [BoxGroup(NodeLabels.MinMaxRIGHTGroupMax) ,  HideLabel]
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