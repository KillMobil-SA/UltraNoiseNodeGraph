using UnityEngine;
using XNode;

namespace NoiseUltra.Nodes
{
    /// <summary>
    ///  Class is able to sample and process a Single Input Node and a Single Output Node.
    /// </summary>
    public abstract class NodeInputOutput : NodeOutput
    {
        [SerializeField, Input] 
        private NodeBase input;

        private bool IsValid => GetInput() != null;

        public override float Sample1D(float x)
        {
            if (!IsValid)
                return -1;
            
            return GetInput().Sample1D(x);
        }

        public override float Sample2D(float x, float y)
        {
            if (!IsValid)
                return -1;

            return GetInput().Sample2D(x, y);
        }

        public override float Sample3D(float x, float y, float z)
        {
            if (!IsValid)
                return -1;

            return GetInput().Sample3D(x, y, z);
        }

        private NodeBase GetInput()
        {
            return GetInputValue(nameof(input), input);
        }
    }
}