using Sirenix.OdinInspector;
using UnityEngine;
using NoiseUltra.Output;

namespace NoiseUltra.Tools.Placement
{
    public class PlacementSettings : ScriptableObject
    {
        private const string PLACEMENT_SETTINGS_NAME = "Placement Settings";
        private const string POSITION_TAB_NAME = "Position";
        private const string ROTATION_TAB_NAME = "Rotation";
        private const string SIZE_TAB_NAME = "Scale";
        private const string NOISE_SETTINGS_NAME = "Noise Settings";
        private const string LIVE_PREVIEW_SETTINGS_NAME = "Live Preview Settings";

        [Header(NOISE_SETTINGS_NAME)] 
        public ExportNode exportNode;

        [SerializeField]
        [Header(PLACEMENT_SETTINGS_NAME)]
        [TabGroup(PLACEMENT_SETTINGS_NAME, SIZE_TAB_NAME)]
        [InlineProperty]
        [HideLabel]
        private ScaleSettings scale = new ScaleSettings();

        [SerializeField]
        [TabGroup(PLACEMENT_SETTINGS_NAME, ROTATION_TAB_NAME)]
        [InlineProperty]
        [HideLabel]
        private RotationSettings rotation = new RotationSettings();

        [SerializeField]
        [TabGroup(PLACEMENT_SETTINGS_NAME, POSITION_TAB_NAME)]
        [InlineProperty]
        [HideLabel]
        private PositionSettings position = new PositionSettings();

        [BoxGroup(LIVE_PREVIEW_SETTINGS_NAME)]
        [SerializeField]
        private Color livePreviewColor = Color.green;

        [SerializeField]
        [BoxGroup(LIVE_PREVIEW_SETTINGS_NAME)]
        private float livePreviewSizeMultiplier = 1;

        public void Initialize()
        {
             scale.Initialize();
             rotation.Initialize();
             position.Initialize();
        }

        private void OnEnable()
        {
            position.OnEnable();
            rotation.OnEnable();
            scale.OnEnable();
        }

        public float GetSample(float x, float z)
        {
            return exportNode.GetSample(x, z);
        }

        public float GetSample(Vector3 pos)
        {
            return exportNode.GetSample(pos.x, pos.y, pos.z);
        }
        
        public bool IsPositionValid(Vector3 pos)
        {
            return position.IsPositionValid(pos);
        }

        public Vector3 GetPos(Vector3 pos, float v)
        {
            return position.Execute(pos, v);
        }

        public Vector3 GetRot(Vector3 pos, float v)
        {
            return rotation.Execute(pos, v);
        }

        public Vector3 GetScale(Vector3 pos, float v)
        {
            return scale.Execute(pos, v);
        }

        public virtual void PlaceObject(Vector3 pos, float v, Transform parent)
        {
        }

        public virtual void CleanObjects(Transform parent)
        {
        }

        public void DebugObject(Vector3 pos, float v)
        {
            Vector3 currentPosition = GetPos(pos, v);
            Vector3 currentScale = GetScale(pos, v) * livePreviewSizeMultiplier;

            Gizmos.color = livePreviewColor;
            Gizmos.DrawCube(currentPosition, currentScale);
            Gizmos.color = Color.white;
        }
    }
}