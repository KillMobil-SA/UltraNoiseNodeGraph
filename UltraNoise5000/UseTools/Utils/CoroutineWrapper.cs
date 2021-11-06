using System.Collections;
using UnityEngine;

namespace NoiseUltra.Tools
{
    public sealed class CoroutineWrapper
    {
        private readonly MonoBehaviour m_Handler;
        private readonly IEnumerator m_Coroutine;
        private Coroutine m_RuntimeRoutine;

        public CoroutineWrapper(MonoBehaviour handler, IEnumerator coroutine)
        {
            m_Handler = handler;
            m_Coroutine = coroutine;
        }

        public void StartCoroutine()
        {
            if (m_Handler != null && m_Coroutine != null)
            {
                m_RuntimeRoutine = m_Handler.StartCoroutine(m_Coroutine);
            }
        }

        public void StopCoroutine()
        {
            if (m_RuntimeRoutine != null)
            {
                m_Handler.StopCoroutine(m_RuntimeRoutine);
                m_RuntimeRoutine = null;
            }
        }
    }
}
