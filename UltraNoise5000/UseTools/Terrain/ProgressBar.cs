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
        public int iterationsPerFrame = 1000;
        private int _maxValue;
        [SerializeField, ReadOnly] private int interationsCount;

        public void SetSize(int max)
        {
            Reset();
            _maxValue = max;
        }

        public void Reset()
        {
            currentValue = 0;
            interationsCount = 0;
        }

        public void Increment()
        {
            currentValue++;
            interationsCount++;
        }

        public IEnumerator ResetIteraction()
        {
            interationsCount = 0;
            yield return null;
        }

        public bool TryProcess()
        {
            Increment();
            return interationsCount < iterationsPerFrame;
        }

        public void Decrement()
        {
            currentValue--;
        }
    }
}