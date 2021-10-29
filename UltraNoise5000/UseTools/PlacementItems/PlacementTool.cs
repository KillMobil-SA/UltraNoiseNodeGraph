using System.Collections.Generic;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    public class PlacementTool : MonoBehaviour
    {
        
        
        
        [SerializeField] [TableList()]
        private List<PlacementItem> generatorItems = new List<PlacementItem>();

        [SerializeField] 
        private bool showDebugInfo;
        [SerializeField]
        public bool useWorldCordinates = true;

        [Header("Modifications")] [SerializeField]
        private bool cordinatesAbs = false;


        
        
        private PlacementBounds _myPlacementBounds;

        private Collider _placementAreaCollider;

        private PlacementBounds myPlacementBounds
        {
            get
            {
                if (_myPlacementBounds == null)
                    _myPlacementBounds = new PlacementBounds(this, placementAreaCollider);
                return _myPlacementBounds;
            }
        }

        private Collider placementAreaCollider
        {
            get
            {
                if (_placementAreaCollider == null)
                    _placementAreaCollider = GetComponent<Collider>();
                return _placementAreaCollider;
            }
        }

        private void OnDrawGizmos()
        {
            if (!showDebugInfo) 
                return;
            PerformPlacement(true);
        }

        [Button]
        public void GenerateObjects()
        {
            ClearObjects();
            PerformPlacement(false);
            showDebugInfo = false;
        }

        [Button]
        public void ClearObjects()
        {
            for (var i = 0; i < generatorItems.Count; i++) generatorItems[i].plamentHandler.CleanObjects(transform);
        }

        private void PerformPlacement(bool isDebug)
        {
            InitPlacement();
            for (var i = 0; i < generatorItems.Count; i++)
            {
                if (!generatorItems[i].active) 
                    continue;
                
                var spacing = generatorItems[i].spacing;
                myPlacementBounds.SetSpace(spacing);

                for (var x = 0; x < myPlacementBounds.xAmount; x++)
                for (var y = 0; y < myPlacementBounds.yAmount; y++)
                for (var z = 0; z < myPlacementBounds.zAmount; z++)
                {
                    var placementPos = new Vector3(x, y, z);
                    var placedItem = generatorItems[i];
                    placedItem.GenerateObject(myPlacementBounds, placementPos, isDebug, transform ,useWorldCordinates ,  cordinatesAbs);
                }
            }
        }

        private void InitPlacement()
        {
            for (var i = 0; i < generatorItems.Count; i++)
                generatorItems[i].plamentHandler.InitializeProperties();
        }

   
    }
}