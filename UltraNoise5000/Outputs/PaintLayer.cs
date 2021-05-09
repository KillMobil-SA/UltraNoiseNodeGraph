using System;
using NoiseUltra.Nodes;
using NoiseUltra.Tools.Terrains;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Output
{
    [Serializable]
    public class PaintLayer : NodeInputOutput
    {
        [SerializeField] private TerrainLayer terrainLayer;
        [SerializeField] private bool isActive = true;
        [SerializeField] private bool isOneMinus;
        [SerializeField] private bool isAnglePaint;

        [ShowIf("isAnglePaint")] [SerializeField]
        private float angleDivide = 1;

        [ShowIf("isAnglePaint")] [SerializeField]
        private AnimationCurve clifCurve = AnimationCurve.Linear(0, 0, 1, 1);

        public TerrainLayer TerrainLayer => terrainLayer;

        public float GetSample(float x, float y, TerrainData terrainData, bool useWorldPos)
        {
            float sample = GetSample(x, y);

            if (!isActive)
                return 0;

            var heightMapResolution = terrainData.heightmapResolution;

            if (isAnglePaint)
            {
                var x01 = x / terrainData.alphamapWidth;
                var y01 = y / terrainData.alphamapHeight;
                var steepness = terrainData.GetSteepness(x01, y01);

                var angleV =
                    clifCurve.Evaluate(
                        Mathf.Clamp01(steepness * steepness / (heightMapResolution / angleDivide)));
                sample *= angleV;
            }

            if (isOneMinus)
            {
                return 1 - sample;
            }

            return sample;
        }
    }
}