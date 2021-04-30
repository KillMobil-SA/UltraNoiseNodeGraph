using System;

namespace ProceduralNoiseProject
{
    internal class PermutationTable
    {
        private int[] Table;

        private readonly int Wrap;

        internal PermutationTable(int size, int max, int seed)
        {
            Size = size;
            Wrap = Size - 1;
            Max = Math.Max(1, max);
            Inverse = 1.0f / Max;
            Build(seed);
        }

        public int Size { get; }

        public int Seed { get; private set; }

        public int Max { get; }

        public float Inverse { get; }

        internal int this[int i] => Table[i & Wrap] & Max;

        internal int this[int i, int j] => Table[(j + Table[i & Wrap]) & Wrap] & Max;

        internal int this[int i, int j, int k] => Table[(k + Table[(j + Table[i & Wrap]) & Wrap]) & Wrap] & Max;

        internal void Build(int seed)
        {
            if (Seed == seed && Table != null) return;

            Seed = seed;
            Table = new int[Size];

            var rnd = new Random(Seed);

            for (var i = 0; i < Size; i++) Table[i] = rnd.Next();
        }
    }
}