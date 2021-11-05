using NoiseUltra.Nodes;
using UnityEngine;
using Sirenix.OdinInspector;
namespace NoiseUltra.Math
{
    [NodeTint(NodeColor.MATH)]
    public class Tan : BaseNodeModifier
    {
        [SerializeField , OnValueChanged(nameof(DrawAsync))]
        private float strenght;
        protected override float ApplyModifier(float sample)
        {
            return Mathf.Tan(sample*strenght);
        }
    }
}