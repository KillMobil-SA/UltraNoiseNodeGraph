using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoiseUltra.Generators;
using NoiseUltra.Nodes;
using NoiseUltra.Output;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Terrains
{
    public class TerrainPainter : TerrainTool
    {
        private readonly List<Task> m_Tasks = new List<Task>();
        private TerrainLayerExportGroup ExportGroup => sourceNode as TerrainLayerExportGroup;
        private float[,,] m_SplatmapData;
        private int m_Resolution;
        private TerrainData m_TerrainData;
        private int m_Width;
        private int m_Height;
        private int m_AlphaLayers;
        private float m_RelativeSize;
        private PaintLayer[] m_PaintLayers;
        private int m_TotalPaintLayers;
        private float[] m_SplatWeights;
        private float[,,] m_SamplesAsync;
        
        [Button]
        private void ExecuteAsync()
        {
            Profiler.Start();
            Initialize();
            m_Resolution = GetHeightMapResolution();
            m_TerrainData = terrain.terrainData;
            m_Width = m_TerrainData.alphamapWidth;
            m_Height = m_TerrainData.alphamapHeight;
            m_AlphaLayers = m_TerrainData.alphamapLayers;
            m_TerrainData.terrainLayers = GetTerrainLayers();
            m_SplatmapData = new float[m_Width, m_Height, m_AlphaLayers];
            m_RelativeSize = GetRelativeSize();
            m_PaintLayers = GetPaintLayers();
            m_TotalPaintLayers = m_PaintLayers.Length;
            m_SamplesAsync = new float[m_Width, m_Height, m_TotalPaintLayers];
            ExecuteSampling();
            ExecuteSplatMapBalance();
            ExecuteSplatMap();
            OnComplete();
            Profiler.End();
        }

        private void ExecuteSampling()
        {
            for (var pixelX = 0; pixelX < m_Width; ++pixelX)
            {
                var relativeX = pixelX * m_RelativeSize;
                if (useWorldCordinates) relativeX += transform.position.x;
                for (var pixelY = 0; pixelY < m_Height; ++pixelY)
                {
                    var relativeY = pixelY * m_RelativeSize;
                    if (useWorldCordinates) relativeY += transform.position.z;
                    for (var layerIndex = 0; layerIndex < m_TotalPaintLayers; ++layerIndex)
                    {
                        var layer = m_PaintLayers[layerIndex];
                        float angleV = 1;
                        if (layer.IsAnglePaint)
                        {
                            var x01 = pixelX / m_TerrainData.alphamapWidth;
                            var y01 = pixelY / m_TerrainData.alphamapHeight;
                            var steepness = m_TerrainData.GetSteepness(x01, y01);
                            angleV = layer.EvaluateCliff(steepness, m_Resolution);
                        }

                        var info = new PaintToolSampleInfo(relativeX, relativeY, pixelX, pixelY, layerIndex, ref m_SamplesAsync, angleV);
                        m_Tasks.Add(Task.Run(() => info.Execute(layer.GetSample)));
                    }
                }
            }

            Task.WaitAll(m_Tasks.ToArray());
            m_Tasks.Clear();
        }

        private void ExecuteSplatMapBalance()
        {
            for (var pixelX = 0; pixelX < m_Width; ++pixelX)
            {
                for (var pixelY = 0; pixelY < m_Height; ++pixelY)
                {
                    for (var layerIndex = 0; layerIndex < m_TotalPaintLayers; ++layerIndex)
                    {
                        float sample = m_SamplesAsync[pixelX, pixelY, layerIndex];
                        float sampleComplement = 1 - sample;
                        float sum = 0;

                        // #YWR: This whole method is hard to be parallelized because of
                        // the code below. Each iteration relies on the previous one.
                        // Which turns everything into a sequence of dependencies.
                        for (var j = layerIndex - 1; j >= 0; --j)
                        {
                            sum += m_SamplesAsync[pixelX, pixelY, j];
                        }

                        for (var j = layerIndex - 1; j >= 0; --j)
                        {
                            var percentage = m_SamplesAsync[pixelX, pixelY, j] / sum;
                            m_SamplesAsync[pixelX, pixelY, j] = percentage * sampleComplement;
                        }
                    }
                }
            }
        }

        private void ExecuteSplatMap()
        {
            for (var pixelX = 0; pixelX < m_Width; ++pixelX)
            {
                for (var pixelY = 0; pixelY < m_Height; ++pixelY)
                {
                    for (var a = 0; a < m_AlphaLayers; ++a)
                    {
                        m_SplatmapData[pixelY, pixelX, a] = m_SamplesAsync[pixelX, pixelY, a];
                    }
                }
            }
        }

        protected override IEnumerator Operation()
        {
            Profiler.Start();
            m_Resolution = GetHeightMapResolution();
            m_TerrainData = GetTerrainData();
            m_Width = m_TerrainData.alphamapWidth;
            m_Height = m_TerrainData.alphamapHeight;
            m_AlphaLayers = m_TerrainData.alphamapLayers;
            m_TerrainData.terrainLayers = GetTerrainLayers();
            m_SplatmapData = new float[m_Width, m_Height, m_AlphaLayers];
            m_RelativeSize = GetRelativeSize();
            m_PaintLayers = GetPaintLayers();
            m_TotalPaintLayers = m_PaintLayers.Length;
            m_SplatWeights = new float[m_AlphaLayers];
            progress.SetSize(m_Resolution * m_Resolution);

            for (var pixelX = 0; pixelX < m_Width; pixelX++) // for each x
            {
                var relativeX = pixelX * m_RelativeSize;
                if (useWorldCordinates) relativeX += transform.position.x;
                for (var pixelY = 0; pixelY < m_Height; pixelY++) //for each y
                {
                    var relativeY = pixelY * m_RelativeSize;
                    if (useWorldCordinates) relativeY += transform.position.z;

                    for (var i = 0; i < m_TotalPaintLayers; i++) //for each layer
                    {
                        var layer = m_PaintLayers[i];
                        var sample = layer.GetSample(relativeX, relativeY, pixelX, pixelY, m_TerrainData,
                            useWorldCordinates);
                        var sampleComplement = 1 - sample;
                        m_SplatWeights[i] = sample;

                        if (i > 0)
                        {
                            float sum = 0;
                            for (var j = i - 1; j >= 0; j--) //recalculate total sum from current to bottom
                            {
                                sum += m_SplatWeights[j];
                            }

                            for (var j = i - 1;
                                j >= 0;
                                j--) // recalculate percentages based on the sum from current to bottom
                            {
                                var percentage = m_SplatWeights[j] / sum;
                                m_SplatWeights[j] = percentage * sampleComplement;
                            }
                        }
                    }

                    for (var a = 0; a < m_AlphaLayers; a++)
                    {
                        m_SplatmapData[pixelY, pixelX, a] = m_SplatWeights[a];
                    }

                    if (!progress.TryProcess())
                    {
                        yield return progress.ResetIteration();
                    }
                }
            }

            OnComplete();
            Profiler.End();
        }

        private void OnComplete()
        {
            m_TerrainData.SetAlphamaps(0, 0, m_SplatmapData);
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