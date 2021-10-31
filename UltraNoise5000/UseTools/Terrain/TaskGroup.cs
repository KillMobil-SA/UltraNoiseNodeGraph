using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoiseUltra.Tasks
{
    public sealed class TaskGroup
    {
        private readonly Action m_OnComplete;
        private readonly List<Task> m_Tasks;
        
        public TaskGroup(Action callback)
        {
            m_OnComplete = callback;
            m_Tasks = new List<Task>();
        }

        public void AddTask(Action action)
        {
            if (action == null)
            {
                return;
            }

            Task task = Task.Run(action);
            m_Tasks.Add(task);
        }

        public void ExecuteAll()
        {
            Task.WaitAll(m_Tasks.ToArray());
            m_OnComplete.Invoke();
        }

        public void Clear() => m_Tasks.Clear();
    }
}