using System;
using NoiseUltra.Output;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Terrains
{
    public partial class TerrainPainter
    {
        [Serializable]
        private class PaintItem
        {
            [SerializeField] private bool isActive = true;
            [SerializeField] private bool isOneMinus;
            [SerializeField] private bool isAnglePaint;
        
            [ShowIf("isAnglePaint")] 
            [SerializeField] private float angleDivide = 1;
        
            [ShowIf("isAnglePaint")] 
            [SerializeField] private AnimationCurve clifCurve = AnimationCurve.Linear(0, 0, 1, 1);
            [SerializeField] public int splatID;
            [SerializeField] private ExportNode sourceNode;


            public float GetSample(float x, float y, Terrain terrain, bool useWorldPos)
            {
                float sample = 0;

                if (!isActive) 
                    return 0;
            
                var xPos = x;
                var yPos = y;

                var terrainData = terrain.terrainData;
                var terrainPosition = terrain.transform.position;
                var heightMapResolution = terrainData.heightmapResolution;

                if (useWorldPos)
                {
                    xPos += terrainPosition.x;
                    yPos += terrainPosition.y;
                }
            
                sample = sourceNode != null ? sourceNode.GetSample(xPos, yPos) : 1;
            
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
}
