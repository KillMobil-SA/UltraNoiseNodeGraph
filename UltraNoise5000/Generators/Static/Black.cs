using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NoiseUltra.Nodes;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace NoiseUltra.Generators.Static
{
    [NodeTint(NodeProprieties.NodeTintGreen)]
    public class Black : NodeOutput
    {
        public override float Sample1D(float x) => 0;
        public override float Sample2D(float x, float y) => 0;
        public override float Sample3D(float x, float y, float z) => 0;
    }
}