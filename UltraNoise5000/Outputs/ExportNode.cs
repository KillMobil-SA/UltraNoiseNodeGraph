﻿using UnityEngine;
using Sirenix.OdinInspector;
using XNode;

namespace NoiseUltra.Output
{
    [NodeTint(Nodes.NodeProprieties.NodeTintBlue)]
    public class ExportNode : Nodes.NodeInputOutput
    {
        [SerializeField, OnValueChanged(nameof(UpdateNoiseName))]
        private string nodeTitle;


        //Todo Is this a correct?? @ycaro
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