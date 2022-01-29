using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Unity.Mathematics;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace NoiseUltra.Tools.Placement
{
  

    [CreateAssetMenu(fileName = "UltraPlacementGreeneyObject", menuName = "KillMobil/UltraNoise/Greenery")]
    public class PlacementGreenery : PlacementSettings
    {
        [TitleGroup("Greenery Settings")]
        public GreeneryItem item;
        public Gradient gradientColor;
        public int copies = 1;


        public SpawnData PlaceSpawnPoint(Vector3 pos, float v, Transform parent)
        {
            
            Vector3 placementPos = GetPos(pos, v);
            Vector3 placementNormal = Vector3.up;
            Color placementColor = gradientColor.Evaluate(v);
            
            SpawnData newSpawnData = new SpawnData(placementPos ,placementNormal , placementColor );

            return newSpawnData;

        }
       
    }
}