using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    public class UltraPlacementToolSphereMode: UltraPlacementBase
    {

        public override void PerformPlacement()
        {
            Initialize();
            
            int totalPlacementItems = placementList.Count;
            for (var i = 0; i < totalPlacementItems; i++)
            {
                PlacementSettings settings = placementList[i];

                if (!CheckSettignsCondition(settings))
                    continue;

                float spacing = settings.spacing;
                myPlacementBounds.SetSpace(spacing);

                int xAmount = (int) myPlacementBounds.xAmount;
                int yAmount = (int) myPlacementBounds.yAmount;
                int zAmount = (int) myPlacementBounds.zAmount;
                
                int index = 0;
                //Live preview needs to init here in order to have the correct array size based on spacing
                settings.InitLivePreview(myPlacementBounds.VolumeInt);

                
                
                List<Vector3> PlacementPoints = new List<Vector3>();
                
                int v = 0;
                for (int y = 0; y <= yAmount; y++) {
                    for (int x = 0; x <= xAmount; x++) {
                        PlacementPoints.Add(GetSphericalPoint(v++, x, y, 0 , spacing));
                    }
                    for (int z = 1; z <= zAmount; z++) {
                        PlacementPoints.Add(GetSphericalPoint(v++, xAmount, y, z, spacing));
                    }
                    for (int x = xAmount - 1; x >= 0; x--) {
                        PlacementPoints.Add(GetSphericalPoint(v++, x, y, zAmount, spacing));
                    }
                    for (int z = zAmount - 1; z > 0; z--) {
                        PlacementPoints.Add(GetSphericalPoint(v++, 0, y, z, spacing));
                    }
                }
        
                for (int z = 1; z < zAmount; z++) {
                    for (int x = 1; x < xAmount; x++) {
                        PlacementPoints.Add(GetSphericalPoint(v++, x, yAmount, z, spacing));
                    }
                }
                for (int z = 1; z < zAmount; z++) {
                    for (int x = 1; x < xAmount; x++) {
                        PlacementPoints.Add(GetSphericalPoint(v++, x, 0, z, spacing));
                    }
                }
                
                
                //for (var x = 0; x < xAmount; x++) {
                   // for (var y = 0; y < yAmount; y++) {
                       // for (var z = 0; z < zAmount; z++) {
                       for (int s = 0; s < PlacementPoints.Count; s++)
                       {
                           Vector3 placementPos = PlacementPoints[s];
                           Debug.DrawLine(placementPos, placementPos + Vector3.up * 0.1f, Color.green, 2);
                            PlaceItem(index, placementPos, settings);
                            index++;
                        }
                  //  }
               // }
            }
        }

        
        private Vector3 GetSphericalPoint (int i, int x, int y, int z , float spacing) {
            Vector3 v = new Vector3(x, y, z) * 2f / spacing - Vector3.one;
            float x2 = v.x * v.x;
            float y2 = v.y * v.y;
            float z2 = v.z * v.z;
            Vector3 s;
            s.x = v.x * Mathf.Sqrt(1f - y2 / 2f - z2 / 2f + y2 * z2 / 3f);
            s.y = v.y * Mathf.Sqrt(1f - x2 / 2f - z2 / 2f + x2 * z2 / 3f);
            s.z = v.z * Mathf.Sqrt(1f - x2 / 2f - y2 / 2f + x2 * y2 / 3f);

            //Debug.DrawLine(s, s + Vector3.up * 0.1f, Color.green, 2);
            return s;
        }
        


    }
}