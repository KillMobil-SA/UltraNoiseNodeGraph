using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Terrains
{
    public class TerrainShaper : TerrainTool
    {
        [SerializeField] private bool useWorldPos;

        [SerializeField] private int iterationsPerFrame = 1000;

        private float[,] _heightMap;

        [Button]
        public void LogTerrainData()
        {
            Debug.Log(string.Format("ter.terrainData.size {0}", terrain.terrainData.size));
            Debug.Log(string.Format("myTerrain.terrainData.alphamapLayers {0}", terrain.terrainData.alphamapLayers));
            Debug.Log(string.Format("ter.terrainData.heightmapResolution {0}",
                terrain.terrainData.heightmapResolution));
            Debug.Log(string.Format("ter.terrainData.alphamapWidth {0}", terrain.terrainData.alphamapWidth));
            Debug.Log(string.Format("ter.terrainData.alphamapHeight {0}", terrain.terrainData.alphamapHeight));
            Debug.Log(string.Format("ter.terrainData.detailWidth {0}", terrain.terrainData.detailWidth));
            Debug.Log(string.Format("ter.terrainData.detailHeight {0}", terrain.terrainData.detailHeight));
        }

        protected override IEnumerator Operation()
        {
            var terrainData = terrain.terrainData;
            var resolution = terrainData.heightmapResolution;
            _heightMap = new float[resolution, resolution];
            var sizeRelativity = terrainData.size.x / resolution;
            progress.SetSize(resolution * resolution);
            var count = 0;
            var position = transform.position;

            for (var x = 0; x < resolution; x++)
            for (var y = 0; y < resolution; y++)
            {
                var xPos = x * sizeRelativity;
                var yPos = y * sizeRelativity;

                if (useWorldPos)
                {
                    xPos += position.x;
                    yPos += position.z;
                }

                _heightMap[y, x] = GetSample(xPos, yPos);
                progress.Increment();

                count++;
                if (count > iterationsPerFrame)
                {
                    count = 0;
                    yield return null;
                }
            }

            terrainData.SetHeights(0, 0, _heightMap);
            progress.Reset();
        }
    }
}