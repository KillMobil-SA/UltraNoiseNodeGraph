using System;
using NoiseUltra.Generators;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NoiseUltra.Nodes
{
    [Serializable]
    public sealed class PreviewImage
    {
        #region Members
        [ReadOnly]
        [SerializeField]
        private Bound bounds = new Bound();

        [OnValueChanged(nameof(Draw))]
        [SerializeField]
        private int size = NodeProprieties.DefaultGlobalZoom;

        [SerializeField]
        [PreviewField(NodeProprieties.DefaultPreviewSize)]
        private Texture2D sourceTexture;

        private NodeBase node;
        private Func<float, float, float> function;
        private int imageSize = NodeProprieties.DefaultPreviewSize;
        private float maxPixel;
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

        public void Draw()
        {
            Profiler.Start();
            if (function == null)
            {
                return;
            }

            DeleteTexture();
            ResetBounds();
            CreateTexture();
            bounds.Reset();

            for (var x = 0; x < imageSize; ++x)
            {
                for (var y = 0; y < imageSize; ++y)
                {
                    var pixelX = x / maxPixel;
                    var pixelY = y / maxPixel;
                    var px = size * pixelX;
                    var py = size * pixelY;
                    var sample = function(px, py);
                    var pixelColor = new Color(sample, sample, sample, 1);
                    IdentifyBounds(sample);
                    sourceTexture.SetPixel(x, y, pixelColor);
                }
            }

            sourceTexture.Apply();
            Profiler.End(node.name);
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




