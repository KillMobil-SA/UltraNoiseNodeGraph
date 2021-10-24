using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoiseUltra.Nodes;

namespace NoiseUltra.Tasks
{
    public sealed class TaskGroup
    {
        private readonly NodeBase m_SourceNode;
        private readonly Action m_OnComplete;
        private readonly List<Task> m_Tasks;
        
        public TaskGroup(NodeBase node, Action callback)
        {
            m_OnComplete = callback;
            m_SourceNode = node;
            m_Tasks = new List<Task>();
        }

        public void AddSampleInfo(BaseSampleInfo sampleInfo)
        {
            Task task = Task.Run(() => m_SourceNode.ExecuteSampleAsync(sampleInfo));
            m_Tasks.Add(task);
        }

        public void ExecuteAll()
        {
            Task.WaitAll(m_Tasks.ToArray());
            m_OnComplete.Invoke();
        }
    }
}