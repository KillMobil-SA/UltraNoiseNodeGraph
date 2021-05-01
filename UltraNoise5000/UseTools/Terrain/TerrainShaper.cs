using System.Collections;
using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using Unity.EditorCoroutines.Editor;

#endif


namespace NoiseUltra
{
    public class TerrainShaper : MonoBehaviour
    {
        public bool useWorldPos;

        public NodeBase nodeGraph;

        private int res;
        private float[,] heights;
        private float sizeRelativity;


        [ProgressBar(0, "totalCount")] public int currentStep;
        [ReadOnly] public int totalCount;

        public int objectPlacementPreFrame = 1000;
        private int internalPlacementCounter;

        private Terrain _myTerrain;

        private Terrain myTerrain
        {
            get
            {
                if (_myTerrain == null)
                    _myTerrain = GetComponent<Terrain>();
                return _myTerrain;
            }
        }


#if UNITY_EDITOR
        private EditorCoroutine terraCoroutine;
#else
private Coroutine terraCoroutine;
#endif

        [Button]
        public void ApplyTerrainHeight()
        {
#if UNITY_EDITOR
            EditorCoroutineUtility.StartCoroutine(TerraForm(), this);
#else
                    StartCoroutine (TerraForm ());
#endif
        }


        private IEnumerator TerraForm()
        {
            res = myTerrain.terrainData.heightmapResolution;
            heights = new float[res, res];
            sizeRelativity = myTerrain.terrainData.size.x / res;
            totalCount = res * res;
            internalPlacementCounter = 0;
            currentStep = 0;


            for (var x = 0; x < res; x++)
            for (var y = 0; y < res; y++)
            {
                var xPos = x * sizeRelativity;
                var yPos = y * sizeRelativity;

                if (useWorldPos)
                {
                    xPos += transform.position.x;
                    yPos += transform.position.z;
                }


                heights[y, x] = nodeGraph.Sample2D(xPos, yPos);
                currentStep++;

                internalPlacementCounter++;
                if (internalPlacementCounter > objectPlacementPreFrame)
                {
                    internalPlacementCounter = 0;
                    yield return null;
                }
            }

            ApplyTerrainData();
        }


        public void ApplyTerrainData()
        {
            myTerrain.terrainData.SetHeights(0, 0, heights);
        }

        [Button]
        public void StopTerraform()
        {
#if UNITY_EDITOR
            EditorCoroutineUtility.StopCoroutine(terraCoroutine);
#else
                StopCoroutine(terraCoroutine);
#endif
        }


        [Button]
        public void GetTerrainData()
        {
            var ter = myTerrain;
            Debug.Log(string.Format("ter.terrainData.size {0}", ter.terrainData.size));
            Debug.Log(string.Format("myTerrain.terrainData.alphamapLayers {0}", myTerrain.terrainData.alphamapLayers));
            Debug.Log(string.Format("ter.terrainData.heightmapResolution {0}", ter.terrainData.heightmapResolution));
            Debug.Log(string.Format("ter.terrainData.alphamapWidth {0}", ter.terrainData.alphamapWidth));
            Debug.Log(string.Format("ter.terrainData.alphamapHeight {0}", ter.terrainData.alphamapHeight));
            Debug.Log(string.Format("ter.terrainData.detailWidth {0}", ter.terrainData.detailWidth));
            Debug.Log(string.Format("ter.terrainData.detailHeight {0}", ter.terrainData.detailHeight));
        }
    }
}