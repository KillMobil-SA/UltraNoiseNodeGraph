using UnityEngine;
using XNode;

namespace NoiseUltra.Nodes
{
    /// <summary>
    ///  Class is able to sample and process a Two Input Nodes and a Single Output Node.
    /// </summary>
    public abstract class NodeDoubleInputOutput : NodeOutput
    {
        private const float MinStrength = 0;
        private const float MaxStrength = 1; // I think the max go be bigger than 1 here, no?
        
        [SerializeField, Range(MinStrength, MaxStrength)] 
        private float strengthA;
		
        [SerializeField, Range(MinStrength, MaxStrength)] 
        private float strengthB;
        
        [SerializeField, Input] 
        private NodeBase inputA;

        [SerializeField, Input] 
        private NodeBase inputB;

        private NodeBase GetInputA()
        {
            return GetNode(inputA);
        }

        private NodeBase GetInputB()
        {
            return GetNode(inputB);
        }
        
        public override float Sample1D(float x)
        {
            var sampleA = GetInputA().Sample1D(x);
            var sampleB = GetInputB().Sample1D(x);
            return ExecuteOperation(sampleA * strengthA, sampleB * strengthB);
        }

        public override float Sample2D(float x, float y)
        {
            var sampleA = GetInputA().Sample2D(x, y);
            var sampleB = GetInputB().Sample2D(x, y);
            return ExecuteOperation(sampleA * strengthA, sampleB * strengthB);
        }
        
        public override float Sample3D(float x, float y, float z)
        {
            var sampleA = GetInputA().Sample3D(x, y, z);
            var sampleB = GetInputB().Sample3D(x, y, z);
            return ExecuteOperation(sampleA * strengthA * strengthA, sampleB * strengthB);
        }

        /// <summary>
        ///     Gets the final operation from the sub class.
        /// </summary>
        protected abstract float ExecuteOperation(float strengthenedA, float strengthenedB);
    }
}