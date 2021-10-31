using System.Collections;
using NoiseUltra.Generators;
using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Tools.Terrains
{
    public class TerrainShaper : TerrainTool
    {
        private float[,] m_HeightMap;

        #region Async

        protected override void OnBeforeApplyAsync()
        {
            Profiler.Start();
            int resolution = GetHeightMapResolution();
            m_HeightMap = new float[resolution, resolution];
            progress.SetSize(resolution * resolution);
            Vector3 position = transform.position;
            float relativeSize = GetRelativeSize();


            for (int x = 0; x < resolution; x++)
            {
                float xPos = x * relativeSize;
                if (useWorldCordinates)
                {
                    xPos -= position.x;
                }
                
                for (int y = 0; y < resolution; y++)
                {
                    float yPos = y * relativeSize;
                    if (useWorldCordinates)
                    {
                        yPos -= position.z;
                    }
                    
                    SampleStepFloat2 sample = new SampleStepFloat2(xPos, yPos, y, x, ref m_HeightMap);
                    void Action() => sourceNode.ExecuteSampleAsync(sample);
                    taskGroup.AddTask(Action);
                }
            }
        }
        
        protected override void OnCompleteTask()
        {
            Profiler.End("Terrain Async");
            GetTerrainData().SetHeights(0, 0, m_HeightMap);
        }

        #endregion

        #region Sync
        protected override IEnumerator ExecuteSync()
        {
            Profiler.Start();
            int resolution = GetHeightMapResolution();
            m_HeightMap = new float[resolution, resolution];
            progress.SetSize(resolution * resolution);
            Vector3 position = transform.position;
            float relativeSize = GetRelativeSize();
            yield return ExecuteSync(resolution, position, relativeSize);
            GetTerrainData().SetHeights(0, 0, m_HeightMap);
            progress.Reset();
            Profiler.End("Sync Terrain Shape");
        } 

        private IEnumerator ExecuteSync(int resolution, Vector3 position, float relativeSize)
        { 
            for (int x = 0; x < resolution; x++)
            {
                float xPos = x * relativeSize;
                if (useWorldCordinates) xPos += position.x;
                for (int y = 0; y < resolution; y++)
                {
                    float yPos = y * relativeSize;

                    if (useWorldCordinates)
                    {
                        yPos += position.z;
                    }

                    m_HeightMap[y, x] = GetSample(xPos, yPos);
                    
                    if (!progress.TryProcess())
                    {
                        yield return progress.ResetIteration();
                    }
                }
            }
        }
        #endregion
    }
}