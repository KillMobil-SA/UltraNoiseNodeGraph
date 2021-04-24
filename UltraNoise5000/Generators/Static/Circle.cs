using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NoiseUltra.Nodes;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace NoiseUltra.Generators.Static
{
    public class Circle : NodeOutput
    {
        private const float Default = 100; 
        [SerializeField] private Vector2 center = new Vector2(Default, Default);
        [SerializeField] private float radius = Default;
        
        public override float Sample1D(float x) => 1;

        public override float Sample2D(float x, float y)
        {
            float distance = Vector2.Distance(center, new Vector2(x, y));
            if (distance < radius)
            {
                return 1 - distance / radius;
            }
            
            return 0;
        }

        public override float Sample3D(float x, float y, float z)
        {
            float distance = Vector3.Distance(center, new Vector3(x, y, z));
            if (distance < radius)
            {
                return 1 - distance / radius;
            }
            
            return 0;
        }
    }
}