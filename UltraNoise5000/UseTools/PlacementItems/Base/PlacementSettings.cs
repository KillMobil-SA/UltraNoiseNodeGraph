using Sirenix.OdinInspector;
using UnityEngine;
using NoiseUltra.Output;

namespace NoiseUltra.Tools.Placement
{
    public class PlacementSettings : ScriptableObject
    {
        private const string PlacementSettingsName = "Placement Settings";
        private const string PositionTabName = "Position";
        private const string RotationTabName = "Rotation";
        private const string SizeTabName = "Scale";
        
        
        [Header("Noise Settings")]
        public ExportNode exportNode;

        [SerializeField]
        [Header(PlacementSettingsName)]
        [TabGroup(PlacementSettingsName, SizeTabName) , InlineProperty () , HideLabel()]   
        private ScaleSettings placementScaleSettings = new ScaleSettings();

        [SerializeField]
        [TabGroup(PlacementSettingsName, RotationTabName) , InlineProperty () , HideLabel()]
        private RotationSettings placementRotationSettings = new RotationSettings();

        [SerializeField]
        [TabGroup(PlacementSettingsName, PositionTabName) , InlineProperty () , HideLabel()]
        private PositionSettings placementPositionsSettings = new PositionSettings();

        [BoxGroup("Live Preview Settings")] [SerializeField]
        private Color livePreviewColor = Color.green;

        [SerializeField]
        [BoxGroup("Live Preview Settings")]
        private float livePreviewSizeMultiplier = 1;

        private void OnEnable()
        {
            placementPositionsSettings.OnEnable();
            placementRotationSettings.OnEnable();
            placementScaleSettings.OnEnable();
        }

        public float GetSample(float x, float z)
        {
            return exportNode.GetSample(x, z);
        }

        public float GetSample(Vector3 pos)
        {
            return exportNode.GetSample(pos.x, pos.y, pos.z);
        }

        public void InitializeProperties()
        {
            placementScaleSettings.InitProperties();
            placementRotationSettings.InitProperties();
            placementPositionsSettings.InitProperties();
        }

        public bool IsPositionValid(Vector3 pos)
        {
            return placementPositionsSettings.IsPositionValid(pos);
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
            var placemntScale = GetScale(pos, v) * livePreviewSizeMultiplier;
            
            Gizmos.color = livePreviewColor;
            Gizmos.DrawCube(placemntPos, placemntScale);
            Gizmos.color = Color.white;
        }
    }
}