using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Terrains
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

        private Cache m_Cache;

        [SerializeField]
        protected ProgressBar progress = new ProgressBar();
        
        [SerializeField]
        protected bool useWorldCordinates;
        
        private CoroutineWrapper m_RoutineWrapper;
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

        [Button]
        public void ApplySync()
        {
            Initialize();
            m_RoutineWrapper = new CoroutineWrapper(this, Operation());
            m_RoutineWrapper.StartCoroutine();
        }

        protected virtual void OnBeforeApplyAsync()
        {
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
        public void StopSync()
        {
            m_RoutineWrapper.StopCoroutine();
        }

        [Button]
        public void ApplyAsync()
        {
            Initialize();
            OnBeforeApplyAsync();
            taskGroup.ExecuteAll();
        }

        private TerrainData GetTerrainDataInternal() => terrain.terrainData;
        private int GetHeightMapResolutionInternal() => GetTerrainDataInternal().heightmapResolution;
        private int GetAlphaMapResolutionInternal() => GetTerrainDataInternal().alphamapResolution;
        private Vector3 GetTerrainSizeInternal() => GetTerrainDataInternal().size;
        private float GetRelativeSizeInternal() => GetTerrainSizeInternal().x / GetHeightMapResolutionInternal();
        private float GetRelativeAlphaMapSizeInternal() => GetTerrainSizeInternal().x / GetAlphaMapResolutionInternal();


        protected abstract IEnumerator Operation();
        protected TerrainData GetTerrainData() => m_Cache.terrainData;
        protected int GetHeightMapResolution() => m_Cache.heightMapResolution;
        protected int GetAlphaMapResolution() => m_Cache.aplhaMapResolution;
        protected Vector3 GetTerrainSize() => m_Cache.terrainSize;
        protected float GetRelativeSize() => m_Cache.relativeSize;
        protected float GetRelativeAlphaMapSize() => m_Cache.relativeAlphaSize;
    }
}