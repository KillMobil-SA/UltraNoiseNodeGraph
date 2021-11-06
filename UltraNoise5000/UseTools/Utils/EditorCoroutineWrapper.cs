using System.Collections;
using Unity.EditorCoroutines.Editor;

namespace NoiseUltra.Tools
{
    public sealed class EditorCoroutineWrapper
    {
        private readonly object m_Handler;
        private IEnumerator m_Coroutine;
        private EditorCoroutine m_Routine;

        public EditorCoroutineWrapper(object handler, IEnumerator coroutine)
        {
            m_Coroutine = coroutine;
            m_Handler = handler;
        }

        public void StartCoroutine()
        {
            if (m_Coroutine != null && m_Handler != null)
            {
                m_Routine = EditorCoroutineUtility.StartCoroutine(m_Coroutine, m_Handler);
            }
        }

        public void StopCoroutine()
        {
            if (m_Routine != null)
            {
                EditorCoroutineUtility.StopCoroutine(m_Routine);
                m_Routine = null;
            }
        }

        public void SetCoroutine(IEnumerator coroutine)
        {
            m_Coroutine = coroutine;
        }
    }
}