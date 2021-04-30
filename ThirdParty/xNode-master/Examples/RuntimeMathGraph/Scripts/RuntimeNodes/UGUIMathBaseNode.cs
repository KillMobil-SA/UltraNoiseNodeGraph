using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace XNode.Examples.RuntimeMathNodes
{
    public class UGUIMathBaseNode : MonoBehaviour, IDragHandler
    {
        [HideInInspector] public Node node;
        [HideInInspector] public RuntimeMathGraph graph;
        public Text header;

        private UGUIPort[] ports;

        public virtual void Start()
        {
            ports = GetComponentsInChildren<UGUIPort>();
            foreach (var port in ports) port.node = node;
            header.text = node.name;
            SetPosition(node.position);
        }

        private void LateUpdate()
        {
            foreach (var port in ports) port.UpdateConnectionTransforms();
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public virtual void UpdateGUI()
        {
        }

        public UGUIPort GetPort(string name)
        {
            for (var i = 0; i < ports.Length; i++)
                if (ports[i].name == name)
                    return ports[i];
            return null;
        }

        public void SetPosition(Vector2 pos)
        {
            pos.y = -pos.y;
            transform.localPosition = pos;
        }

        public void SetName(string name)
        {
            header.text = name;
        }
    }
}