using System;
using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Output
{
    [Serializable,NodeTint(NodeColor.YELLOW)]
    public class PaintLayer : NodeInputOutput
    {
        [SerializeField]
        [OnValueChanged(nameof(UpdateNoiseName))]
        private TerrainLayer terrainLayer;
        
        [SerializeField]
        private bool isAnglePaint;
        public bool IsAnglePaint => isAnglePaint;

        [SerializeField]
        [ShowIf(nameof(isAnglePaint))]
        private float angleDivide = 1;

        [SerializeField]
        [ShowIf(nameof(isAnglePaint))]
        private AnimationCurve clifCurve = AnimationCurve.Linear(0, 0, 1, 1);

        public TerrainLayer TerrainLayer => terrainLayer;

        public float EvaluateCliff(float steepness, float resolution)
        {
            float value = steepness * steepness / (resolution / angleDivide);
            return clifCurve.Evaluate(Mathf.Clamp01(value));
        }

        public float GetSample(float x, float y , float xAlpha , float yAlpha, TerrainData terrainData, bool useWorldPos)
        {
            float sample = GetSample(x, y);

            if (isAnglePaint)
            {
                int heightMapResolution = terrainData.heightmapResolution;
                float x01 = xAlpha / terrainData.alphamapWidth;
                float y01 = yAlpha / terrainData.alphamapHeight;
                float steepness = terrainData.GetSteepness(x01, y01);
                float value = steepness * steepness / (heightMapResolution / angleDivide);
                float clampValue = Mathf.Clamp01(value);
                float angleV = clifCurve.Evaluate(clampValue);
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