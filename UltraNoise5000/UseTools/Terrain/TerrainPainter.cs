using System.Collections;
using UnityEngine;

namespace NoiseUltra.Tools.Terrains
{
    public partial class TerrainPainter : TerrainTool
    {
        [SerializeField] private PaintItem[] paintItems;
        
        protected override IEnumerator Operation()
        {
            var resolution = GetHeightMapResolution();
            var terrainData = GetTerrainData();
            var width = terrainData.alphamapWidth;
            var height = terrainData.alphamapHeight;
            var alphaLayers = terrainData.alphamapLayers;
            var splatmapData = new float[width, height, alphaLayers];
            var interationsCount = 0;
            progress.SetSize(resolution * resolution);

            for (var x = 0; x < terrainData.alphamapWidth; x++)
            {
                for (var y = 0; y < terrainData.alphamapHeight; y++)
                {
                    var splatWeights = new float[terrainData.alphamapLayers];

                    for (var i = 0; i < paintItems.Length; i++)
                    {
                        var xPos = x * GetRelativeSize();
                        var yPos = y * GetRelativeSize();
                        var current = paintItems[i];
                        var currentID = current.splatID;
                        var sample = current.GetSample(xPos, yPos, terrain, useWorldPos);
                        splatWeights[currentID] = sample;

                        if (i > 0)
                        {
                            float sum = 0;
                            var complementValue = 1 - sample;
                            for (var j = i - 1; j >= 0; j--)
                            {
                                var inner = paintItems[j];
                                var innerID = inner.splatID;
                                var prevVal = splatWeights[innerID];
                                sum += prevVal;
                            }

                            for (var j = i - 1; j >= 0; j--)
                            {
                                var inner = paintItems[j];
                                var innerID = inner.splatID;
                                var prevVal = splatWeights[innerID];
                                prevVal = prevVal / sum * complementValue;
                                splatWeights[innerID] = prevVal;
                            }
                        }
                    }

                    for (var a = 0; a < terrainData.alphamapLayers; a++)
                    {
                        splatmapData[y, x, a] = splatWeights[a];
                    }

                    progress.Increment();

                    interationsCount++;
                    if (interationsCount > progress.iterationsPerFrame)
                    {
                        interationsCount = 0;
                        yield return null;
                    }
                }
            }

            terrainData.SetAlphamaps(0, 0, splatmapData);
        }
    }
}