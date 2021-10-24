using System;
using UnityEngine;

namespace NoiseUltra.Nodes
{
    public abstract class SampleInfo2DAsync<T> : BaseSampleInfo
    {
        private int xMax;
        private int yMax;
        private int pxIndex;
        private int pyIndex;
        private float x;
        private float y;
        private T[,] values;
        
        public override void Execute(Func<float, float, float> sampleFunction)
        {
            var sample = sampleFunction(x, y);
            values[pxIndex, pyIndex] = Create(sample);
        }

        protected abstract T Create(float sample);

        protected SampleInfo2DAsync(float x, float y, int xMax, int yMax, int pxIndex, int pyIndex, ref T[,] values)
        {
            this.xMax = xMax;
            this.yMax = yMax;
            this.x = x;
            this.y = y;
            this.pxIndex = pxIndex;
            this.pyIndex = pyIndex;
            this.values = values;
        }

        public override string ToString()
        {
            return $"x: {x}|" +
                   $"y: {y}|" +
                   $"xMax: {xMax}|" +
                   $"yMax: {yMax}|" +
                   $"pxIndex: {pxIndex}|" +
                   $"pyIndex: {pyIndex}|" +
                   $"values: {values}|";
        }
    }
}