using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Utilities 
{
    [NodeTintAttribute(NodeProprieties.NodeTintPurple)]
    public class Coupler : NodeModifier
    {

        protected override float ApplyModifier(float sample)
        {
            return sample;
        }
    }
}