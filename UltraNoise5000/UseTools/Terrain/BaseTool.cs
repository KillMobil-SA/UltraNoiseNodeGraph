using NoiseUltra.Nodes;
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
        private bool IsValid => sourceNode != null;

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
            return IsValid ? sourceNode.GetSample(sampleX) : NodeProprieties.InvalidValue;
        }

        protected float GetSample(float sampleX, float sampleY)
        {
            return IsValid ? sourceNode.GetSample(sampleX, sampleY) : NodeProprieties.InvalidValue;
        }

        protected float GetSample(float sampleX, float sampleY, float sampleZ)
        {
            return IsValid ? sourceNode.GetSample(sampleX, sampleY, sampleZ) : NodeProprieties.InvalidValue;
        }
    }
}