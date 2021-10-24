using System;
using UnityEngine;

namespace NoiseUltra.Generators
{
    public static class Profiler
    {
        public static DateTime Begin;
        public static void Start()
        {
            Begin = DateTime.Now;
            Debug.Log($"Start");
        }
        public static void End(string context)
        {
            Debug.Log($"Elapsed Time: {(DateTime.Now.Subtract(Begin).TotalSeconds)} Seconds {context}");
        }
    }
}