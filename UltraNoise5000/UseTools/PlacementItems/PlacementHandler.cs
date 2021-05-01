using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;
using NoiseUltra.Output;

namespace NoiseUltra.Tools.Placement
{
    public class PlacementHandler : MonoBehaviour
    {

        public List<PlacementItem> generatorItems = new List<PlacementItem>();
        
        [Title("Noise Settings")] public int seed;
     [Title("Debug Settings")] [ReadOnly] public int itemCounter = 0;
        public bool showDebugInfo;

        void PreBuildPreparation()
        {
            itemCounter = 0;
            Random.InitState(seed);
        }

        [Button]
        public void GenerateObjects()
        {
            ClearObjects();
            PerformGeneration(false);
            showDebugInfo = false;
        }

        [Button]
        public void ClearObjects()
        {
            for (int i = 0; i < generatorItems.Count; i++)
            {
                generatorItems[i].plamentHandler.CleanObjects(this.transform);
            }
            
        }


        public Vector3 DebugVal;

        void OnDrawGizmos()
        {
            if (!showDebugInfo) return;
            PerformGeneration(true);
            DebugVal = myPlacementBounds.GetPosVector(transform.position.x, 1, transform.position.z);
        }

        void PerformGeneration(bool isDebug)
        {
            InitPlacement();
            for (int i = 0; i < generatorItems.Count; i++)
            {
                if (!generatorItems[i].active) continue;
                myPlacementBounds.SetSpace(generatorItems[i].spacing);
                
                for (int x = 0; x < myPlacementBounds.xAmount; x++)
                {
                    for (int y = 0; y < myPlacementBounds.yAmount; y++)
                    {
                        for (int z = 0; z < myPlacementBounds.zAmount; z++)
                        {
                            Vector3 placementPos = new Vector3(x, y, z);
                            bool placementSuccess = generatorItems[i].GenerateObject(myPlacementBounds, placementPos, isDebug, this.transform);
                            if (placementSuccess)
                                itemCounter++;
                        }
                    }
                }
            }
        }
        

        void InitPlacement()
        {
            itemCounter = 0;
            Random.InitState(seed);
        }

        private PlacementBounds _myPlacementBounds;
        private PlacementBounds myPlacementBounds
        {
            get
            {
                if (_myPlacementBounds == null)
                    _myPlacementBounds = new PlacementBounds(this, placementAreaCollider);
                return _myPlacementBounds;
            }
            set { _myPlacementBounds = value; }
        }

        private Collider _placementAreaCollider;
        private Collider placementAreaCollider
        {
            get
            {
                if (_placementAreaCollider == null)
                    _placementAreaCollider = GetComponent<Collider>();
                return _placementAreaCollider;
            }
            set { _placementAreaCollider = value; }
        }
    }
}