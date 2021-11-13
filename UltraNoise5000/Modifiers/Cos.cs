using NoiseUltra.Nodes;
using NoiseUltra.Operations;
using UnityEngine;
using Sirenix.OdinInspector;

namespace NoiseUltra.Modifiers
{
    [NodeTint(NodeColor.MODIFIER)]
    public class Cos : BaseNodeModifier
    {
        [SerializeField , OnValueChanged(nameof(DrawAsync))]
        private float strenght;
        protected override float ApplyModifier(float sample)
        {
            return CosCalculation(sample * strenght);
        }

        private float CosCalculation(float sample)
        {
            float f = sample * MathUtils.PI_TIMES_2;
            float resultValue =  0.5f * (1f - Mathf.Cos(f)); 
            return resultValue;
        }

    }
}