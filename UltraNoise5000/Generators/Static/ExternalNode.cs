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
    [NodeTint(NodeProprieties.NodeTintYellow)]
    public class ExternalNode : NodeOutput
    {
        [SerializeField] private NodeBase node;
        public override float Sample1D(float x) => node.Sample1D(x);
        public override float Sample2D(float x, float y) => node.Sample2D(x, y);
        public override float Sample3D(float x, float y, float z) => node.Sample3D(x, y, z);
    }
}