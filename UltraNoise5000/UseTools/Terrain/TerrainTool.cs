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
            terrain.terrainData.size = Vector3.one * sourceNode.Resolution;
        }

        [Button]
        public void Stop()
        {
            _routineWrapper.StopCoroutine();
        }

        protected abstract IEnumerator Operation();
        
        protected TerrainData GetTerrainData() => terrain.terrainData;
        protected int GetHeightMapResolution () =>  GetTerrainData().heightmapResolution;
        protected Vector3 GetTerrainSize() => GetTerrainData().size;
        protected float GetRelativeSize() => GetTerrainSize().x / GetHeightMapResolution();
        protected Vector3 GetTerrainPosition() => transform.position;
        protected Transform GetTerrainTransform() => terrain.transform;
    }
}