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
        [SerializeField] private PreviewImage previewImage = new PreviewImage();
        public float Resolution => previewImage.Resolution;

        protected override void Init()
        {
            previewImage.SetNode(this);
            SetDefaultZoom();
            if (previewImage.autoPreview)
            {
                Draw();
            }
        }
        
        
        [Button]
        public void Draw()
        {
            OnBeforeDrawPreview();
            previewImage.Update(GetSample);
        }

        protected virtual void OnBeforeDrawPreview()
        {
        }

        protected override void OnSelect()
        {
            Draw();
        }

        public abstract float GetSample(float x);
        public abstract float GetSample(float x, float y);
        public abstract float GetSample(float x, float y, float z);

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

        public void SetZoom(int globalZoom)
        {
            previewImage.SetZoom(globalZoom);
            Draw();
        }

        private void SetDefaultZoom()
        {
            var nodeGraph = graph as NoiseNodeGraph;
            if (nodeGraph == null) 
                return;
            var globalZoom = nodeGraph.GlobalZoom;
            previewImage.SetZoom(globalZoom);
        }
    }
}