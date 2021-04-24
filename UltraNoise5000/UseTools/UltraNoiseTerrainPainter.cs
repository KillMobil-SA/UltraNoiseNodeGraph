using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using Sirenix.OdinInspector;


#if UNITY_EDITOR
using Unity.EditorCoroutines.Editor;
using UnityEditor;
#endif
using NoiseUltra.Nodes;

namespace NoiseUltra
{
    public class UltraNoiseTerrainPainter : MonoBehaviour
    {

        public bool useWorldPos;
        
        public List<PaintItem> paintItems = new List<PaintItem>();

        private int res;
        private float[,] heights;
        private float sizeRelativity;
        
        
        
        [ProgressBar(0 , "totalCount")]
        public int currentStep;
        [ReadOnly]
        public int totalCount;
        
        public int objectPlacementPreFrame = 1000;
        private int internalPlacementCounter = 0;
        
        
        [Button]
        public void ApplyTerrainPaint() {
        #if UNITY_EDITOR
                    EditorCoroutineUtility.StartCoroutine (TerraPaint (), this);
        #else
                            StartCoroutine (TerraPaint ());
        #endif
            
        }
        
        
        IEnumerator TerraPaint ()
        {
            res = myTerrainData.heightmapResolution; 
            float[, ,] splatmapData = new float[myTerrainData.alphamapWidth, myTerrainData.alphamapHeight,myTerrainData.alphamapLayers];

            sizeRelativity = (float)myTerrainData.size.x / (float)res;
            totalCount = res * res;
            internalPlacementCounter = 0;
            currentStep = 0;
             
            for (int x = 0; x < myTerrainData.alphamapWidth; x++) {
                for (int y = 0; y < myTerrainData.alphamapHeight; y++) {
             
                    //heights[y, x] = noiseNodeGraph.Sample2D(x * sizeRelativity, y * sizeRelativity);
                    float[] splatWeights = new float[myTerrainData.alphamapLayers];
                    
                      
                    for (int i = 0; i < paintItems.Count; i++)
                    {
                        float xPos = x * sizeRelativity;
                        float yPos = y * sizeRelativity;

                      
                        float v = paintItems[i].Sample2D(xPos, yPos , myTerrainData , useWorldPos , transform);
                        splatWeights[paintItems[i].splatID] = v;

                        if (i > 0)
                        {
                     
                                float sum = 0;
                                float remainingV = 1 - v;
                                for (int j = i - 1; j >= 0; j--)
                                {
                                    float prevVal = splatWeights[paintItems[j].splatID];
                                    sum += prevVal;
                                }
                                

                                for (int j = i - 1; j >= 0; j--)
                                {
                                    float prevVal = splatWeights[paintItems[j].splatID];
                                    prevVal = (prevVal / sum) * remainingV;
                                    splatWeights[paintItems[j].splatID] = prevVal;
                                }
                     
                        }
                    }

                    for (int a = 0; a < myTerrainData.alphamapLayers; a++)
                    {
                        splatmapData[y, x,  a] = splatWeights[a];
                    }
                    
                    //splatmapData[x, y, paintItems[i].splatID] = splatWeights[paintItems[i].splatID];
                    
                    currentStep++;
                     
                    internalPlacementCounter++;
                    if (internalPlacementCounter > objectPlacementPreFrame) {
                        internalPlacementCounter = 0;
                        yield return null;
                    }
                }
            }

            myTerrainData.SetAlphamaps(0, 0, splatmapData);
            //ApplyTerrainPaintData();
        }





        private Terrain _myTerrain;

        private Terrain myTerrain
        {
            get
            {
                if (_myTerrain == null)
                    _myTerrain = GetComponent<Terrain>();
                return _myTerrain;
            }
            set { _myTerrain = value; }
        }
        
        private TerrainData _myTerrainData;

        private TerrainData myTerrainData
        {
            get
            {
                if (_myTerrainData == null)
                    _myTerrainData = myTerrain.terrainData;
                return _myTerrainData;
            }
            set { _myTerrainData = value; }
        }
        
        
        [System.Serializable]
        public class PaintItem
        {
            public string label;
            public bool isActive = true;
            public bool isOneMinus = false;
            public bool isAnglePaint = false;
            [ShowIf("isAnglePaint" , true) ]
            public float angleDivide = 1;
            [ShowIf("isAnglePaint" , true) ]
            public AnimationCurve clifCurve = AnimationCurve.Linear(0 , 0 , 1 , 1);
            
            public NodeBase nodeGraph;
            public int splatID;


            public float Sample2D(float x , float y , TerrainData trd , bool useWorldPos , Transform terrainTransform)
            {
                float v = 0;

                if (!isActive) return 0;
                
                
                float xPos = x ;
                float yPos = y ;

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
                    float x_01 = (float)x/(float)trd.alphamapWidth;
                    float y_01 = (float)y/(float)trd.alphamapHeight;
                    
                    float steepness = trd.GetSteepness(x_01, y_01);
                    
                    float angleV = clifCurve.Evaluate(Mathf.Clamp01(steepness*steepness/(trd.heightmapResolution/angleDivide)));
                    v *= angleV;
                }
                

                if (isOneMinus) 
                    return 1 - v;
                else 
                    return v;
            }
        }

    }

}