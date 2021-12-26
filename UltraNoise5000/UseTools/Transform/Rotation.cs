using UnityEngine;
using NoiseUltra.Nodes;
using Sirenix.OdinInspector;


namespace NoiseUltra.TransformNodes
{
    public class Rotation  : NodeInputOutput
    {
        [SerializeField , OnValueChanged(nameof(DrawAsync))]
        private Vector3 roationOffSet = new Vector3(0, 0, 0);
        
        
        public override float GetSample(float x)
        {
            var resultVector = ConvertVector(new Vector3(x, 0, 0), Quaternion.Euler(roationOffSet), Vector3.zero,
                Vector3.one);
            
            return base.GetSample(resultVector.x);
        }

        public override float GetSample(float x, float y)
        {
            
            var resultVector = ConvertVector(new Vector3(x, y, 0), Quaternion.Euler(roationOffSet), Vector3.zero,
                Vector3.one);
            
            return  base.GetSample(resultVector.x, resultVector.y);
            
           
        }
        
        public override float GetSample(float x, float y, float z)
        {
            var resultVector = ConvertVector(new Vector3(x, y, z), Quaternion.Euler(roationOffSet), Vector3.zero,
                Vector3.one);
            
            return  base.GetSample(resultVector.x, resultVector.y, resultVector.z);
        }

        
        public  Vector3 ConvertVector (Vector3 source, Quaternion rotate, Vector3 move, Vector3 scale)
        {
            Matrix4x4 m = Matrix4x4.TRS (move, rotate, scale); 
            Vector3 result = m.MultiplyPoint3x4 (source);
            return result;
        }
        
    }
}