using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    public class PlacementHandler : MonoBehaviour
    {
        public List<PlacementItem> generatorItems = new List<PlacementItem>();

        [Title("Noise Settings")] public int seed;
        [Title("Debug Settings")] [ReadOnly] public int itemCounter;
        public bool showDebugInfo;


        public Vector3 DebugVal;

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
            set => _myPlacementBounds = value;
        }

        private Collider placementAreaCollider
        {
            get
            {
                if (_placementAreaCollider == null)
                    _placementAreaCollider = GetComponent<Collider>();
                return _placementAreaCollider;
            }
            set => _placementAreaCollider = value;
        }

        private void OnDrawGizmos()
        {
            if (!showDebugInfo) return;
            PerformGeneration(true);
            DebugVal = myPlacementBounds.GetPosVector(transform.position.x, 1, transform.position.z);
        }

        private void PreBuildPreparation()
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
            for (var i = 0; i < generatorItems.Count; i++) generatorItems[i].plamentHandler.CleanObjects(transform);
        }

        private void PerformGeneration(bool isDebug)
        {
            InitPlacement();
            for (var i = 0; i < generatorItems.Count; i++)
            {
                if (!generatorItems[i].active) continue;
                myPlacementBounds.SetSpace(generatorItems[i].spacing);

                for (var x = 0; x < myPlacementBounds.xAmount; x++)
                for (var y = 0; y < myPlacementBounds.yAmount; y++)
                for (var z = 0; z < myPlacementBounds.zAmount; z++)
                {
                    var placementPos = new Vector3(x, y, z);
                    var placementSuccess = generatorItems[i]
                        .GenerateObject(myPlacementBounds, placementPos, isDebug, transform);
                    if (placementSuccess)
                        itemCounter++;
                }
            }
        }


        private void InitPlacement()
        {
            itemCounter = 0;
            Random.InitState(seed);
        }
    }
}