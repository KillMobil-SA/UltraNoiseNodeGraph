using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NoiseUltra.Nodes
{
    [Serializable]
    public class PreviewImage
    {
        private const int previewSise = 256;
        private int ImageSize = 256;
        public bool autoPreview;
        
        [SerializeField, ReadOnly] private Bound bounds = new Bound();

        [SerializeField, OnValueChanged(nameof(Draw))] private int size = 200;

        [SerializeField, PreviewField(previewSise)]
        private Texture2D sourceTexture;

        private NodeBase Node { get; set; }

        private float MaxPixel { get; set; }
        public float Resolution => size;
        
        public PreviewImage()
        {
            ResetBounds();
        }

        void ResetBounds()
        {
            MaxPixel = ImageSize - 1;
            bounds.ResetBounds();
        }
        


        public void Update(Func<float, float, float> sampleFunction)
        {
            if (sampleFunction == null)
                return;
            DeleteTexture(); // the trick of the tricks
            ResetBounds();
            CreateTexture();
            bounds.ResetBounds();

            for (var x = 0; x < ImageSize; x++)
            {
                for (var y = 0; y < ImageSize; y++)
                {
                    var pixelX = x / MaxPixel;
                    var pixelY = y / MaxPixel;
                    var px = size * pixelX;
                    var py = size * pixelY;
                    var sample = sampleFunction(px, py);
                    var pixelColor = new Color(sample, sample, sample, 1);
                    IdentifyBounds(sample);
                    sourceTexture.SetPixel(x, y, pixelColor);
                }
            }

            sourceTexture.Apply();
        }

        private float IdentifyBounds(float sample)
        {
            sample = Mathf.Clamp01(sample);
            bounds.max = Mathf.Max(bounds.max, sample);
            bounds.min = Mathf.Min(bounds.min, sample);
            return sample;
        }

        public void DeleteTexture()
        {
            //Need to profile to be sure about this, but I think
            //the tex remains hanging in memory if we dont kill it
            Object.DestroyImmediate(sourceTexture);
            sourceTexture = null;
        }

        private void CreateTexture()
        {
            sourceTexture = new Texture2D(ImageSize, ImageSize, TextureFormat.RGB24, false, false);
        }

        public Texture2D GetTexture()
        {
            return sourceTexture;
        }
        
        private void Draw()
        {
            if(Node)
                Node.Draw();
        }

        public void SetNode(NodeBase nodeBase)
        {
            Node = nodeBase;
        }

        public void SetZoom(int globalZoom)
        {
            size = globalZoom;
        }

        public void SetImageSize(int newImageSize)
        {
            ImageSize = newImageSize;
        }

        public void ResetImageSize()
        {
            ImageSize = previewSise;
        }
    }
}




