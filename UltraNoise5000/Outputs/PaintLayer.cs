﻿using System;
using NoiseUltra.Nodes;
using NoiseUltra.Tools.Terrains;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Output
{
    [Serializable,NodeTint(NodeProprieties.NodeTintYellow)]
    public class PaintLayer : NodeInputOutput
    {
        [SerializeField,OnValueChanged(nameof(UpdateNoiseName))] private TerrainLayer terrainLayer;
        [SerializeField] private bool isAnglePaint;

        [ShowIf("isAnglePaint")] [SerializeField]
        private float angleDivide = 1;

        [ShowIf("isAnglePaint")] [SerializeField]
        private AnimationCurve clifCurve = AnimationCurve.Linear(0, 0, 1, 1);

        public TerrainLayer TerrainLayer => terrainLayer;

        public float GetSample(float x, float y , float xAlpha , float yAlpha, TerrainData terrainData, bool useWorldPos)
        {
            float sample = GetSample(x, y);
            var heightMapResolution = terrainData.heightmapResolution;

            if (isAnglePaint)
            {
                var x01 = xAlpha / terrainData.alphamapWidth;
                var y01 = yAlpha / terrainData.alphamapHeight;
                var steepness = terrainData.GetSteepness(x01, y01);

                var angleV =
                    clifCurve.Evaluate(
                        Mathf.Clamp01(
                            steepness * steepness / (heightMapResolution / angleDivide)
                          )
                        );
                sample *= angleV;
            }

            return sample;
        }
        
        private void UpdateNoiseName()
        {
            name = terrainLayer.name;
        }
    }
}