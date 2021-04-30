using System;
using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NoiseUltra
{
    [Serializable]
    public class UltraPlacementRotationSettings
    {
        public bool useNoiseRandomizationBase;

        [ShowIf("useNoiseRandomizationBase")] public bool useExternalNoiseSource;

        [ShowIf("useExternalNoiseSource")] public NodeBase externalSource;
        public Vector3 randomRotation;


        public bool roundRotation;
        [ShowIf("roundRotation")] public float rotationRound;

        public Vector3 RotationCalculator(Vector3 pos, float thresHold)
        {
            if (useNoiseRandomizationBase)
                NoiseRandomization(pos, thresHold);

            var xRot = Random.Range(-randomRotation.x, randomRotation.x);
            var yRot = Random.Range(-randomRotation.y, randomRotation.y);
            var zRot = Random.Range(-randomRotation.z, randomRotation.z);

            if (roundRotation)
            {
                xRot = RoundRotation(xRot);
                yRot = RoundRotation(yRot);
                zRot = RoundRotation(zRot);
            }

            var rotation = new Vector3(xRot, yRot, zRot);
            return rotation;
        }

        private void NoiseRandomization(Vector3 pos, float thresHold)
        {
            float v;
            if (useExternalNoiseSource)

                v = externalSource.Sample3D(pos.x, pos.y, pos.z);
            else
                v = thresHold;

            Random.InitState(Mathf.RoundToInt(v * 9999));
        }

        private float RoundRotation(float yRotation)
        {
            var amount = Mathf.Round(yRotation / rotationRound);
            return amount * rotationRound;

            /*
            if (90 < yRotation + 45 && 90 > yRotation - 45) {
                return 90;
            } else if (180 < yRotation + 45 && 180 > yRotation - 45) {
                return 180;
            } else if (270 < yRotation + 45 && 270 > yRotation - 45) {
                return 270;
            } else if (0 < yRotation + 45 && 0 > yRotation - 45) {
                return 0;
            } else if (360 < yRotation + 45 && 360 > yRotation - 45) {
                return 0;
            } else {
                return 0;
            }
            */
        }
    }
}