using System;
using UnityEngine;

namespace NoiseUltra.Generators
{
    public static class Profiler
    {
        private static bool IsEnabled { get; } = false;
        private const string CLOSE_LOG_COLOR = "</color>";
        private const string OPEN_LOG_COLOR = "<color=#{0}>";
        private static readonly string COLOR = ColorUtility.ToHtmlStringRGB(new Color(0, 0.8f, 0));
        public static DateTime begin;
        public static void Start()
        {
            if (IsEnabled)
            {
                begin = DateTime.Now;
            }
        }

        public static void End(string context = "")
        {
            if(IsEnabled)
            {
                Debug.Log(string.Format(OPEN_LOG_COLOR, COLOR)
                       + $"Elapsed Time: {(DateTime.Now.Subtract(begin).TotalSeconds)} Seconds {context}"
                       + CLOSE_LOG_COLOR);
            }
        }
    }
}