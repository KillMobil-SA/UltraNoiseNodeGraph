using NoiseUltra.Nodes;
using NoiseUltra.Operations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Modifiers
{
    [NodeTint(NodeColor.MODIFIER)]
    public class Step : BaseNodeModifier
    {
        private const int MIN_STEPS = 1;
        private const int MAX_STEPS = 50;

        [SerializeField]
        [Range(MIN_STEPS, MAX_STEPS)]
        [OnValueChanged(nameof(DrawAsync))]
        private int steps = MIN_STEPS;

        protected override float ApplyModifier(float sample)
        {
            return Mathf.Floor(steps * sample) / MathUtils.NaNCheck(steps);
        }
    }
}