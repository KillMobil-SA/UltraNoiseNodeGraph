using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NoiseUltra.Nodes
{
    [Serializable]
    public class PreviewImage
    {
        private const int MaxZoom = 1000;
        private const int ImageSize = 256;
        private const int Width = ImageSize;
        private const int Height = ImageSize;
        private float MaxPixel { get; }

        [SerializeField, ReadOnly] 
        private Bound bounds = new Bound();
        
        [SerializeField, Range(0, MaxZoom)] 
        private float zoom = 200; //need to solve the auto update

        [SerializeField, PreviewField(ImageSize)]
        private Texture2D sourceTexture;

        public PreviewImage()
        {
            MaxPixel = ImageSize - 1;
            bounds.ResetBounds();
        }

        public void Update(Func<float, float, float> sampleFunction)
        {
            if (sampleFunction == null)
                return;

            DeleteTexture(); // the trick of the tricks
            CreateTexture();
            bounds.ResetBounds();
            
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    var pixelX = x / MaxPixel;
                    var pixelY = y / MaxPixel;
                    var px = zoom * pixelX;
                    var py = zoom * pixelY;
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
            sourceTexture = new Texture2D(Width, Height, TextureFormat.RGB24, false, false);
        }
    }
}