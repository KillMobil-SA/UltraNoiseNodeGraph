using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace NoiseUltra.Generators {
    public class NoiseNodeGrandienLine : NoiseNodeBase
    {
        [OnValueChanged("Update")] public GrandientLineType lineType;
        [OnValueChanged ("Update")]
        public float start;
        [OnValueChanged ("Update")]
        public float end;


        public override float Sample1D (float x) {
            return Mathf.Lerp(start, end, x);
        }

        public override float Sample2D (float x, float y)
        {
            if(lineType == GrandientLineType.Horizontal)
                return Mathf.InverseLerp(start, end, x);
            else
                return Mathf.InverseLerp(start, end, y);
        }
        public override float Sample3D (float x, float y, float z) {
    
            if(lineType == GrandientLineType.Horizontal)
                return Mathf.InverseLerp(start, end, x);
            else
                return Mathf.InverseLerp(start, end, y);
        }

        [Output] public NoiseNodeBase noiseOutPut;
        public override object GetValue (NodePort port) {
            return this;
        }

        public enum GrandientLineType
        {
            Horizontal,
            Vertical
            
        }

    }
}