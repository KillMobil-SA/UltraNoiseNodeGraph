using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    public class UltraPlacementTool : UltraPlacementBase
    {
        [TableList]
        public List<PlacementItem> placementItem = new List<PlacementItem>();

        [Button]
        public void BackUpList ()
        {
            for (int i = 0; i < placementItem.Count; i++)
                placementList.Add(placementItem[i].settings);
        }
        

        public override void PerformPlacement()
        {
            Initialize();
            
            int totalPlacementItems = placementList.Count;
            for (var i = 0; i < totalPlacementItems; i++)
            {
                PlacementSettings settings = placementList[i];

                if (!CheckSettignsCondition(settings))
                    continue;

                myPlacementBounds.SetSpace(settings.spacing);

                int xAmount = (int) myPlacementBounds.xAmount;
                int yAmount = (int) myPlacementBounds.yAmount;
                int zAmount = (int) myPlacementBounds.zAmount;
                
                int index = 0;
                //Live preview needs to init here in order to have the correct array size based on spacing
                settings.InitLivePreview(myPlacementBounds.VolumeInt);

                for (var x = 0; x < xAmount; x++) {
                    for (var y = 0; y < yAmount; y++) {
                        for (var z = 0; z < zAmount; z++) {
                            Vector3 placementPos = new Vector3(x, y, z);
                            PlaceItem(index, placementPos, settings);
                            index++;
                        }
                    }
                }
            }
        }



    }
}