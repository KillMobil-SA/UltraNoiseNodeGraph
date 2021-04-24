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

        public override float Sample1D(float x)
        {
            return GetInput().Sample1D(x);
        }

        public override float Sample2D(float x, float y)
        {
            return GetInput().Sample2D(x, y);
        }

        public override float Sample3D(float x, float y, float z)
        {
            return GetInput().Sample3D(x, y, z);
        }

        protected NodeBase GetInput()
        {
            return GetInputValue(nameof(input), input);
        }
    }
}