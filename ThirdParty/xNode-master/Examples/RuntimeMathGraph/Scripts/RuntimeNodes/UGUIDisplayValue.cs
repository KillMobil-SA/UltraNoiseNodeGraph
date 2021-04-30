using UnityEngine.UI;
using XNode.Examples.MathNodes;

namespace XNode.Examples.RuntimeMathNodes
{
    public class UGUIDisplayValue : UGUIMathBaseNode
    {
        public Text label;

        private void Update()
        {
            var displayValue = node as DisplayValue;
            var obj = displayValue.GetInputValue<object>("input");
            if (obj != null) label.text = obj.ToString();
            else label.text = "n/a";
        }
    }
}