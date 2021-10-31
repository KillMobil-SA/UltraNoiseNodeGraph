using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Modifiers
{
    public class Clamp : BaseNodeModifier
    {
        [SerializeField] 
        [OnValueChanged(nameof(DrawAsync))]
        private Vector2 limit = new Vector2(0, 1);

        [SerializeField]
        [OnValueChanged(nameof(DrawAsync))]
        private bool smoothClampValue;

        protected override float ApplyModifier(float sample)
        {
            float clamp = Mathf.Clamp(sample, limit.x, limit.y);
            if (smoothClampValue)
            {
                clamp = Mathf.InverseLerp(limit.x, limit.y, clamp);
            }

            return clamp;
        }
    }
}