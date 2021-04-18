using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


namespace NoiseUltra
{
    [System.Serializable]
    public class UltraPlacementRotationSettings
    {
        public bool useNoiseRandomizationBase = false;
        [ShowIf("useNoiseRandomizationBase", true)]
        public bool useExternalNoiseSource = false; 
        [ShowIf("useExternalNoiseSource", true)] public NoiseNodeBase externalNoiseSource;
        public Vector3 randomRotation;


        public bool roundRotation;
        [ShowIf("roundRotation", true)] public float rotationRound;

        public Vector3 RotationCalculator(Vector3 pos, float thresHold)
        {

            if (useNoiseRandomizationBase)
                NoiseRandomization(pos , thresHold);
            
            float xRot = Random.Range((float) -randomRotation.x, (float) randomRotation.x);
            float yRot = Random.Range((float) -randomRotation.y, (float) randomRotation.y);
            float zRot = Random.Range((float) -randomRotation.z, (float) randomRotation.z);

            if (roundRotation)
            {
                xRot = RoundRotation(xRot);
                yRot = RoundRotation(yRot);
                zRot = RoundRotation(zRot);
            }

            Vector3 rotation = new Vector3(xRot, yRot, zRot);
            return rotation;
        }

        void NoiseRandomization(Vector3 pos, float thresHold)
        {
            float v;
            if (useExternalNoiseSource)

                v = externalNoiseSource.Sample3D(pos.x, pos.y, pos.z);
            else
                v = thresHold;
                
            Random.InitState(Mathf.RoundToInt(v * 9999));
        }

        float RoundRotation(float yRotation)
        {
            float amount = Mathf.Round(yRotation / rotationRound);
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
