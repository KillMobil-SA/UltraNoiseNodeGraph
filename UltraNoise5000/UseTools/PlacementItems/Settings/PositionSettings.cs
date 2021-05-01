using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NoiseUltra.Tools.Placement
{
    [Serializable]
    public class PositionSettings
    {
        public Vector3 randomPositioning;

        [Header("Height Calulations Settings")] [OnValueChanged(nameof(UpdateHeightInterFace))] [SerializeField]
        private HeightPosType heightPosType = HeightPosType.Grid;

        [ShowIf(nameof(heightPosType), HeightPosType.Raycast)] [SerializeField]
        private RayCastHeightPos rayCastHeightPos = new RayCastHeightPos();

        [ShowIf("heightPosType", HeightPosType.Noise)] [SerializeField]
        private NoiseHeightPos noiseHeightPos = new NoiseHeightPos();

        [SerializeField] private IHeightBase _heightBase;
        private GridHeightPos noiseGridPos = new GridHeightPos();

        public PositionSettings()
        {
            UpdateHeightInterFace();
        }

        public void OnEnable()
        {
            UpdateHeightInterFace();
        }


        private void UpdateHeightInterFace()
        {
            _heightBase = noiseGridPos;
            switch (heightPosType)
            {
                case HeightPosType.Grid:
                    _heightBase = noiseGridPos;
                    break;
                case HeightPosType.Noise:
                    _heightBase = noiseHeightPos;
                    break;
                case HeightPosType.Raycast:
                    _heightBase = rayCastHeightPos;
                    break;
                default:
                    _heightBase = noiseGridPos;
                    break;
            }
        }

        public Vector3 PotisionCalculator(Vector3 pos, float thresHold)
        {
            var sourceHeightCalculation = new Vector3(pos.x, CalculateHeight(pos), pos.z);

            var xPos = Random.Range(-randomPositioning.x, randomPositioning.x);
            var yPos = Random.Range(-randomPositioning.y, randomPositioning.y);
            var zPos = Random.Range(-randomPositioning.z, randomPositioning.z);
            var randomPosition = new Vector3(xPos, yPos, zPos);

            return sourceHeightCalculation + randomPosition;
        }

        private float CalculateHeight(Vector3 pos)
        {
            return _heightBase.GetHeightPos(pos);
            /*  /// i keep this commented area Until we evaluate  current Height pos Implementation
            switch (heightPosType)
            {
                case HeightPosType.Grid:
                    return noiseGridPos.GetHeightPos(pos);
                case HeightPosType.Noise:
                    return noiseHeightPos.GetHeightPos(pos);
                case HeightPosType.Raycast:
                    return rayCastHeightPos.GetHeightPos(pos);
                default:
                    return pos.y;
            }
            */
        }

        public bool ChechPos(Vector3 pos)
        {
            return _heightBase.HeightCheck(pos);
            /*
            switch (heightPosType)
            {
                case HeightPosType.Grid:
                    return noiseGridPos.HeightCheck(pos);
                case HeightPosType.Noise:
                    return noiseHeightPos.HeightCheck(pos);
                case HeightPosType.Raycast:
                    return rayCastHeightPos.HeightCheck(pos);
                default:
                    return true;
            }
            */
        }
    }
}