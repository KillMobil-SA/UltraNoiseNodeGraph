using System.Linq;
using UnityEngine;

namespace XNode.Examples.LogicToy
{
    [NodeWidth(140)]
    [NodeTint(70, 70, 100)]
    public class ToggleNode : LogicNode
    {
        [Input] [HideInInspector] public bool input;
        [Output] [HideInInspector] public bool output;
        public override bool led => output;

        protected override void OnInputChanged()
        {
            var newInput = GetPort("input").GetInputValues<bool>().Any(x => x);

            if (!input && newInput)
            {
                input = newInput;
                output = !output;
                SendSignal(GetPort("output"));
            }
            else if (input && !newInput)
            {
                input = newInput;
            }
        }

        public override object GetValue(NodePort port)
        {
            return output;
        }
    }
}