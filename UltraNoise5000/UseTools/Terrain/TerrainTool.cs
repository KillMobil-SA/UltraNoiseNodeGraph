using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Terrains
{
    [RequireComponent(typeof(Terrain))]
    public abstract class TerrainTool : BaseTool
    {
        [SerializeField] protected ProgressBar progress = new ProgressBar();
        [SerializeField] protected bool useWorldPos;
        private CoroutineWrapper _routineWrapper;
        protected Terrain terrain;

        protected override void Initialize()
        {
            base.Initialize();
            terrain = GetComponent<Terrain>();
        }

        [Button]
        public void Apply()
        {
            if (!IsInitialized)
                Initialize();
            _routineWrapper = new CoroutineWrapper(this, Operation());
            _routineWrapper.StartCoroutine();
        }

        [Button]
        private void MatchSize()
        {
            var terrainData = terrain.terrainData;
            var currentSize = terrainData.size;
            var sizeX = sourceNode.Resolution;
            var sizeY = currentSize.y;
            var sizeZ = sourceNode.Resolution;
            var size = new Vector3(sizeX, sizeY, sizeZ);
            terrainData.size = size;
        }

        [Button]
        public void Stop()
        {
            _routineWrapper.StopCoroutine();
        }

        protected abstract IEnumerator Operation();
        
        protected TerrainData GetTerrainData() => terrain.terrainData;
        protected int GetHeightMapResolution () =>  GetTerrainData().heightmapResolution;
        protected int GetAlphaMapResolution () =>  GetTerrainData().alphamapResolution;
        protected Vector3 GetTerrainSize() => GetTerrainData().size;
        protected float GetRelativeSize() => GetTerrainSize().x / GetHeightMapResolution();
        protected float GetRelativeAlphaMapSize() => GetTerrainSize().x / GetAlphaMapResolution();
        protected Vector3 GetTerrainPosition() => transform.position;
        protected Transform GetTerrainTransform() => terrain.transform;
    }
}