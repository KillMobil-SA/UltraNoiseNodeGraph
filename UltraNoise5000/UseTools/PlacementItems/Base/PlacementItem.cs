using System;
using NoiseUltra.Output;
using UnityEngine;

namespace NoiseUltra.Tools.Placement

{
    [Serializable]
    public class PlacementItem
    {
        public bool active;
        [Min(1)] public float spacing = 10;
        public NodeExport nodeExport;
        public PlacementSettings plamentHandler;

        public bool GenerateObject(PlacementBounds placementBound, Vector3 plaementPos, bool isDebug, Transform parent)
        {
            if (nodeExport == null || plamentHandler == null)
                return false;


            var pos = placementBound.GetPosVector(plaementPos);

            var v = GetNoise(pos, placementBound);

            if (!PlacementValidation(pos, v)) return false;

            if (isDebug) plamentHandler.DebugObject(pos, v);
            else
                plamentHandler.PlaceObject(pos, v, parent);

            return true;
        }

        private float GetNoise(Vector3 pos, PlacementBounds placementBounds)
        {
            // to enable world relativity later
            //if (!useWorldPos)
            //pos -= transform.position;

            if (placementBounds.heightIs2D)
                return nodeExport.Sample2D(pos.x, pos.z);
            return nodeExport.Sample3D(pos.x, pos.y, pos.z);
        }

        private bool PlacementValidation(Vector3 pos, float v)
        {
            if (v <= 0) return false;

            var heightPlacementCheck = plamentHandler.ChechPos(pos);
            if (!heightPlacementCheck)
                return false;

            return true;
        }
    }
}