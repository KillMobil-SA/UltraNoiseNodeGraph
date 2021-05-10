using System.Collections;
using NoiseUltra.Output;
using UnityEngine;

namespace NoiseUltra.Tools.Terrains
{
    public class TerrainPainter : TerrainTool
    {
        private TerrainLayerExportGroup ExportGroup => sourceNode as TerrainLayerExportGroup;
        
        protected override IEnumerator Operation()
        {
            var resolution = GetHeightMapResolution();
            var terrainData = GetTerrainData();
            var width = terrainData.alphamapWidth;
            var height = terrainData.alphamapHeight;
            terrainData.terrainLayers = GetTerrainLayers();
            var alphaLayers = terrainData.alphamapLayers;
            var splatmapData = new float[width, height, alphaLayers];
            var relativeSize = GetRelativeSize();
            var relativeAlphaMapSize = GetRelativeAlphaMapSize();
            var paintLayers = GetPaintLayers();
            var totalLayers = paintLayers.Length;
            var splatWeights = new float[alphaLayers]; //create splatWeight

            progress.SetSize(resolution * resolution);
            

            for (var pixelX = 0; pixelX < width; pixelX++)// for each x
            {
                var relativeX = pixelX * relativeSize;
                var relativeAlphaMapX = pixelX * relativeAlphaMapSize;
                for (var pixelY = 0; pixelY < height; pixelY++) //for each y
                {
                    var relativeY = pixelY * relativeSize;
                    var relativeAlphaMapY = pixelY * relativeAlphaMapSize;
                    
                    for (var i = 0; i < totalLayers; i++) //for each layer
                    {
                        var layer = paintLayers[i];
                        var sample = layer.GetSample(relativeX, relativeY , pixelX , pixelY, terrainData, useWorldPos);
                        var sampleComplement = 1 - sample;
                        splatWeights[i] = sample;

                        if (i > 0 )
                        {
                            float sum = 0;
                            for (var j = i - 1; j >= 0; j--) //recalculate total sum from current to bottom
                            {
                                sum += splatWeights[j];
                            }

                            for (var j = i - 1; j >= 0; j--) // recalculate percentages based on the sum from current to bottom
                            {
                                var percentage = splatWeights[j] / sum;
                                splatWeights[j] = percentage * sampleComplement;
                            }
                        }
                    }

                    for (var a = 0; a < alphaLayers; a++)
                    {
                        splatmapData[pixelY, pixelX, a] = splatWeights[a];
                    }
                    
                    if (!progress.TryProcess())
                    {
                        yield return progress.ResetIteraction();
                    }
                }
            }

            terrainData.SetAlphamaps(0, 0, splatmapData);
        }

        private TerrainLayer[] GetTerrainLayers()
        {
            return ExportGroup.GetTerrainLayers();
        }

        private PaintLayer[] GetPaintLayers()
        {
            return ExportGroup.GetPaintLayers();
        }
    }
}