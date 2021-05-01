using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace NoiseUltra.Nodes
{
    /// <summary>
    ///     Class handles the image preview and basic node operations.
    /// </summary>
    [NodeWidth(NodeProprieties.NodeWidth)]
    public abstract class NodeBase : Node
    {
        [SerializeField] private PreviewImage previewImage;

        protected override void Init()
        {
            previewImage = new PreviewImage();
        }

        [Button]
        public void DrawPreview()
        {
            OnBeforeDrawPreview();
            previewImage.Update(Sample2D);
        }

        protected virtual void OnBeforeDrawPreview()
        {
        }

        public abstract float Sample1D(float x);
        public abstract float Sample2D(float x, float y);
        public abstract float Sample3D(float x, float y, float z);

        //-------------------------------------------------------------------

        public override object GetValue(NodePort port)
        {
            return this;
        }

        protected bool IsConnected(NodeBase node)
        {
            var port = GetPort(nameof(node));
            return port != null && port.IsConnected;
        }

        protected NodePort GetPort(NodeBase node)
        {
            return GetPort(nameof(node));
        }

        protected NodeBase GetInputNode(string nodeName, NodeBase fallback)
        {
            return GetInputValue(nodeName, fallback);
        }
    }
}