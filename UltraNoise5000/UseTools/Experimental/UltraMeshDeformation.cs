using NoiseUltra.Nodes;
using NoiseUltra.Output;
using NoiseUltra.Tools.Placement;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif


namespace NoiseUltra
{
    public class UltraMeshDeformation : MonoBehaviour
    {

        public ExportNode exportNode;
        public bool isWorldCordinates;
        [OnValueChanged(nameof(AutoDraw))]
        public Vector3 offSetAmount;
        [OnValueChanged(nameof(AutoDraw))]
        public Vector3 noiseOffsetAmount = new Vector3(0,1000,10000);
        [OnValueChanged(nameof(AutoDraw))]
        public float noiseModifier;

        public bool autoDraw = false;
        

        
        [SerializeField] private Vector3[] originalMeshPoints;
 
        [SerializeField] private Vector3[] meshPoints;



        [Button]
        public void BackUpMeshPoints()
        {
#if UNITY_EDITOR
            CloneMesh();
#endif
            
            originalMeshPoints = myMesh.vertices;
            meshPoints = new Vector3[originalMeshPoints.Length];
        }

        [Button]
        public void ApplyBackUp()
        {
            myMesh.vertices = originalMeshPoints;
            myMesh.RecalculateNormals();
        }


        [Button]
        public void ApplyNoiseToMesh()
        {
            if (originalMeshPoints == null || originalMeshPoints.Length == 0) 
                BackUpMeshPoints();
            
            for (var i = 0; i < meshPoints.Length; i++)
            {
                var pos = originalMeshPoints[i];
                
                if (isWorldCordinates)
                    pos = transform.localToWorldMatrix.MultiplyPoint3x4(pos);


                var xSample = exportNode.GetSample(pos.x + noiseOffsetAmount.x, pos.y + noiseOffsetAmount.x, pos.z + noiseOffsetAmount.x);
                var ySample = exportNode.GetSample(pos.x + noiseOffsetAmount.y, pos.y + noiseOffsetAmount.y, pos.z + noiseOffsetAmount.y);
                var zSample = exportNode.GetSample(pos.x + noiseOffsetAmount.z, pos.y + noiseOffsetAmount.z, pos.z + noiseOffsetAmount.z);

                var deformResult = pos +  new Vector3(
                    (xSample + noiseModifier) * offSetAmount.x,
                    (ySample + noiseModifier) * offSetAmount.y,
                    (zSample + noiseModifier) * offSetAmount.z
                    );
                

                if (isWorldCordinates)
                    meshPoints[i] = transform.worldToLocalMatrix.MultiplyPoint3x4(deformResult);
                else
                    meshPoints[i] = deformResult;
                
            }
            
            myMesh.vertices = meshPoints;
            myMesh.RecalculateNormals();

        }


        public void AutoDraw()
        {
            if (autoDraw) ApplyNoiseToMesh();
        }
        

#if UNITY_EDITOR
        [Button]
        public void CloneMesh()
        {

           string meshAssetPath =  AssetDatabase.GetAssetPath(myMesh);
           string meshFileName = Path.GetFileName(meshAssetPath);
           string meshFolderPath = meshAssetPath.Replace(meshFileName, string.Empty);
           string meshCopyFilename = meshFolderPath + myMesh.name + "_clone.asset";
           
           
           Debug.Log("assetPath:" + meshAssetPath);
           Debug.Log("filename:" + meshFileName);
           Debug.Log("meshFolderPath:" + meshFolderPath);
           Debug.Log("meshCopyFilename:" + meshCopyFilename);
           
           
           
           
           Mesh meshToSave = Object.Instantiate(myMesh) as Mesh ;
           AssetDatabase.CreateAsset( meshToSave, meshCopyFilename);
           AssetDatabase.SaveAssets();

           myMesh = myMeshFilter.sharedMesh = meshToSave;
        }
        
        #endif



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
    }
}




/*
public NodeBase @base;
public PositionSettings noisePlacement;
public float heightMultiplayer;
public float heightFloatMinus = 0.5f;






private Vector3[] meshPoints;
private Vector3[] worldMeshPoints;



[Button]
public void ApplyNoiseToMesh()
{
    meshPoints = myMesh.vertices;
    worldMeshPoints = new Vector3[meshPoints.Length];

    for (var i = 0; i < meshPoints.Length; i++)
    {
        worldMeshPoints[i] = transform.localToWorldMatrix.MultiplyPoint3x4(meshPoints[i]);
        noisePlacement.IsPositionValid(worldMeshPoints[i]);
        worldMeshPoints[i] = noisePlacement.Execute(worldMeshPoints[i], 1);
        worldMeshPoints[i].y =
            (@base.GetSample(worldMeshPoints[i].x, worldMeshPoints[i].z) - heightFloatMinus) *
            heightMultiplayer;
        meshPoints[i] = transform.worldToLocalMatrix.MultiplyPoint3x4(worldMeshPoints[i]);
    }

    myMesh.vertices = meshPoints;
    myMesh.RecalculateNormals();
}
}

*/
