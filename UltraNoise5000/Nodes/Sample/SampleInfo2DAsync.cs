using System;
using UnityEngine;

namespace NoiseUltra.Nodes
{
    public abstract class SampleInfo2DAsync<T>
    {
        private int xMax;
        private int yMax;
        private int pxIndex;
        private int pyIndex;
        private Action onComplete;
        private float x;
        private float y;
        private T[,] values;

        private void Evaluate()
        {
            bool isLastRow = xMax - 1 == pxIndex;
            bool isLastColumn = yMax - 1 == pyIndex;
            bool isLast = isLastColumn && isLastRow;
            if (isLast)
            {
                onComplete?.Invoke();
            }
        }

        public void Execute(Func<float, float, float> sampleFunction)
        {
            var sample = sampleFunction(x, y);
            values[pxIndex, pyIndex] = Create(sample);
            Evaluate();
        }

        protected abstract T Create(float sample);

        protected SampleInfo2DAsync(float x, float y, int xMax, int yMax, int pxIndex, int pyIndex, ref T[,] values, Action onComplete)
        {
            this.xMax = xMax;
            this.yMax = yMax;
            this.x = x;
            this.y = y;
            this.pxIndex = pxIndex;
            this.pyIndex = pyIndex;
            this.values = values;
            this.onComplete = onComplete;
        }

        public override string ToString()
        {
            return $"x: {x}|" +
                   $"y: {y}|" +
                   $"xMax: {xMax}|" +
                   $"yMax: {yMax}|" +
                   $"pxIndex: {pxIndex}|" +
                   $"pyIndex: {pyIndex}|" +
                   $"values: {values}|" +
                   $"onComplete: {onComplete.Method.Name}";
        }
    }
}