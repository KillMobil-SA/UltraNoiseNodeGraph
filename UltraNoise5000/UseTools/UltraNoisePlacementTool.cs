using NoiseUltra.Output;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra
{
    public class UltraNoisePlacementTool : MonoBehaviour
    {
        [Title("Noise Settings")] public int seed;

        public ExportNode nodeGraph;

        [Title("Placement Settings")] public bool useWorldPos = true;

        [Min(1)] public float spacing;

        public UltraPlacementItemBase plamentHandler;

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
            if (plamentHandler == null || !showDebugInfo) return;
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
            plamentHandler.CleanObjects(transform);
        }

        private void PerformGeneration(bool isDebug)
        {
            InitPlacement();

            for (var x = 0; x < myPlacementBounds.xAmount; x++)
            for (var y = 0; y < myPlacementBounds.yAmount; y++)
            for (var z = 0; z < myPlacementBounds.zAmount; z++)
            {
                var pos = myPlacementBounds.GetPosVector(x, y, z);

                var v = GetNoise(pos);

                if (!PlacementValidation(pos, v)) continue;

                if (isDebug) plamentHandler.DebugObject(pos, v);
                else
                    plamentHandler.PlaceObject(pos, v, transform);
                itemCounter++;
            }
        }

        private void InitPlacement()
        {
            itemCounter = 0;
            Random.InitState(seed);
        }

        private bool PlacementValidation(Vector3 pos, float v)
        {
            if (v <= 0) return false;

            var heightPlacementCheck = plamentHandler.ChechPos(pos);
            if (!heightPlacementCheck)
                return false;

            return true;
        }

        private float GetNoise(Vector3 pos)
        {
            if (!useWorldPos)
                pos -= transform.position;
            if (myPlacementBounds.heightIs2D)
                return nodeGraph.Sample2D(pos.x, pos.z);
            return nodeGraph.Sample3D(pos.x, pos.y, pos.z);
            //
        }
    }
}