using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    [Serializable]
    public sealed class PlacementItem
    {
        [SerializeField]
        [TableColumnWidth(40, false)]
        public bool active;

        [SerializeField]
        [TableColumnWidth(60, false)]
        [Min(0.1f)]
        public float spacing = 10;

        public PlacementSettings settings;

        public bool GenerateObject(PlacementBounds placementBound, Vector3 placementPosition, bool isLivePreview, Transform parent, bool useWorldCoordinates, bool useAbsoluteCoordinates)
        {
            if (settings.exportNode == null || settings == null)
            {
                return false;
            }
            
            Vector3 pos = placementBound.GetPosVector(placementPosition);

            if (useAbsoluteCoordinates)
            {
                pos = new Vector3(Mathf.Abs(pos.x), Mathf.Abs(pos.y), Mathf.Abs(pos.z));
            }

            if (!useWorldCoordinates)
            {
                pos -= placementBound.center;
            }

            bool is2D = placementBound.heightIs2D;
            float sample = is2D ? settings.GetSample(pos.x, pos.z) : settings.GetSample(pos);

            if (!settings.IsPositionValid(pos))
            {
                return false;
            }

            if (isLivePreview)
                settings.DebugObject(pos, sample);
            else
                settings.PlaceObject(pos, sample, parent);

            return true;
        }
    }
}