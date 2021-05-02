using NoiseUltra.Nodes;
using NoiseUltra.Output;
using UnityEngine;

namespace NoiseUltra.Tools
{
    public abstract class BaseTool : MonoBehaviour
    {
        [SerializeField] protected ExportNode sourceNode;

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
            return !IsValid ? NodeProprieties.Invalid : sourceNode.GetSample(sampleX);
        }

        protected float GetSample(float sampleX, float sampleY)
        {
            return !IsValid ? NodeProprieties.Invalid : sourceNode.GetSample(sampleX, sampleY);
        }

        protected float GetSample(float sampleX, float sampleY, float sampleZ)
        {
            return !IsValid ? NodeProprieties.Invalid : sourceNode.GetSample(sampleX, sampleY, sampleZ);
        }
    }
}