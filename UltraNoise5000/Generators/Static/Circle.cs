using System;
using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Generators.Static
{
    [NodeTint(NodeColor.GENERATOR_OTHER)]
    public class Circle : NodeOutput
    {
        private const float Default = 0;
        [SerializeField] 
        private Vector3 center = new Vector2(Default, Default);
        
        [SerializeField]
        private float radius = Default;
        
        [SerializeField] 
        private bool hardEdges;
        

        public override float GetSample(float x)
        {
            return 1;
        }

        public override float GetSample(float x, float y)
        {
            var distance = Vector2.Distance(center, new Vector2(x, y));
            var isInRange = distance < radius;
            
            if (hardEdges)
                return isInRange ? 1 : 0;
            
            return 1 -  Mathf.InverseLerp(0 , radius , distance);
        }

        public override float GetSample(float x, float y, float z)
        {
            var distance = Vector3.Distance(center, new Vector3(x, y, z));
            var isInRange = distance < radius;

            if (hardEdges)
                return isInRange ? 1 : 0;
            
            return 1-  Mathf.InverseLerp(0 , radius , distance);
        }
    }
}