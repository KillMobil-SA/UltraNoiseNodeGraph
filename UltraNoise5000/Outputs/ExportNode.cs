using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Output
{
    [NodeTint(NodeProprieties.NodeTintBlue)]
    public class ExportNode : NodeInputOutput
    {
        [SerializeField] [OnValueChanged(nameof(UpdateNoiseName))]
        private string nodeTitle;


        //Todo Is this a correct?? @ycaro
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