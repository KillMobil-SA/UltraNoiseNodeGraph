using NoiseUltra.Nodes;
using NoiseUltra.Operations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Modifiers
{
    [NodeTint(NodeColor.MODIFIER)]
    public class Sin : BaseNodeModifier
    {
        [SerializeField , OnValueChanged(nameof(DrawAsync))]
        private float strenght;

        protected override float ApplyModifier(float sample)
        {
            return SinCalculation(sample * strenght);
        }
        

        public float SinCalculation(float sample)
        {
            float f = sample * MathUtils.PI_TIMES_2;
            float resultValue = 0.5f *(1 + Mathf.Sin(f));
            return resultValue;
        }
    }
}