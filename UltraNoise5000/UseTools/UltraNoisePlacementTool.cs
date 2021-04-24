using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;
using NoiseUltra.Nodes;
namespace NoiseUltra {
    public class UltraNoisePlacementTool : SerializedMonoBehaviour {

        [Title ("Noise Settings")]
        public int seed;
        public NodeBase nodeGraph;
        [Title("Placement Settings")] 
        public bool useWorldPos = true;
        public float spacing;
        public UltraPlacementItemBase plamentHandler;
        [Title ("Debug Settings")]
        [ReadOnly]
        public int itemCounter = 0;
        public bool showDebugInfo;
        void PreBuildPreparation () {
            itemCounter = 0;
            Random.InitState (seed);
        }

        [Button]
        public void GenerateObjects ()
        {
            ClearObjects();
            PerformGeneration (false);
            showDebugInfo = false;
        }

        [Button]
        public void ClearObjects () {
            plamentHandler.CleanObjects (this.transform);
        }


        public Vector3 DebugVal;
        
        void OnDrawGizmos () {
            
            
            if (plamentHandler == null || !showDebugInfo) return;
            PerformGeneration (true);
            DebugVal  =  myPlacementBounds.GetPosVector (transform.position.x, 1, transform.position.z);
            
        }

        void PerformGeneration (bool isDebug) {
            InitPlacement ();

            for (int x = 0; x < myPlacementBounds.xAmount; x++) {
                for (int y = 0; y < myPlacementBounds.yAmount; y++) {
                    for (int z = 0; z < myPlacementBounds.zAmount; z++) {
                        Vector3 pos = myPlacementBounds.GetPosVector (x, y, z);

                        float v = GetNoise (pos);

                        if (!PlacementValidation (pos, v)) continue;

                        if (isDebug) plamentHandler.DebugObject (pos, v);
                        else {
                            plamentHandler.PlaceObject (pos, v, this.transform);
                        }
                        itemCounter++;
                    }
                }
            }
        }

        void InitPlacement () {
            itemCounter = 0;
            Random.InitState (seed);
        }

        bool PlacementValidation (Vector3 pos, float v) {
            if (v <= 0) return false;

            bool heightPlacementCheck = plamentHandler.ChechPos (pos);
            if (!heightPlacementCheck)
                return false;

            return true;

        }

        float GetNoise (Vector3 pos)
        {
            if (!useWorldPos)
                pos -= transform.position;
            if (myPlacementBounds.heightIs2D)
                return nodeGraph.Sample2D ((float) pos.x, (float) pos.z);
            else
                 return nodeGraph.Sample3D ((float) pos.x, (float) pos.y, (float) pos.z);
            //
        }

        private PlacementBounds _myPlacementBounds;

        private PlacementBounds myPlacementBounds {
            get {
                if (_myPlacementBounds == null)
                    _myPlacementBounds = new PlacementBounds (this, placementAreaCollider);
                return _myPlacementBounds;
            }
            set { _myPlacementBounds = value; }
        }

        private Collider _placementAreaCollider;

        private Collider placementAreaCollider {
            get {
                if (_placementAreaCollider == null)
                    _placementAreaCollider = GetComponent<Collider> ();
                return _placementAreaCollider;
            }
            set { _placementAreaCollider = value; }
        }

    }
}