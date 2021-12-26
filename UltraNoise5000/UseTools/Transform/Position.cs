using UnityEngine;
using NoiseUltra.Nodes;
using Sirenix.OdinInspector;

namespace NoiseUltra.TransformNodes
{
    public class Position : NodeInputOutput
    {
        [SerializeField , OnValueChanged(nameof(DrawAsync))]
        private Vector3 offSet = new Vector3(0, 0, 0);

        public override float GetSample(float x)
        {
            
            return base.GetSample(x + offSet.x);
        }

        public override float GetSample(float x, float y)
        {
            
            return base.GetSample(x+ offSet.x, y + offSet.y);
        }

        public override float GetSample(float x, float y, float z)
        {
            
            return  base.GetSample(x+ offSet.x, y+ offSet.y, z + offSet.z);
        }
        
    }
}