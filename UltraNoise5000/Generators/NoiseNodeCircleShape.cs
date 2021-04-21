using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace NoiseUltra.Generators {
    public class NoiseNodeCircleShape : NoiseNodeBase {
        [OnValueChanged ("UpdateValues")]
        public Vector3 center;
        [OnValueChanged ("UpdateValues")]
        public float radius;


        public override float Sample1D (float x) {
            float v = 1;
            return IdentifyBounds (v);
        }

        public override float Sample2D (float x, float y)
        {
            Vector2 centerZ = new Vector2(center.x, center.z);
            float distance = Vector2.Distance (centerZ, new Vector2 (x, y));

            if (distance < radius)
                return 1 - IdentifyBounds (distance / radius);
            else
                return 0;

        }
        public override float Sample3D (float x, float y, float z) {
            float distance = Vector3.Distance (center, new Vector3 (x, y, z));

            if (distance < radius)
                return 1 - IdentifyBounds (distance / radius);
            else
                return 0;
        }

        [Output] public NoiseNodeBase noiseOutPut;
        public override object GetValue (NodePort port) {
            return this;
        }

    }
}