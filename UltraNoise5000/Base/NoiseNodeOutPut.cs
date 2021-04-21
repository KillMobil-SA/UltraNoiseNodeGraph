using System;
using System.Collections;
using System.Collections.Generic;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using XNode;

namespace NoiseUltra {
    [NodeTint(0.1f ,0.45f ,0.32f )]
    public class NoiseNodeOutPut : NoiseNodeBase {
        // Start is called before the first frame update

        [Input] public NoiseNodeBase noiseA;

        public override float Sample1D (float x) {
            if (noiseAInput == null) return -1;

            float v = noiseAInput.Sample1D (x);

            return IdentifyBounds (v);
        }

        public override float Sample2D (float x, float y) {
            if (noiseAInput == null) return -1;

            float v = (noiseAInput.Sample2D (x, y));

            return IdentifyBounds (v);
        }

        public override float Sample3D (float x, float y, float z) {
            if (noiseAInput == null) return -1;

            float v = (noiseAInput.Sample3D (x, y, z));

            return IdentifyBounds (v);
        }

        public override void OnCreateConnection (NodePort from, NodePort to) {
            _noiseAInput = null;
            _noiseBInput = null;
            UpdateValues ();
            UpdatePreview ();
        }

        private NoiseNodeBase _noiseAInput;
        private NoiseNodeBase noiseAInput {
            get {
                if (_noiseAInput == null)
                    _noiseAInput = GetInputValue<NoiseNodeBase> ("noiseA", this.noiseA);
                return _noiseAInput;
            }
            set { _noiseAInput = value; }
        }

        private NoiseNodeBase _noiseBInput;

    }
}