using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEngine;

namespace NoiseUltra.Tools.Terrains
{
    public sealed class CoroutineWrapper
    {
        private readonly MonoBehaviour _handler;
        private readonly IEnumerator _coroutine;

#if UNITY_EDITOR
        private EditorCoroutine _editorRoutine;
#else
        private Coroutine _runtimeRoutine;
#endif

        public CoroutineWrapper(MonoBehaviour handler, IEnumerator coroutine)
        {
            _handler = handler;
            _coroutine = coroutine;
        }

        public void StartCoroutine()
        {
#if UNITY_EDITOR
            _editorRoutine = EditorCoroutineUtility.StartCoroutine(_coroutine, _handler);
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
