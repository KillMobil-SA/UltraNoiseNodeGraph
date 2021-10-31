using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Operations
{
    public class Lerp : NodeDoubleInputOutput
    {
        [SerializeField]
        [Range(NodeProprieties.MIN_VALUE, NodeProprieties.MAX_VALUE)]
        [OnValueChanged(nameof(DrawAsync))]
        private float factor;

        protected override float ExecuteOperation(float strengthenedA, float strengthenedB)
        {
            return Mathf.Lerp(strengthenedA, strengthenedB, factor);
        }
    }
}