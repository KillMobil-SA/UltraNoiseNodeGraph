using UnityEngine;

namespace XNode.Examples.MathNodes
{
    public class Vector : Node
    {
        [Input] public float x, y, z;
        [Output] public Vector3 vector;

        public override object GetValue(NodePort port)
        {
            vector.x = GetInputValue("x", x);
            vector.y = GetInputValue("y", y);
            vector.z = GetInputValue("z", z);
            return vector;
        }
    }
}