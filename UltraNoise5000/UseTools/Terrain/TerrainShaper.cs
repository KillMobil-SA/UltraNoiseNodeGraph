using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoiseUltra.Generators;
using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Terrains
{
    [ExecuteInEditMode]
    public class TerrainShaper : TerrainTool
    {
        private List<Task> _tasks = new List<Task>();
        private float[,] _heightMap;

        [Button]
        private void SetupHeightMap()
        {
            var resolution = GetHeightMapResolution();
            _heightMap = new float[resolution, resolution];
            progress.SetSize(resolution * resolution);
            var position = transform.position;
            var relativeSize = GetRelativeSize();
            Operate(resolution, position, relativeSize, 0);
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

        private void Operate(int resolution, Vector3 position, float relativeSize, int test)
        {
            Profiler.Start();
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

                    var sample = new SampleInfoHeightMap(xPos, yPos, resolution, resolution, y, x, ref _heightMap, OnComplete);
                    Action task = () => sourceNode.GetSampleAsync(sample);
                    _tasks.Add(Task.Run(task));
                }
            }

            Task.WaitAll(_tasks.ToArray());
        }
        

        private bool isCompleted = false;
        
        private void OnComplete() => isCompleted = true;

        private void Complete()
        {
            Profiler.End("Terrain Async");
            GetTerrainData().SetHeights(0, 0, _heightMap);
        }

        private void Update()
        {
            if (isCompleted)
            {
                Complete();
                isCompleted = false;
            }
        }
    }
}