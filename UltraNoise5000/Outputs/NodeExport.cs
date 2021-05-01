using NoiseUltra.Nodes;
using UnityEngine;
using Sirenix.OdinInspector;
using XNode;

namespace NoiseUltra.Output
{
    [NodeTintAttribute(NodeProprieties.NodeTintBlue)]
    public class NodeExport : NodeInputOutput
    {
        [SerializeField, OnValueChanged(nameof(UpdateNoiseName))]
        private string nodeTitle;
        
        protected override void Init()
        {
            base.Init();
            nodeTitle = this.name;
        }

        void UpdateNoiseName()
        {
            this.name = nodeTitle;
        }
    }
}