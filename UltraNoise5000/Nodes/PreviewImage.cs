using System;
using NoiseUltra.Generators;
using NoiseUltra.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NoiseUltra.Nodes
{
    [Serializable]
    public sealed class PreviewImage
    {
        #region Members
        
        [SerializeField , HideInInspector]
        private bool showPreviewImage = false;
        [Button, PropertyOrder(-1)]
        private void TogglePreview()
        {
            showPreviewImage = !showPreviewImage;
        }

        
        
        private TaskGroup m_TaskGroup;

        [SerializeField, InlineProperty, HideLabel , ShowIf (nameof(showPreviewImage))]
        private Bound bounds;

        [OnValueChanged(nameof(DrawAsync)) , ShowIf(nameof(showPreviewImage))]
        [SerializeField]
        private int size = NodeProprieties.DEFAULT_GLOBAL_ZOOM;

        [SerializeField , ShowIf(nameof(showPreviewImage))]
        [HideLabel,PreviewField(NodeProprieties.DEFAULT_PREVIEW_SIZE)]
        private Texture2D sourceTexture;

        private NodeBase m_Node;
        private Func<float, float, float> m_SampleFunction;
        private int m_ImageSize = NodeProprieties.DEFAULT_TEXTURE_SIZE;
        private float m_MAXPixel;
        private Color[] m_Pixels;
        public float Zoom => size;
        #endregion

        #region Initialization
        public PreviewImage()
        {
            ResetBounds();
        }

        public void Initialize(NodeBase nodeBase)
        {
            
            if (nodeBase == null)
            {
                Debug.Log("PreviewImage - Initialize nodeBase: Null");
                return;
            }

            bounds = new Bound(this);
            m_TaskGroup = new TaskGroup(OnCompleteTask);
            m_SampleFunction = nodeBase.GetSample;
            m_Node = nodeBase;
            
            NoiseNodeGraph nodeGraph = nodeBase.graph as NoiseNodeGraph;
            if (nodeGraph == null)
            {
                return;
            }

            int globalZoom = nodeGraph.GlobalZoom;
            SetZoom(globalZoom);
        }

        #endregion

        #region Public

        public void DrawAsync()
        {
            if (m_SampleFunction == null || !showPreviewImage)
            {
                return;
            }

            DeleteTexture();
            SetImageSize(NodeProprieties.DEFAULT_TEXTURE_SIZE);
            ResetBounds();
            CreateTexture();
            bounds.Reset();

            
            
            int totalColors = m_ImageSize * m_ImageSize;
            m_Pixels = new Color[totalColors];
            int index = 0;
            Profiler.Start();
            
            
            for (int x = 0; x < m_ImageSize; ++x)
            {
                float pixelX = x / m_MAXPixel;
                float px = size * pixelX;
                for (int y = 0; y < m_ImageSize; ++y)
                {
                    float pixelY = y / m_MAXPixel;
                    float py = size * pixelY;
                    SampleStepColor sampleStep = new SampleStepColor(px, py, index, ref m_Pixels);
                    void Action() => m_Node.ExecuteSampleAsync(sampleStep);
                    m_TaskGroup.AddTask(Action);
                    ++index;
                }
            }

            m_TaskGroup.ExecuteAll();
        }

        public void DrawSync()
        {
            if (m_SampleFunction == null)
            {
                return;
            }

            DeleteTexture();
            SetImageSize(NodeProprieties.DEFAULT_TEXTURE_SIZE);

            ResetBounds();
            CreateTexture();
            bounds.Reset();
            
            int totalColors = m_ImageSize * m_ImageSize;
            m_Pixels = new Color[totalColors];
            int index = 0;
            Profiler.Start();

            for (int x = 0; x < m_ImageSize; ++x)
            {
                float pixelX = x / m_MAXPixel;
                float px = size * pixelX;
                for (int y = 0; y < m_ImageSize; ++y)
                {
                    float pixelY = y / m_MAXPixel;
                    float py = size * pixelY;
                    float sample = m_Node.GetSample(px, py);
                    Color color = new Color(sample, sample, sample);
                    m_Pixels[index] = color;
                    ++index;
                }
            }

            OnCompleteTask();
        }

        private void OnCompleteTask()
        {
            sourceTexture.SetPixels(m_Pixels);
            sourceTexture.Apply();
            IdentifyBounds();
            Profiler.End();
        }

        public void DeleteTexture()
        {
            //Need to profile to be sure about this, but I think
            //the tex remains hanging in memory if we dont kill it
            Object.DestroyImmediate(sourceTexture);
            sourceTexture = null;
        }

        
        public Texture2D GetTexture()
        {
            return sourceTexture;
        }

        public void SetZoom(int globalZoom)
        {
            size = globalZoom;
        }

        public void SetImageSize(int newImageSize)
        {
            m_ImageSize = newImageSize;
        }

        public void ResetImageSize()
        {
            m_ImageSize = NodeProprieties.DEFAULT_TEXTURE_SIZE;
        }
        #endregion

        #region Private

        private void IdentifyBounds()
        {
            int length = m_Pixels.Length;
            for (int i = 0; i < length; i++)
            {
                Color color = m_Pixels[i];
                bounds.IdentifyBounds(color.r);
            }
        }

        private void ResetBounds()
        {
            m_MAXPixel = m_ImageSize - 1;
            bounds.Reset();
        }

        private void CreateTexture()
        {
            sourceTexture = new Texture2D(m_ImageSize, m_ImageSize, TextureFormat.RGB24, false, false);
        }
        #endregion
    }
}




