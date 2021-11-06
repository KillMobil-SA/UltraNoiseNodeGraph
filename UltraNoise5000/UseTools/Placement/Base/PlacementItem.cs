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

        public bool GenerateObject(PlacementBounds placementBound, Vector3 placementPosition, bool isLivePreview,
            Transform parent, bool useWorldCoordinates, bool useAbsoluteCoordinates)
        {
            if (settings.exportNode == null || settings == null)
            {
                return false;
            }
            
            Vector3 pos = placementBound.GetPosVector(placementPosition);
            float sample = GetSample(pos, placementBound, useWorldCoordinates, useAbsoluteCoordinates);

            if (!IsPositionValid(pos, sample))
            {
                return false;
            }

            if (isLivePreview)
                settings.DebugObject(pos, sample);
            else
                settings.PlaceObject(pos, sample, parent);

            return true;
        }

        private float GetSample(Vector3 pos, PlacementBounds placementBounds, bool useWorldCordinates,
            bool cordinatesAbs)
        {

            if (cordinatesAbs)
            {
                pos = new Vector3(Mathf.Abs(pos.x), Mathf.Abs(pos.y), Mathf.Abs(pos.z));
            }

            if (!useWorldCordinates)
            {
                pos -= placementBounds.center;
            }

            bool is2D = placementBounds.heightIs2D;
            return is2D ? settings.GetSample(pos.x, pos.z) : settings.GetSample(pos);
        }

        private bool IsPositionValid(Vector3 pos, float v)
        {
            if (v <= 0)
            {
                return false;
            }

            return settings.IsPositionValid(pos);
        }
    }
}