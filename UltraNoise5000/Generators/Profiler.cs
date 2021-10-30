using System;
using UnityEngine;

namespace NoiseUltra.Generators
{
    public static class Profiler
    {
        public static DateTime begin;
        public static void Start()
        {
            begin = DateTime.Now;
        }

        public static void End(string context)
        {
            Debug.Log($"Elapsed Time: {(DateTime.Now.Subtract(begin).TotalSeconds)} Seconds {context}");
        }
    }
}