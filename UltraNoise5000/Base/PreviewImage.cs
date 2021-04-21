using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NoiseUltra
{
    [Serializable]
    public class PreviewImage
    {
        private const int MaxZoom = 1000;
        private const int ImageSize = 256;
        private const int Width = ImageSize;
        private const int Height = ImageSize;
        private float MaxPixel { get; }

        [SerializeField, Range(0, MaxZoom)] 
        private float zoom = 200; //need to solve the auto update

        [SerializeField, PreviewField(ImageSize)]
        private Texture2D sourceTexture;

        public PreviewImage()
        {
            MaxPixel = ImageSize - 1;
        }

        public void Update(Func<float, float, float> sampleFunction)
        {
            if (sampleFunction == null)
                return;

            DeleteTexture(); // the trick of the tricks
            CreateTexture();

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    var pixelX = x / MaxPixel;
                    var pixelY = y / MaxPixel;
                    var px = zoom * pixelX;
                    var py = zoom * pixelY;
                    var color = sampleFunction(px, py);
                    var pixelColor = new Color(color, color, color, 1);
                    sourceTexture.SetPixel(x, y, pixelColor);
                }
            }

            sourceTexture.Apply();
        }

        private void DeleteTexture()
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