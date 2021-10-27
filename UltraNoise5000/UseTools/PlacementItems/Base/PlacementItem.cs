using System;
using NoiseUltra.Output;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    [Serializable]
    public class PlacementItem
    {
        [SerializeField] public bool active;
        [SerializeField, Min(0.1f)] public float spacing = 10;
        [SerializeField] private ExportNode exportNode;
        [SerializeField] public PlacementSettings plamentHandler;

        public bool GenerateObject(PlacementBounds placementBound, Vector3 placementPosition, bool isDebug, Transform parent, bool useWorldCordinates, bool cordinatesAbs)
        {
            if (plamentHandler.exportNode == null || plamentHandler == null)
                return false;

            var pos = placementBound.GetPosVector(placementPosition);
            var v = GetSample(pos, placementBound, useWorldCordinates , cordinatesAbs);

            if (!IsPositionValid(pos, v))
                return false;

            if (isDebug)
                plamentHandler.DebugObject(pos, v);
            else
                plamentHandler.PlaceObject(pos, v, parent);

            return true;
        }

        private float GetSample(Vector3 pos, PlacementBounds placementBounds, bool useWorldCordinates, bool cordinatesAbs)
        {

            if (cordinatesAbs) pos = new Vector3(Mathf.Abs(pos.x), Mathf.Abs(pos.y), Mathf.Abs(pos.z));
            // to enable world relativity later
            if (!useWorldCordinates)
            pos -= placementBounds.center;
            var is2D = placementBounds.heightIs2D;
            return is2D ? plamentHandler.GetSample(pos.x, pos.z) : plamentHandler.GetSample(pos);
        }

        private bool IsPositionValid(Vector3 pos, float v)
        {
            if (v <= 0)
                return false;

            var heightPlacementCheck = plamentHandler.IsPositionValid(pos);
            return heightPlacementCheck;
        }

        [Button]
        public void UpDateNoiseFile()
        {
            plamentHandler.exportNode = exportNode;
        } 
    }
}