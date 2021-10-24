using System;

namespace NoiseUltra.Nodes
{

    public abstract class SampleInfoAsync<T>
    {
        private int index;
        private Action onComplete;
        private float x;
        private float y;
        private T[] values;
        
        private void Evaluate()
        {
            bool isLast = index == values.Length - 1;
            if (isLast)
            {
                onComplete?.Invoke();
            }
        }

        public void Execute(Func<float, float, float> sampleFunction)
        {
            var sample = sampleFunction(x, y);
            values[index] = Create(sample);
            Evaluate();
        }

        protected abstract T Create(float sample);

        protected SampleInfoAsync(float x, float y, int index, ref T[] values, Action onComplete)
        {
            this.x = x;
            this.y = y;
            this.index = index;
            this.values = values;
            this.onComplete = onComplete;
        }

        public override string ToString()
        {
            return $"x: {x}|" +
                   $"y: {y}|" +
                   $"index: {index}|" +
                   $"values: {values}|" +
                   $"onComplete: {onComplete.Method.Name}";
        }
    }
}