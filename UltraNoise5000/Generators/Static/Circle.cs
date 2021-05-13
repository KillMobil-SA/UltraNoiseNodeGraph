using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Generators.Static
{
    [NodeTint(NodeProprieties.NodeTintGreen)]
    public class Circle : NodeOutput
    {
        private const float Default = 100;
        [SerializeField] private Vector3 center = new Vector2(Default, Default);
        [SerializeField, OnValueChanged(nameof(RebuildCurve))] private float radius = Default;
        [SerializeField] private bool hardEdges;
        [SerializeField] private AnimationCurve heightCurve = AnimationCurve.Linear(0, 0, Default, 1);


        public override float GetSample(float x)
        {
            return 1;
        }

        protected override void Init()
        {
            base.Init();
            RebuildCurve();
        }

        [Button]
        private void RebuildCurve()
        {
            heightCurve = AnimationCurve.Linear(0, 0, radius, 1);
        }

        public override float GetSample(float x, float y)
        {
            var distance = Vector2.Distance(center, new Vector2(x, y));
            
            var isInRange = distance < radius;
            if (hardEdges)
            {
                return isInRange ? 1 : 0;
            }

            if (isInRange)
            {
                return 1 - heightCurve.Evaluate(distance);
            }

            return 0;
        }

        public override float GetSample(float x, float y, float z)
        {
            var distance = Vector3.Distance(center, new Vector3(x, y, z));
            var isInRange = distance < radius;

            if (hardEdges)
            {
                return isInRange ? 1 : 0;
            }

            if (distance < radius)
            {
                return 1 - heightCurve.Evaluate(distance);
            }

            return 0;
        }
    }
}