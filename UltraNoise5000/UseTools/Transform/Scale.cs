using UnityEngine;
using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
namespace NoiseUltra.TransformNodes
{
    public class Scale  : NodeInputOutput
    {
        
        [SerializeField , OnValueChanged(nameof(DrawAsync))]
        private Vector3 scaleOffSet = new Vector3(1, 1, 1);
        
        public override float GetSample(float x)
        {
            
            return base.GetSample(x * scaleOffSet.x);
        }

        public override float GetSample(float x, float y)
        {
            
            return base.GetSample(x * scaleOffSet.x, y * scaleOffSet.y);
        }

        public override float GetSample(float x, float y, float z)
        {
            
            return  base.GetSample(x * scaleOffSet.x, y * scaleOffSet.y, z * scaleOffSet.z);
        }
        
    }
}