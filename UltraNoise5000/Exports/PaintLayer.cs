using System;
using NoiseUltra.Nodes;
using NoiseUltra.Tools.Terrains;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Output
{
    [Serializable,NodeTint(NodeColor.Yellow)]
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