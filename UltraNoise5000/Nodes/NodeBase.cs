using System.Collections;
using NoiseUltra.Tools;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;


namespace NoiseUltra.Nodes
{
    /// <summary>
    ///     Class handles the image preview and basic node operations.
    /// </summary>
    [NodeWidth(NodeProprieties.NODE_WIDTH_PIXELS)]
    public abstract class NodeBase : Node
    {
        #region Members
        [SerializeField]
        protected PreviewImage previewImage = new PreviewImage();
        private EditorCoroutineWrapper m_EditorCoroutine;

        public float Zoom => previewImage.Zoom;
        
        #endregion

        #region Initialization
        protected override void Init()
        {
            m_EditorCoroutine = new EditorCoroutineWrapper( this, DrawAsyncInternal());
            previewImage.Initialize(this);
        }
        #endregion

        #region Public


        public abstract float GetSample(float x);
        public abstract float GetSample(float x, float y);
        public abstract float GetSample(float x, float y, float z);

        public virtual void ExecuteSampleAsync<T>(T sampleInfo) where T : BaseSampleStep
        {
            sampleInfo.Execute(GetSample);
        }

        [Button]
        public void DrawAsync()
        {
            m_EditorCoroutine.StopCoroutine();
            m_EditorCoroutine.SetCoroutine(DrawAsyncInternal());
            m_EditorCoroutine.StartCoroutine();
        }

        private IEnumerator DrawAsyncInternal()
        {
            yield return Wait.WAIT_ONE_SECOND;
            OnBeforeDrawPreview();
            previewImage.DrawAsync();
        }
        
 
        [Button]
        public void DrawSync()
        {
            OnBeforeDrawPreview();
            previewImage.DrawSync();
        }

        public void SetZoom(int globalZoom)
        {
            previewImage.SetZoom(globalZoom);
            DrawAsync();
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
            //DrawAsync();
        }
        
        protected bool IsConnected(NodeBase node)
        {
            NodePort port = GetPort(nameof(node));
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