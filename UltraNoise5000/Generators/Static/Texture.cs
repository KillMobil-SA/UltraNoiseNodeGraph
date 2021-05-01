﻿using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Generators.Static
{
    [NodeTint(NodeProprieties.NodeTintGreen)]
    public class Texture : NodeOutput
    {
        [SerializeField] private Texture2D texture2d;

        [SerializeField] private float scale = 1;

        [SerializeField] private Vector2 offSet = new Vector2(0, 0);

        [SerializeField] private Rect textureRect;

        private bool IsValid => texture2d != null;

        [Button]
        public void UpdateTexture()
        {
            textureRect = new Rect(0, 0, texture2d.width, texture2d.height);
            DrawPreview();
        }

        protected override void Init()
        {
            base.Init();
            DrawPreview();
        }

        public override float Sample1D(float x)
        {
            return 0;
        }

        public override float Sample2D(float x, float y)
        {
            var scaledPosition = GetScaledPosition(x, y);

            if (!IsValid || !IsValidPosition(scaledPosition))
                return 0;

            var px = Mathf.RoundToInt(scaledPosition.x);
            var py = Mathf.RoundToInt(scaledPosition.y);
            return texture2d.GetPixel(px, py).r;
        }

        private bool IsValidPosition(Vector2 scaledPosition)
        {
            return textureRect.Contains(scaledPosition);
        }

        public override float Sample3D(float x, float y, float z)
        {
            var scaledPosition = GetScaledPosition(x, z);

            if (!IsValid || !IsValidPosition(scaledPosition))
                return 0;

            var px = Mathf.RoundToInt(scaledPosition.x);
            var py = Mathf.RoundToInt(scaledPosition.y);
            return texture2d.GetPixel(px, py).r;
        }

        private Vector2 GetScaledPosition(float x, float y)
        {
            var posCord = new Vector2(x, y);
            return (posCord + offSet) * scale;
        }
    }
}