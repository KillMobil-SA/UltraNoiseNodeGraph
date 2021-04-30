using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    [System.Serializable]
    public class PositionSettings : ShowOdinSerializedPropertiesInInspectorAttribute
    {
        public Vector3 randomPositioning;

        [Header("Height Calulations Settings"), SerializeField]
        private HeightPosType heightPosType = HeightPosType.Grid;

        [ShowIf("heightPosType", HeightPosType.Raycast), SerializeField]
        private RayCastHeightPos rayCastHeightPos = new RayCastHeightPos();

        [ShowIf("heightPosType", HeightPosType.Noise), SerializeField]
        private NoiseHeightPos noiseHeightPos = new NoiseHeightPos();

        public Vector3 PotisionCalculator(Vector3 pos, float thresHold)
        {
            Vector3 sourceHeightCalculation = new Vector3(pos.x, CalculateHeight(pos), pos.z);

            float xPos = Random.Range(-randomPositioning.x, randomPositioning.x);
            float yPos = Random.Range(-randomPositioning.y, randomPositioning.y);
            float zPos = Random.Range(-randomPositioning.z, randomPositioning.z);
            Vector3 randomPosition = new Vector3(xPos, yPos, zPos);

            return sourceHeightCalculation + randomPosition;
        }

        float CalculateHeight(Vector3 pos)
        {
            switch (heightPosType)
            {
                case HeightPosType.Grid:
                    return pos.y;
                case HeightPosType.Noise:
                    return noiseHeightPos.GetHeightPos(pos);
                case HeightPosType.Raycast:
                    return rayCastHeightPos.GetHeightPos(pos);
                default:
                    return pos.y;
            }
        }

        public bool ChechPos(Vector3 pos)
        {
            switch (heightPosType)
            {
                case HeightPosType.Grid:
                    return true;
                case HeightPosType.Noise:
                    return true;
                //return noiseHeightPos.HeightCheck (pos);
                case HeightPosType.Raycast:
                    return rayCastHeightPos.HeightCheck(pos);
                default:
                    return true;
            }
        }
    }
}