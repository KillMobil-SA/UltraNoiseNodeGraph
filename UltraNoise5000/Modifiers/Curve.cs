using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Modifiers
{
    [NodeTint(NodeColor.MODIFIER)]
    public class Curve : BaseNodeModifier
    {
        [SerializeField]
        [OnValueChanged(nameof(DrawAsync))]
        private AnimationCurve resultCurve = AnimationCurve.Linear(0, 0, 1, 1);

        protected override float ApplyModifier(float sample)
        {
            return resultCurve.Evaluate(sample);
        }
    }
}