using NoiseUltra.Output;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif


namespace NoiseUltra
{
    public class UltraMeshDeformationTool : MonoBehaviour
    {
        [SerializeField]
        private ExportNode exportNode;

        [SerializeField]
        [OnValueChanged(nameof(AutoDraw))]
        private Vector3 offSetAmount;

        [SerializeField]
        [OnValueChanged(nameof(AutoDraw))]
        public Vector3 noiseOffsetAmount = new Vector3(0, 1000, 10000);

        [SerializeField]
        [OnValueChanged(nameof(AutoDraw))]
        private float noiseModifier = -0.5f;

        [SerializeField]
        private bool isWorldCordinates;

        [SerializeField]
        private bool enableSphereMode;
        
        [SerializeField]
        private bool autoDraw = false;

        [SerializeField]
        private Vector3[] originalMeshPoints;

        [SerializeField]
        private Vector3[] meshPoints;


        #region Backfield Properties

        private MeshFilter _myMeshFilter;

        private MeshFilter myMeshFilter
        {
            get
            {
                if (_myMeshFilter == null)
                    _myMeshFilter = GetComponent<MeshFilter>();
                return _myMeshFilter;
            }
            set => _myMeshFilter = value;
        }

        private Mesh _myMesh;

        private Mesh myMesh
        {
            get
            {
                if (_myMesh == null)
#if UNITY_EDITOR
                    _myMesh = myMeshFilter.sharedMesh;
#else
                    _myMesh = myMeshFilter.mesh;
#endif
                return _myMesh;
            }
            set => _myMesh = value;
        }

        #endregion

        [Button]
        public void ApplyNoiseToMesh()
        {
            if (originalMeshPoints == null || originalMeshPoints.Length == 0)
            {
            //  BackUpMeshPoints();
            }

            float noiseOffsetX = noiseOffsetAmount.x;
            float noiseOffsetY = noiseOffsetAmount.y;
            float noiseOffsetZ = noiseOffsetAmount.z;

            Matrix4x4 worldLocalMatrix = transform.worldToLocalMatrix;
            Matrix4x4 localWorldMatrix = transform.localToWorldMatrix;
            
            int length = meshPoints.Length;
            for (var i = 0; i < length; ++i)
            {
                Vector3 pos = originalMeshPoints[i];

                if (isWorldCordinates)
                {
                    pos = localWorldMatrix.MultiplyPoint3x4(pos);
                }


                float seedYx = noiseOffsetX + pos.x;
                float seedYy = noiseOffsetY + pos.y;
                float seedYz = noiseOffsetZ + pos.z;


                
           
                float ySample = exportNode.GetSample(seedYx, seedYy, seedYz);
      

         
                float sampleNoiseY = (ySample + noiseModifier) * offSetAmount.y;
         

                Vector3 sampleNoise = new Vector3(0, sampleNoiseY, 0);

                if (enableSphereMode)
                {
                    Vector3 center = Vector3.zero;
                    if (isWorldCordinates)
                    {
                        center = transform.position;
                    }

                    Quaternion diffRotation = Quaternion.LookRotation(pos - center);
                    sampleNoise = ConvertVector(sampleNoise, diffRotation, Vector3.zero, Vector3.one);
                }

                Vector3 deformationResult = sampleNoise + pos;

                meshPoints[i] = isWorldCordinates
                    ? worldLocalMatrix.MultiplyPoint3x4(deformationResult)
                    : deformationResult;
            }

            myMesh.vertices = meshPoints;
            myMesh.RecalculateNormals();
        }

        [Button]
        private void RevertMesh()
        {
            myMesh.vertices = originalMeshPoints;
            myMesh.RecalculateNormals();
        }

        private Vector3 ConvertVector(Vector3 source, Quaternion rotate, Vector3 move, Vector3 scale)
        {
            Matrix4x4 m = Matrix4x4.TRS(move, rotate, scale);
            Vector3 result = m.MultiplyPoint3x4(source);
            return result;
        }

        private void AutoDraw()
        {
            if (autoDraw)
            {
                ApplyNoiseToMesh();
            }
        }


       
        


#if UNITY_EDITOR
        const string assetSuffix = "_clone.asset";
        [Button]
        private void CloneMesh()
        {
            string meshAssetPath = AssetDatabase.GetAssetPath(myMesh);
            string meshFileName = Path.GetFileName(meshAssetPath);
            string meshFolderPath = meshAssetPath.Replace(meshFileName, string.Empty);
            string newFileName = myMesh.name + assetSuffix;
            string meshCopyFilename = meshFolderPath + newFileName;

            Mesh meshToSave = Object.Instantiate(myMesh) as Mesh;
            
            if (!meshAssetPath.Contains("Library"))
                AssetDatabase.CreateAsset(meshToSave, meshCopyFilename);
            else
                AssetDatabase.CreateAsset(meshToSave, "Assets/" + newFileName);
            
            AssetDatabase.SaveAssets();
            myMesh = myMeshFilter.sharedMesh = meshToSave;
        }

#endif
        [Button]
        private void BackUpMeshPoints()
        {
#if UNITY_EDITOR
            CloneMesh();
#endif

            originalMeshPoints = myMesh.vertices;
            meshPoints = new Vector3[originalMeshPoints.Length];
        }
    }
}