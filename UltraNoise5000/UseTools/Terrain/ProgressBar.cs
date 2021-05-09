using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Terrains
{
    [Serializable]
    public class ProgressBar
    {
        [ReadOnly] [ProgressBar(0, nameof(_maxValue))]
        public int currentValue;
        private int _iterationsPerFrame = 4096;
        private int _maxValue;
        private int _iterationsCount;

        public void SetSize(int max)
        {
            Reset();
            _maxValue = max;
        }

        public void Reset()
        {
            currentValue = 0;
            _iterationsCount = 0;
        }

        public void Increment()
        {
            currentValue++;
            _iterationsCount++;
        }

        public IEnumerator ResetIteraction()
        {
            _iterationsCount = 0;
            yield return null;
        }

        public bool TryProcess()
        {
            Increment();
            return _iterationsCount < _iterationsPerFrame;
        }

        public void Decrement()
        {
            currentValue--;
        }
    }
}