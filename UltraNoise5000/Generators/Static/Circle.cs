using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Generators.Static
{
    [NodeTint(NodeProprieties.NodeTintGreen)]
    public class Circle : NodeOutput
    {
        private const float Default = 100;
        [SerializeField] private Vector2 center = new Vector2(Default, Default);
        [SerializeField] private float radius = Default;
        [SerializeField] private bool hardEdges;

        public override float Sample1D(float x)
        {
            return 1;
        }

        public override float Sample2D(float x, float y)
        {
            var distance = Vector2.Distance(center, new Vector2(x, y));
            var isInRange = distance < radius;
            if (hardEdges) return isInRange ? 1 : 0;

            if (isInRange) return 1 - distance / radius;

            return 0;
        }

        public override float Sample3D(float x, float y, float z)
        {
            var distance = Vector3.Distance(center, new Vector3(x, y, z));
            var isInRange = distance < radius;

            if (hardEdges) return isInRange ? 1 : 0;

            if (distance < radius) return 1 - distance / radius;

            return 0;
        }
    }
}