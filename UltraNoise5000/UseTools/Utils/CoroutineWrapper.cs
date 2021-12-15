using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using Unity.EditorCoroutines.Editor;
#endif

namespace NoiseUltra.Tools
{
    public sealed class CoroutineWrapper
    {
        private object m_Handler;
#if UNITY_EDITOR
        private EditorCoroutine m_Routine;
#endif
        private Coroutine m_RuntimeRoutine;
        private IEnumerator m_Coroutine;

        private bool IsPlaying => Application.isPlaying;
        
        public CoroutineWrapper(object handler, IEnumerator coroutine)
        {
            m_Coroutine = coroutine;
            m_Handler = handler;
        }

        public void StartCoroutine()
        {
            if (!IsPlaying)
            {
#if UNITY_EDITOR
                if (m_Coroutine != null && m_Handler != null)
                {
                    m_Routine = EditorCoroutineUtility.StartCoroutine(m_Coroutine, m_Handler);
                }
#endif
            }
            else
            {
                if (m_Handler == null || m_Coroutine == null)
                {
                    return;
                }

                MonoBehaviour handler = m_Handler as MonoBehaviour;
                if (handler != null)
                {
                    m_RuntimeRoutine = handler.StartCoroutine(m_Coroutine);
                }
            }
        }

        public void StopCoroutine()
        {
            if (IsPlaying)
            {
                if (m_RuntimeRoutine == null)
                {
                    return;
                }

                MonoBehaviour handler = m_Handler as MonoBehaviour;
                if (handler != null)
                {
                    handler.StopCoroutine(m_RuntimeRoutine);
                }
                m_RuntimeRoutine = null;
            }
            else
            {
#if UNITY_EDITOR
                if (m_Routine == null)
                {
                    return;
                }

                EditorCoroutineUtility.StopCoroutine(m_Routine);
                m_Routine = null;
#endif
            }
        }
        
        public void SetCoroutine(IEnumerator coroutine)
        {
            m_Coroutine = coroutine;
        }
    }
}
