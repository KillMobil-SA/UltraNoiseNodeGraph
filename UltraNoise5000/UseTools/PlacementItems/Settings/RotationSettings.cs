using System;
using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = System.Random;

namespace NoiseUltra.Tools.Placement
{
    [Serializable]
    public class RotationSettings :PlacementProperties
    {
        
        [SerializeField] private Vector3 randomRotation;
        [SerializeField] private bool useNoiseRandomizationBase;
        [ShowIf("useNoiseRandomizationBase")] [SerializeField]
        private bool useExternalNoiseSource;

        [ShowIf("useExternalNoiseSource")] [SerializeField]
        private NodeBase externalSource;

        public bool roundRotation;

        [ShowIf("roundRotation")] [SerializeField]
        private float rotationRound;

        
  
        public override Vector3 Calculator(Vector3 pos, float thresHold)
        {
            if (useNoiseRandomizationBase)
                NoiseRandomization(pos, thresHold);

            var xRot =(float) random.Next(Mathf.RoundToInt(-randomRotation.x * DemDevide), Mathf.RoundToInt(randomRotation.x* DemDevide)) / DemDevide;
            var yRot =(float) random.Next(Mathf.RoundToInt(-randomRotation.y * DemDevide), Mathf.RoundToInt(randomRotation.y* DemDevide)) / DemDevide;
            var zRot =(float) random.Next(Mathf.RoundToInt(-randomRotation.z * DemDevide), Mathf.RoundToInt(randomRotation.z* DemDevide)) / DemDevide;
     

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

                v = externalSource.GetSample(pos.x, pos.y, pos.z);
            else
                v = thresHold;
            
            random = new Random(Mathf.RoundToInt(v * 9999));

        }

        private float RoundRotation(float yRotation)
        {
            var amount = Mathf.Round(yRotation / rotationRound);
            return amount * rotationRound;
        }


  
    }
}