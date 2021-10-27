using System;
using System.Collections;
using System.Threading.Tasks;
using NoiseUltra.Generators;
using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Terrains
{
    public class TerrainShaper : TerrainTool
    {
        private float[,] _heightMap;

        [Button]
        private void ApplyAsync()
        {
            Initialize();

            var resolution = GetHeightMapResolution();
            _heightMap = new float[resolution, resolution];
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
                    

                    var sample = new SampleInfoHeightMap(xPos, yPos, y, x, ref _heightMap);
                    taskGroup.AddSampleInfo(sample);
                }
            }

            taskGroup.ExecuteAll();
        }
        
        protected override void OnCompleteTask()
        {
            Profiler.End("Terrain Async");
            GetTerrainData().SetHeights(0, 0, _heightMap);
        }

        protected override IEnumerator Operation()
        {
            Profiler.Start();
            var resolution = GetHeightMapResolution();
            _heightMap = new float[resolution, resolution];
            progress.SetSize(resolution * resolution);
            var position = transform.position;
            var relativeSize = GetRelativeSize();
            yield return Operate(resolution, position, relativeSize);
            GetTerrainData().SetHeights(0, 0, _heightMap);
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