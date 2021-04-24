﻿using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NoiseUltra.Nodes;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace NoiseUltra.Generators.Static
{
    public class Line : NodeOutput
    {
        private enum GrandientLineType
        {
            Horizontal,
            Vertical
        }

        [SerializeField] private GrandientLineType lineType;
        [SerializeField] private float start;
        [SerializeField] private float end;
        
        public override float Sample1D(float x) => Mathf.Lerp(start, end, x);

        public override float Sample2D(float x, float y)
        {
            return Mathf.InverseLerp(start, end, lineType == GrandientLineType.Horizontal ? x : y);
        }

        public override float Sample3D(float x, float y, float z)
        {
            return Mathf.InverseLerp(start, end, lineType == GrandientLineType.Horizontal ? x : y);
        }
    }
}