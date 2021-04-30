using NoiseUltra.Output;
using Sirenix.OdinInspector;
using UnityEngine;
<<<<<<< HEAD:UltraNoise5000/UseTools/PlacementItems/PlacementGenerator.cs
using Random = UnityEngine.Random;
using NoiseUltra.Output;

namespace NoiseUltra.Tools.Placement {
    public class PlacementGenerator : MonoBehaviour {
=======
>>>>>>> f5ee208a90c9bc5a5c97c3c815672de79590a885:UltraNoise5000/UseTools/UltraNoisePlacementTool.cs

namespace NoiseUltra
{
    public class UltraNoisePlacementTool : MonoBehaviour
    {
        [Title("Noise Settings")] public int seed;

        public ExportNode nodeGraph;
<<<<<<< HEAD:UltraNoise5000/UseTools/PlacementItems/PlacementGenerator.cs
        [Title("Placement Settings")] 
        public bool useWorldPos = true;
        [Min(1)]
        public float spacing;
        public PlacementItemBase plamentHandler;
        [Title ("Debug Settings")]
        [ReadOnly]
        public int itemCounter = 0;
=======

        [Title("Placement Settings")] public bool useWorldPos = true;

        [Min(1)] public float spacing;

        public UltraPlacementItemBase plamentHandler;

        [Title("Debug Settings")] [ReadOnly] public int itemCounter;

>>>>>>> f5ee208a90c9bc5a5c97c3c815672de79590a885:UltraNoise5000/UseTools/UltraNoisePlacementTool.cs
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