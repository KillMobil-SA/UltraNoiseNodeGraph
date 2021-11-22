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
        private static readonly WaitForSecondsRealtime DRAW_DELAY = Wait.CreateWaitRealtime(0.75f);
        [SerializeField, InlineProperty, HideLabel]
        private PreviewImage _previewImage;
        protected PreviewImage previewImage
        {
            get
            {
                if (m_IsPreviewDirty)
                {
                    CreatePreviewImage();
                }

                return _previewImage;
            }
        }

        private bool m_IsPreviewDirty = true;

        private EditorCoroutineWrapper m_DrawEditorAsyncRoutine;
        private EditorCoroutineWrapper drawEditorAsyncRoutine
        {
            get
            {
                return m_DrawEditorAsyncRoutine ??= new EditorCoroutineWrapper(this, DrawAsyncInternal());
            }
        }

        public float Zoom => previewImage.Zoom;
        #endregion

        //#YWR: Do not include complex code in this initialization. It tends to slow down the editor
        //the more nodes you have within the project. Go for lazy instantiation or dirty flag instead.
        protected override void Init()
        {
            m_IsPreviewDirty = true;
        }
        
        
        

        #region Public
        public abstract float GetSample(float x);
        public abstract float GetSample(float x, float y);
        public abstract float GetSample(float x, float y, float z);

        public virtual void ExecuteSampleAsync<T>(T sampleInfo) where T : BaseSampleStep
        {
            sampleInfo.Execute(GetSample);
        }

        private bool showPreview => previewImage.showPreviewImage;
        
        [Button ("Draw Preview") , ShowIf(nameof(showPreview))]
        public void DrawAsync()
        {
            drawEditorAsyncRoutine.StopCoroutine();
            DRAW_DELAY.Reset();
            drawEditorAsyncRoutine.SetCoroutine(DrawAsyncInternal());
            drawEditorAsyncRoutine.StartCoroutine();
        }

        
        protected void DrawSync()
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

        #region private
        private IEnumerator DrawAsyncInternal()
        {
            yield return DRAW_DELAY;
            OnBeforeDrawPreview();
            previewImage.DrawAsync();
        }

        private void CreatePreviewImage()
        {
            _previewImage.Initialize(this);
            m_IsPreviewDirty = false;
        }
        #endregion

        #region Protected
        protected virtual void OnBeforeDrawPreview()
        {
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