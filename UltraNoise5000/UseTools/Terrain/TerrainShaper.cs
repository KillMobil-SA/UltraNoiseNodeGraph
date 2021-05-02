using System.Collections;
using System.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Terrains
{
    public class TerrainShaper : TerrainTool
    {
        private float[,] _heightMap;
     
        protected override IEnumerator Operation()
        {
            var resolution = GetHeightMapResolution();
            _heightMap = new float[resolution, resolution];
            progress.SetSize(resolution * resolution);
            var interationsCount = 0;
            var position = transform.position;
            
            for (var x = 0; x < resolution; x++)
            {
                for (var y = 0; y < resolution; y++)
                {
                    var xPos = x * GetRelativeSize();
                    var yPos = y * GetRelativeSize();
            
                    if (useWorldPos)
                    {
                        xPos += position.x;
                        yPos += position.z;
                    }
            
                    _heightMap[y, x] = GetSample(xPos, yPos);
                    progress.Increment();
            
                    interationsCount++;
                    if (interationsCount > progress.iterationsPerFrame)
                    {
                        interationsCount = 0;
                        yield return null;
                    }
                }
            }
            
            GetTerrainData().SetHeights(0, 0, _heightMap);
            progress.Reset();
        }
    }
}