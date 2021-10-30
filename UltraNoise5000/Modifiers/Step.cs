﻿using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Modifiers
{
    public class Step : BaseNodeModifier
    {
        private const int MinSteps = 1;
        private const int MaxSteps = 50;

        [SerializeField]
        [Range(MinSteps, MaxSteps)]
        [OnValueChanged(nameof(DrawAsync))]
        private int steps = MinSteps;

        protected override float ApplyModifier(float sample)
        {
            return Mathf.Floor(steps * sample) / steps;
        }
    }
}