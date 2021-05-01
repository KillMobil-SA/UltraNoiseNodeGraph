using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Terrains
{
    [RequireComponent(typeof(Terrain))]
    public abstract class TerrainTool : BaseTool
    {
        [SerializeField] protected ProgressBar progress = new ProgressBar();

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
        public void Stop()
        {
            _routineWrapper.StopCoroutine();
        }

        protected abstract IEnumerator Operation();
    }
}