using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEngine;

namespace NoiseUltra.Tools.Terrains
{
    public class CoroutineWrapper
    {
        private MonoBehaviour Handler { get; }
        private IEnumerator Coroutine { get; }

#if UNITY_EDITOR
        private EditorCoroutine _editorRoutine;
#else
        private Coroutine _runtimeRoutine;
#endif

        public CoroutineWrapper(MonoBehaviour handler, IEnumerator coroutine)
        {
            Handler = handler;
            Coroutine = coroutine;
        }

        public void StartCoroutine()
        {
#if UNITY_EDITOR
            EditorCoroutineUtility.StartCoroutine(Coroutine, this);
#else
            _runtimeRoutine = Handler.StartCoroutine(Coroutine);
#endif
        }

        public void StopCoroutine()
        {
#if UNITY_EDITOR
            EditorCoroutineUtility.StopCoroutine(_editorRoutine);
#else
            Handler.StopCoroutine(_runtimeRoutine);
#endif
        }
    }
}