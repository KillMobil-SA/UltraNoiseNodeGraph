using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Generators.Static
{
    [NodeTint(NodeProprieties.NodeTintGreen)]
    public class Triangle : NodeOutput
    {
        [SerializeField] private Vector2 p1;

        [SerializeField] private Vector2 p2;

        [SerializeField] private Vector2 p3;

        public override float Sample1D(float x) => 1;

        public override float Sample2D(float x, float y)
        {
            return IsInTriangle(new Vector2(x, y)) ? 1 : 0;
        }

        public override float Sample3D(float x, float y, float z)
        {
            return 1;
        }

        private float Sign(Vector2 p1, Vector2 p2, Vector2 p3)
        {
            return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
        }

        private bool IsInTriangle(Vector2 point)
        {
            var d1 = Sign(point, p1, p2);
            var d2 = Sign(point, p2, p3);
            var d3 = Sign(point, p3, p1);
            var hasNeg = (d1 < 0) || (d2 < 0) || (d3 < 0);
            var hasPos = (d1 > 0) || (d2 > 0) || (d3 > 0);
            return !(hasNeg && hasPos);
        }
    }
}