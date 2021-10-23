using System;
using System.Threading;
using System.Threading.Tasks;
using NoiseUltra.Generators;
using Sirenix.OdinInspector;
using UnityEditor;
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
        private Color[] _colors;
        private Color[] _colorsAsync;
        private Task[] _tasks;
        private bool isCompleted;
        public float Resolution => size;
        #endregion

        #region Initialization
        public PreviewImage()
        {
            ResetBounds();
        }

        ~PreviewImage()
        {
            EditorApplication.update -= Update;
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

            EditorApplication.update += Update;

            var globalZoom = nodeGraph.GlobalZoom;
            SetZoom(globalZoom);
        }

        #endregion

        #region Public

        public void Draw()
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
            _colors = new Color[totalColors];
            _colorsAsync = new Color[totalColors];
            _tasks = new Task[totalColors];
            int index = 0;
            Profiler.Start();
            for (var x = 0; x < imageSize; ++x)
            {
                var pixelX = x / maxPixel;
                var px = size * pixelX;
                for (var y = 0; y < imageSize; ++y)
                {
                    var pixelY = y / maxPixel;
                    var py = size * pixelY;
                    // Work in progress
                    //var sample = function(px, py);
                    //IdentifyBounds(sample);
                    //var pixelColor = new Color(sample, sample, sample, 1);
                    //_colors[index] = pixelColor;
                    int index1 = index;
                    _tasks[index] = Task.Run(() => node.GetSampleAsync(px, py, index1, ref _colorsAsync, OnComplete));
                    ++index;
                }
            }

            //sourceTexture.SetPixels(_colors);
            //sourceTexture.Apply();
            Profiler.End("Sync");
            Task.WaitAll(_tasks);
        }

        private void OnComplete() => isCompleted = true;

        private void Complete()
        {
            sourceTexture.SetPixels(_colorsAsync);
            sourceTexture.Apply();
            Profiler.End("Async");
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

        public void Update()
        {
            if (isCompleted)
            {
                Complete();
                isCompleted = false;
            }
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




