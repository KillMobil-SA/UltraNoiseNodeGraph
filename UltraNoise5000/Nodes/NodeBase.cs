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
        #region Members
        public PreviewImage previewImage = new PreviewImage();
        public float Resolution => previewImage.Resolution;
        #endregion

        #region Initialization
        protected override void Init()
        {
            previewImage.Initialize(this);
        }
        #endregion

        #region Public
        public abstract float GetSample(float x);
        public abstract float GetSample(float x, float y);
        public abstract float GetSample(float x, float y, float z);

        [Button]
        public void Draw()
        {
            OnBeforeDrawPreview();
            previewImage.Update(GetSample);
        }

        public void SetZoom(int globalZoom)
        {
            previewImage.SetZoom(globalZoom);
            Draw();
        }

        public override object GetValue(NodePort port)
        {
            return this;
        }

        #endregion

        #region Protected
        protected virtual void OnBeforeDrawPreview()
        {
        }

        protected override void OnSelect()
        {
            Draw();
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
        #endregion
    }
}