using NoiseUltra.Nodes;
using UnityEngine;
using Sirenix.OdinInspector;

namespace NoiseUltra.Math
{
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
            var f = sample * 2f * Mathf.PI;
            var resultValue =  0.5f * (1f - Mathf.Cos(f)); 
            return resultValue;
        }
    }
}