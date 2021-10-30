using NoiseUltra.Output;
using NoiseUltra.Tasks;
using UnityEngine;

namespace NoiseUltra.Tools
{
    public abstract class BaseTool : MonoBehaviour
    {
        [SerializeField]
        protected ExportNode sourceNode;
        protected TaskGroup taskGroup;

        private void OnEnable()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            taskGroup = new TaskGroup(sourceNode, OnCompleteTask);
        }

        protected virtual void OnCompleteTask()
        {
            
        }

        protected float GetSample(float sampleX)
        {
            return sourceNode.GetSample(sampleX);
        }

        protected float GetSample(float sampleX, float sampleY)
        {
            return sourceNode.GetSample(sampleX, sampleY);
        }

        protected float GetSample(float sampleX, float sampleY, float sampleZ)
        {
            return sourceNode.GetSample(sampleX, sampleY, sampleZ);
        }
    }
}