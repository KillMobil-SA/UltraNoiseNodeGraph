using System;
using Sirenix.OdinInspector;

namespace NoiseUltra.Tools.Terrains
{
    [Serializable]
    public class ProgressBar
    {
        [ReadOnly] [ProgressBar(0, nameof(_maxValue))]
        public int currentValue;

        private int _maxValue;

        public void SetSize(int max)
        {
            Reset();
            _maxValue = max;
        }

        public void Reset()
        {
            currentValue = 0;
        }

        public void Increment()
        {
            currentValue++;
        }

        public void Decrement()
        {
            currentValue--;
        }
    }
}