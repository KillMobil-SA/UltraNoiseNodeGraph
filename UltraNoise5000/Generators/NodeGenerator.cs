using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoiseUltra.Nodes;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace NoiseUltra.Generators
{
    public static class Profiler
    {
        public static DateTime Begin;
        public static void Start()
        {
            Begin = DateTime.Now;
        }
        public static void End(string context)
        {
            Debug.Log($"Elapsed Time: {(DateTime.Now.Subtract(Begin).Milliseconds)} Milliseconds {context}");
        }
    }

    [Serializable]
    public class Cache : Dictionary<float, float>
    {

    }

    public class Cache2d : Dictionary<Cache2d.Entry, float>
    {
        public struct Entry
        {
            private const float Precision = float.Epsilon;
            public float x;
            public float y;

            public Entry(float x, float y)
            {
                this.x = x;
                this.y = y;
            }

            public static bool operator ==(Entry left, Entry right)
            {
                return Mathf.Abs(left.x - right.x) < Precision
                       && Mathf.Abs(left.y - right.y) < Precision;
            }

            public static bool operator !=(Entry left, Entry right)
            {
                return !(right == left);
            }
        }
    }

    [NodeTint(NodeColor.Green)]
    public class NodeGenerator : NodeOutput
    {
        //[SerializeField] 
        //private Cache cache = new Cache();

        //[SerializeField]
        //private Cache2d cache2d = new Cache2d();
        //private object locker2d = new object();

        #region Members
        [SerializeField]
        private Attributes attributes = new Attributes();
        private FractalNoise _fractal;
        
        private bool IsDirty => _fractal == null;
        #endregion

        #region Public
        public void SetFractalDirty()
        {
            _fractal = null;
            //cache.Clear();
            //cache2d.Clear();
        }

        public override float GetSample(float x)
        {
            //if (cache.ContainsKey(x))
            //{
            //    return cache[x];
            //}

            var sample = GetFractal().Sample1D(x);
            //cache.Add(x, sample);
            return sample;
        }

        public override float GetSample(float x, float y)
        {
            CacheSample2d(x, y);
            var sample = GetFractal().Sample2D(x, y);
            return sample;
            return 0;
        }

        private void CacheSample2d(float x, float y)
        {
            //lock (locker2d)
            //{
            //    var task = new Task<float>(() => GetFractal().Sample2D(x, y));
            //    Task.Run(() => task);
            //    cache2d.Add(new Cache2d.Entry(x, y), task.Result);
            //    Debug.Log($"added {x} {y} {task}");
            //}
        }

        public override float GetSample(float x, float y, float z)
        {
            return GetFractal().Sample3D(x, y, z);
        }
        #endregion

        #region Private
        [Button]
        private void New()
        {
            attributes.RandomizeSeed();
            SetFractalDirty();
            Draw();
        }

        private FractalNoise GetFractal()
        {
            return IsDirty ? CreateFractal() : _fractal;
        }

        private FractalNoise CreateFractal()
        {
            attributes.SetGenerator(this);
            return NodeGeneratorHelper.CreateFractal(attributes);
        }

        #endregion
    }
}