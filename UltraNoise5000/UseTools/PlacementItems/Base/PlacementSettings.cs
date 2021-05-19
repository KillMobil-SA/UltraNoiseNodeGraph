using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    public class PlacementSettings : ScriptableObject
    {
        [Header("Placement Settings")] [TabGroup("Placement Settings", "Size")] [SerializeField]
        private ScaleSettings placementScaleSettings = new ScaleSettings();

        [TabGroup("Placement Settings", "Rotation")] [SerializeField]
        private RotationSettings placementRotationSettings = new RotationSettings();

        [TabGroup("Placement Settings", "Position")] [SerializeField]
        private PositionSettings placementPositionsSettings = new PositionSettings();

        public Color debugColor;
        public float debugSizeMultiplier = 1;

        private void OnEnable()
        {
            placementPositionsSettings.OnEnable();
        }

        public void InitializeProperties()
        {
            placementScaleSettings.InitPropertie();
            placementRotationSettings.InitPropertie();
            placementPositionsSettings.InitPropertie();
        }
        public bool ChechPos(Vector3 pos)
        {
            return placementPositionsSettings.ChechPos(pos);
        }

        public Vector3 GetPos(Vector3 pos, float v)
        {
            return placementPositionsSettings.Calculator(pos, v);
        }

        public Vector3 GetRot(Vector3 pos, float v)
        {
            return placementRotationSettings.Calculator(pos, v);
        }

        public Vector3 GetScale(Vector3 pos, float v)
        {
            return placementScaleSettings.Calculator(pos, v);
        }

        public virtual void PlaceObject(Vector3 pos, float v, Transform parent)
        {
        }

        public virtual void CleanObjects(Transform parent)
        {
        }

        public void DebugObject(Vector3 pos, float v)
        {
            var placemntPos = GetPos(pos, v);
            var placemntScale = GetScale(pos, v) * debugSizeMultiplier;

            Gizmos.color = debugColor;
            Gizmos.DrawCube(placemntPos, placemntScale);
            Gizmos.color = Color.white;
        }
    }
}