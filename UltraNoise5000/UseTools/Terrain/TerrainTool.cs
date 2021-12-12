using System.Collections;
using NoiseUltra.Tools.Terrains;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools
{
    [RequireComponent(typeof(Terrain))]
    public abstract class TerrainTool : BaseTool
    {
        public struct Cache
        {
            public Transform transform;
            public int heightMapResolution;
            public int aplhaMapResolution;
            public TerrainData terrainData;
            public Vector3 terrainSize;
            public float relativeSize;
            public float relativeAlphaSize;
        }

        private CoroutineWrapper m_RoutineWrapper;
        private Cache m_Cache;

        [SerializeField]
        protected ProgressBar progress = new ProgressBar();
        
        [SerializeField]
        protected bool useWorldCordinates;
        
        protected Terrain terrain;

        protected override void Initialize()
        {
            base.Initialize();
            terrain = GetComponent<Terrain>();
            CreateCache();
        }

        private void CreateCache()
        {
            m_Cache = new Cache()
            {
                terrainData = GetTerrainDataInternal(),
                aplhaMapResolution = GetAlphaMapResolutionInternal(),
                heightMapResolution = GetHeightMapResolutionInternal(),
                relativeAlphaSize = GetRelativeAlphaMapSizeInternal(),
                relativeSize = GetRelativeSizeInternal(),
                terrainSize = GetTerrainSizeInternal(),
                transform = terrain.transform
            };
        }

        [Button ("Render")]
        public void ApplySync()
        {
            Initialize();
            m_RoutineWrapper = new CoroutineWrapper(this, ExecuteSync());
            m_RoutineWrapper.StartCoroutine();
        }

        protected virtual void OnBeforeApplyAsync()
        {
        }

        //[Button]
        public void StopSync()
        {
            m_RoutineWrapper.StopCoroutine();
        }

        [Button ("Preview")]
        public void ApplyAsync()
        {
            Initialize();
            OnBeforeApplyAsync();
            taskGroup.ExecuteAll();
        }

        [Button]
        private void MatchSize()
        {
            TerrainData terrainData = terrain.terrainData;
            Vector3 currentSize = terrainData.size;
            float sizeX = sourceNode.Zoom;
            float sizeY = currentSize.y;
            float sizeZ = sourceNode.Zoom;
            Vector3 size = new Vector3(sizeX, sizeY, sizeZ);
            terrainData.size = size;
        }

        protected abstract IEnumerator ExecuteSync();
        protected TerrainData GetTerrainData() => m_Cache.terrainData;
        protected int GetHeightMapResolution() => m_Cache.heightMapResolution;
        protected float GetRelativeSize() => m_Cache.relativeSize;
        

        #region Internal
        private TerrainData GetTerrainDataInternal() => terrain.terrainData;
        private int GetHeightMapResolutionInternal() => GetTerrainDataInternal().heightmapResolution;
        private int GetAlphaMapResolutionInternal() => GetTerrainDataInternal().alphamapResolution;
        private Vector3 GetTerrainSizeInternal() => GetTerrainDataInternal().size;
        private float GetRelativeSizeInternal() => GetTerrainSizeInternal().x / GetHeightMapResolutionInternal();
        private float GetRelativeAlphaMapSizeInternal() => GetTerrainSizeInternal().x / GetAlphaMapResolutionInternal();
        #endregion

    }
}