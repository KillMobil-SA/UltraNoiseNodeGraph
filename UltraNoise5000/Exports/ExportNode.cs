using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Output
{
    [NodeTint(NodeColor.EXPORT_NODE)]
    public class ExportNode : NodeInputOutput
    {
        [SerializeField] [OnValueChanged(nameof(UpdateNoiseName))]
        private string nodeTitle;

        protected override void Init()
        {
            base.Init();
            nodeTitle = name;
        }

        private void UpdateNoiseName()
        {
            name = nodeTitle;
        }
    }
}