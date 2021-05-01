﻿using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Modifiers
{
    public class Curve : NodeModifier
    {
        [SerializeField, OnValueChanged(nameof(Update))]
        private AnimationCurve resultCurve = AnimationCurve.Linear(0, 0, 1, 1);

        protected override float ApplyModifier(float sample)
        {
            return resultCurve.Evaluate(sample);
        }
    }
}