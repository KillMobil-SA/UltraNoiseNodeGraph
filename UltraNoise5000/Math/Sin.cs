using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Math
{
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
            var f = sample * 2 * Mathf.PI ;
            var resultValue = 0.5f *(1 + Mathf.Sin(f));
            return resultValue;
        }
    }
}