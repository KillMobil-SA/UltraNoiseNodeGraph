using UnityEngine;
using NoiseUltra.Nodes;
using Sirenix.OdinInspector;


namespace NoiseUltra.Modifiers
{
    [NodeTint(NodeColor.MODIFIER)]
    public class AbsCordinates :NodeInputOutput
    {
        
        public override float GetSample(float x)
        {
            
            return base.GetSample(Mathf.Abs(x));
        }

        public override float GetSample(float x, float y)
        {
            
            return base.GetSample(Mathf.Abs(x), Mathf.Abs(y));
        }

        public override float GetSample(float x, float y, float z)
        {
            
            return  base.GetSample(Mathf.Abs(x), Mathf.Abs(y), Mathf.Abs(z) );
        }
        
    }
}