using System;
using System.Collections;
using System.Collections.Generic;
using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using Unity.EditorCoroutines.Editor;
#endif

namespace NoiseUltra
{
    public class UltraNoiseTerrainPainter : MonoBehaviour
    {
        public bool useWorldPos;

        public List<PaintItem> paintItems = new List<PaintItem>();


        [ProgressBar(0, "totalCount")] public int currentStep;
        [ReadOnly] public int totalCount;

        public int objectPlacementPreFrame = 1000;


        private Terrain _myTerrain;

        private TerrainData _myTerrainData;
        private float[,] heights;
        private int internalPlacementCounter;

        private int res;
        private float sizeRelativity;

        private Terrain myTerrain
        {
            get
            {
                if (_myTerrain == null)
                    _myTerrain = GetComponent<Terrain>();
                return _myTerrain;
            }
            set => _myTerrain = value;
        }

        private TerrainData myTerrainData
        {
            get
            {
                if (_myTerrainData == null)
                    _myTerrainData = myTerrain.terrainData;
                return _myTerrainData;
            }
            set => _myTerrainData = value;
        }


        [Button]
        public void ApplyTerrainPaint()
        {
#if UNITY_EDITOR
            EditorCoroutineUtility.StartCoroutine(TerraPaint(), this);
#else
                            StartCoroutine (TerraPaint ());
#endif
        }


        private IEnumerator TerraPaint()
        {
            res = myTerrainData.heightmapResolution;
            var splatmapData = new float[myTerrainData.alphamapWidth, myTerrainData.alphamapHeight,
                myTerrainData.alphamapLayers];

            sizeRelativity = myTerrainData.size.x / res;
            totalCount = res * res;
            internalPlacementCounter = 0;
            currentStep = 0;

            for (var x = 0; x < myTerrainData.alphamapWidth; x++)
            for (var y = 0; y < myTerrainData.alphamapHeight; y++)
            {
                //heights[y, x] = noiseNodeGraph.Sample2D(x * sizeRelativity, y * sizeRelativity);
                var splatWeights = new float[myTerrainData.alphamapLayers];


                for (var i = 0; i < paintItems.Count; i++)
                {
                    var xPos = x * sizeRelativity;
                    var yPos = y * sizeRelativity;


                    var v = paintItems[i].Sample2D(xPos, yPos, myTerrainData, useWorldPos, transform);
                    splatWeights[paintItems[i].splatID] = v;

                    if (i > 0)
                    {
                        float sum = 0;
                        var remainingV = 1 - v;
                        for (var j = i - 1; j >= 0; j--)
                        {
                            var prevVal = splatWeights[paintItems[j].splatID];
                            sum += prevVal;
                        }


                        for (var j = i - 1; j >= 0; j--)
                        {
                            var prevVal = splatWeights[paintItems[j].splatID];
                            prevVal = prevVal / sum * remainingV;
                            splatWeights[paintItems[j].splatID] = prevVal;
                        }
                    }
                }

                for (var a = 0; a < myTerrainData.alphamapLayers; a++) splatmapData[y, x, a] = splatWeights[a];

                //splatmapData[x, y, paintItems[i].splatID] = splatWeights[paintItems[i].splatID];

                currentStep++;

                internalPlacementCounter++;
                if (internalPlacementCounter > objectPlacementPreFrame)
                {
                    internalPlacementCounter = 0;
                    yield return null;
                }
            }

            myTerrainData.SetAlphamaps(0, 0, splatmapData);
            //ApplyTerrainPaintData();
        }


        [Serializable]
        public class PaintItem
        {
            public string label;
            public bool isActive = true;
            public bool isOneMinus;
            public bool isAnglePaint;
            [ShowIf("isAnglePaint")] public float angleDivide = 1;
            [ShowIf("isAnglePaint")] public AnimationCurve clifCurve = AnimationCurve.Linear(0, 0, 1, 1);

            public NodeBase nodeGraph;
            public int splatID;


            public float Sample2D(float x, float y, TerrainData trd, bool useWorldPos, Transform terrainTransform)
            {
                float v = 0;

                if (!isActive) return 0;


                var xPos = x;
                var yPos = y;

                if (useWorldPos)
                {
                    xPos += terrainTransform.position.x;
                    yPos += terrainTransform.position.z;
                }


                if (nodeGraph != null)
                    v = nodeGraph.Sample2D(xPos, yPos);
                else
                    v = 1;


                if (isAnglePaint)
                {
                    var x_01 = x / trd.alphamapWidth;
                    var y_01 = y / trd.alphamapHeight;
                    var steepness = trd.GetSteepness(x_01, y_01);

                    var angleV =
                        clifCurve.Evaluate(
                            Mathf.Clamp01(steepness * steepness / (trd.heightmapResolution / angleDivide)));
                    v *= angleV;
                }


                if (isOneMinus)
                    return 1 - v;
                return v;
            }
        }
    }
}