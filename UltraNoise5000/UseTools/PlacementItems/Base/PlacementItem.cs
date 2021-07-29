using System;
using NoiseUltra.Output;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    [Serializable]
    public class PlacementItem
    {
        [SerializeField] public bool active;
        [SerializeField, Min(0.1f)] public float spacing = 10;
        [SerializeField] private  ExportNode exportNode;
        [SerializeField] public PlacementSettings plamentHandler;

        public bool GenerateObject(PlacementBounds placementBound, Vector3 placementPosition, bool isDebug, Transform parent)
        {
            if (exportNode == null || plamentHandler == null)
                return false;

            var pos = placementBound.GetPosVector(placementPosition);
            var v = GetSample(pos, placementBound);

            if (!PlacementValidation(pos, v)) 
                return false;

            if (isDebug) 
                plamentHandler.DebugObject(pos, v);
            else
                plamentHandler.PlaceObject(pos, v, parent);

            return true;
        }

        private float GetSample(Vector3 pos, PlacementBounds placementBounds)
        {
            // to enable world relativity later
            //if (!useWorldPos)
            //pos -= transform.position;
            var is2D = placementBounds.heightIs2D;
            return  is2D ? GetSample(pos.x, pos.z) : GetSample(pos);
        }

        private float GetSample(float x, float z)
        {
            return exportNode.GetSample(x, z);
        }
        
        private float GetSample(Vector3 pos)
        {
            return exportNode.GetSample(pos.x, pos.y, pos.z);
        }

        private bool PlacementValidation(Vector3 pos, float v)
        {
            if (v <= 0) 
                return false;

            var heightPlacementCheck = plamentHandler.ChechPos(pos);
            return heightPlacementCheck;
        }
    }
}