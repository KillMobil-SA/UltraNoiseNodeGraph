using System.Collections;
using System.Threading.Tasks;
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
            var position = transform.position;
            var relativeSize = GetRelativeSize();
            yield return Operate(resolution, position, relativeSize);
            GetTerrainData().SetHeights(0, 0, _heightMap);
            progress.Reset();
        } 

        private IEnumerator Operate(int resolution, Vector3 position, float relativeSize)
        {
            for (var x = 0; x < resolution; x++)
            {
                var xPos = x * relativeSize;
                for (var y = 0; y < resolution; y++)
                {
                    var yPos = y * relativeSize;
            
                    if (useWorldPos)
                    {
                        xPos += position.x;
                        yPos += position.z;
                    }
                    
                    _heightMap[y, x] = GetSample(xPos, yPos);
                    
                    if (!progress.TryProcess())
                    {
                        yield return progress.ResetIteration();
                    }
                }
            }
        }
    }
}