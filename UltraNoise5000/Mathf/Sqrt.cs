using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Math {
    public class Sqrt : NodeModifier {
        protected override float ApplyModifier (float sample) {
            return Mathf.Sqrt (sample);
        }
    }
}