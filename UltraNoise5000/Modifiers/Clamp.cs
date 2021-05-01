using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Modifiers
{
    public class Clamp : NodeModifier
    {
        [SerializeField] [OnValueChanged(nameof(Update))]
        private Vector2 limit = new Vector2(0, 1);

        [SerializeField] [OnValueChanged(nameof(Update))]
        private bool smoothClampValue;

        protected override float ApplyModifier(float sample)
        {
            var clamp = Mathf.Clamp(sample, limit.x, limit.y);
            if (smoothClampValue) clamp = Mathf.InverseLerp(limit.x, limit.y, clamp);

            return clamp;
        }
    }
}