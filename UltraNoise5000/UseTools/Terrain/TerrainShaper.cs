using System.Collections;
using NoiseUltra.Generators;
using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Tools.Terrains
{
    public class TerrainShaper : TerrainTool
    {
        private float[,] m_HeightMap;

        protected override void OnBeforeApplyAsync()
        {
            var resolution = GetHeightMapResolution();
            m_HeightMap = new float[resolution, resolution];
            progress.SetSize(resolution * resolution);
            var position = transform.position;
            var relativeSize = GetRelativeSize();
            Profiler.Start();
            for (var x = 0; x < resolution; x++)
            {
                var xPos = x * relativeSize;
                if (useWorldCordinates)
                    xPos -= position.x;
                
                for (var y = 0; y < resolution; y++)
                {
                    var yPos = y * relativeSize;
                    if (useWorldCordinates)
                        yPos -= position.z;
                    

                    var sample = new SampleInfoFloat2(xPos, yPos, y, x, ref m_HeightMap);
                    taskGroup.AddSampleInfo(sample);
                }
            }
        }
        
        protected override void OnCompleteTask()
        {
            Profiler.End("Terrain Async");
            GetTerrainData().SetHeights(0, 0, m_HeightMap);
        }

        protected override IEnumerator Operation()
        {
            Profiler.Start();
            var resolution = GetHeightMapResolution();
            m_HeightMap = new float[resolution, resolution];
            progress.SetSize(resolution * resolution);
            var position = transform.position;
            var relativeSize = GetRelativeSize();
            yield return Operate(resolution, position, relativeSize);
            GetTerrainData().SetHeights(0, 0, m_HeightMap);
            progress.Reset();
            Profiler.End("Sync Terrain Shape");
        } 

        private IEnumerator Operate(int resolution, Vector3 position, float relativeSize)
        { 
            for (var x = 0; x < resolution; x++)
            {
                var xPos = x * relativeSize;
                if (useWorldCordinates) xPos += position.x;
                for (var y = 0; y < resolution; y++)
                {
                    var yPos = y * relativeSize;
            
                    if (useWorldCordinates) 
                        yPos += position.z;
                    

                    m_HeightMap[y, x] = GetSample(xPos, yPos);
                    
                    if (!progress.TryProcess())
                    {
                        yield return progress.ResetIteration();
                    }
                }
            }
        }
        
        
    }
}