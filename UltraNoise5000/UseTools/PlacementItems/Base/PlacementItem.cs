using System;
using UnityEngine;
using NoiseUltra.Output;

namespace NoiseUltra.Tools.Placement

{
    [Serializable]
    public class PlacementItem
    { 
       public bool active;
       [Min(1)] public float spacing = 10;
       public NodeExport nodeExport;
       public PlacementSettings plamentHandler;

       public bool GenerateObject(PlacementBounds placementBound , Vector3 plaementPos, bool isDebug , Transform parent)
       {

           if (nodeExport == null || plamentHandler == null)
               return false;           
           
           
           Vector3 pos = placementBound.GetPosVector(plaementPos);

           float v = GetNoise(pos , placementBound);
               
           if (!PlacementValidation(pos, v)) return false;

           if (isDebug) plamentHandler.DebugObject(pos, v);
           else
           {
               plamentHandler.PlaceObject(pos, v, parent);
           }

           return true;
       }

       float GetNoise(Vector3 pos , PlacementBounds placementBounds)
       {
           // to enable world relativity later
           //if (!useWorldPos)
           //pos -= transform.position;
           
           if (placementBounds.heightIs2D)
               return nodeExport.Sample2D((float) pos.x, (float) pos.z);
           else
               return nodeExport.Sample3D((float) pos.x, (float) pos.y, (float) pos.z);
       }
       
       bool PlacementValidation(Vector3 pos, float v)
       {
           if (v <= 0) return false;

           bool heightPlacementCheck = plamentHandler.ChechPos(pos);
           if (!heightPlacementCheck)
               return false;

           return true;
       }
       
    }
}