using NoiseUltra.Nodes;
using NoiseUltra.Output;
using UnityEngine;

namespace NoiseUltra.Tools
{
    public abstract class BaseTool : MonoBehaviour
    {
        [SerializeField]
        protected ExportNode sourceNode;

        private bool IsValid => sourceNode != null;
        protected bool IsInitialized { get; private set; }

        private void OnEnable()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            if (IsInitialized)
                return;
            IsInitialized = true;
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