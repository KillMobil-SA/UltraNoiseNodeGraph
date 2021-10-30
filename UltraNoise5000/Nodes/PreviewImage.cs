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
        private TaskGroup m_TaskGroup;
        [ReadOnly]
        [SerializeField]
        private Bound bounds = new Bound();

        [OnValueChanged(nameof(DrawAsync))]
        [SerializeField]
        private int size = NodeProprieties.DefaultGlobalZoom;

        [SerializeField]
        [PreviewField(NodeProprieties.DefaultPreviewSize)]
        private Texture2D sourceTexture;

        private NodeBase node;
        private Func<float, float, float> function;
        
        private int imageSize = NodeProprieties.DefaultPreviewSize;
        private float maxPixel;
        private Color[] _colorsAsync;
        public float Resolution => size;
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
                return;
            }

            node = nodeBase;
            m_TaskGroup = new TaskGroup(node, OnCompleteTask);
            function = nodeBase.GetSample;
            var nodeGraph = nodeBase.graph as NoiseNodeGraph;
            if (nodeGraph == null)
            {
                return;
            }

            var globalZoom = nodeGraph.GlobalZoom;
            SetZoom(globalZoom);
        }

        #endregion

        #region Public

        public void DrawAsync()
        {
            if (function == null)
            {
                return;
            }

            DeleteTexture();
            ResetBounds();
            CreateTexture();
            bounds.Reset();

            int totalColors = imageSize * imageSize;
            _colorsAsync = new Color[totalColors];
            int index = 0;
            Profiler.Start();
            for (int x = 0; x < imageSize; ++x)
            {
                float pixelX = x / maxPixel;
                float px = size * pixelX;
                for (int y = 0; y < imageSize; ++y)
                {
                    float pixelY = y / maxPixel;
                    float py = size * pixelY;
                    SampleInfoColorAsync sampleInfo = new SampleInfoColorAsync(px, py, index, ref _colorsAsync);
                    m_TaskGroup.AddSampleInfo(sampleInfo);
                    ++index;
                }
            }

            m_TaskGroup.ExecuteAll();
        }

        public void DrawSync()
        {
            if (function == null)
            {
                return;
            }

            DeleteTexture();
            ResetBounds();
            CreateTexture();
            bounds.Reset();
            int totalColors = imageSize * imageSize;
            _colorsAsync = new Color[totalColors];
            int index = 0;
            for (int x = 0; x < imageSize; ++x)
            {
                float pixelX = x / maxPixel;
                float px = size * pixelX;
                for (int y = 0; y < imageSize; ++y)
                {
                    float pixelY = y / maxPixel;
                    float py = size * pixelY;
                    float sample = node.GetSample(px, py);
                    Color color = new Color(sample, sample, sample);
                    _colorsAsync[index] = color;
                    ++index;
                }
            }

            OnCompleteTask();
        }

        private void OnCompleteTask()
        {
            sourceTexture.SetPixels(_colorsAsync);
            sourceTexture.Apply();
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
            imageSize = newImageSize;
        }

        public void ResetImageSize()
        {
            imageSize = NodeProprieties.DefaultPreviewSize;
        }
        #endregion

        #region Private

        private void ResetBounds()
        {
            maxPixel = imageSize - 1;
            bounds.Reset();
        }

        private float IdentifyBounds(float sample)
        {
            sample = Mathf.Clamp01(sample);
            bounds.max = Mathf.Max(bounds.max, sample);
            bounds.min = Mathf.Min(bounds.min, sample);
            return sample;
        }

        private void CreateTexture()
        {
            sourceTexture = new Texture2D(imageSize, imageSize, TextureFormat.RGB24, false, false);
        }
        #endregion
    }
}




