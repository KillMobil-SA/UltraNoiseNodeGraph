using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Operations
{
    public class Lerp : NodeDoubleInputOutput
    {
        private const float MinSpeed = 0;
        private const float MaxSpeed = 1; //I think we can have values greater than 1 here, no?

        [SerializeField] [Range(MinSpeed, MaxSpeed)] [OnValueChanged(nameof(DrawAsync))]
        private float factor;

        protected override float ExecuteOperation(float strengthenedA, float strengthenedB)
        {
            return Mathf.Lerp(strengthenedA, strengthenedB, factor);
        }
    }
}