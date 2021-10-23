using NoiseUltra.Nodes;
using NoiseUltra.Tools.Placement;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra
{
    public class UltraMeshDeformation : MonoBehaviour
    {
        public NodeBase @base;
        public PositionSettings noisePlacement;
        public float heightMultiplayer;
        public float heightFloatMinus = 0.5f;


        private Mesh _myMesh;


        private MeshFilter _myMeshFilter;
        private Vector3[] meshPoints;
        private Vector3[] worldMeshPoints;

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

        private Mesh myMesh
        {
            get
            {
                if (_myMesh == null)
                    _myMesh = myMeshFilter.mesh;
                return _myMesh;
            }
            set => _myMesh = value;
        }


        [Button]
        public void ApplyNoiseToMesh()
        {
            meshPoints = myMesh.vertices;
            worldMeshPoints = new Vector3[meshPoints.Length];

            for (var i = 0; i < meshPoints.Length; i++)
            {
                worldMeshPoints[i] = transform.localToWorldMatrix.MultiplyPoint3x4(meshPoints[i]);
                noisePlacement.IsPositionValid(worldMeshPoints[i]);
                worldMeshPoints[i] = noisePlacement.Calculator(worldMeshPoints[i], 1);
                worldMeshPoints[i].y =
                    (@base.GetSample(worldMeshPoints[i].x, worldMeshPoints[i].z) - heightFloatMinus) *
                    heightMultiplayer;
                meshPoints[i] = transform.worldToLocalMatrix.MultiplyPoint3x4(worldMeshPoints[i]);
            }

            myMesh.vertices = meshPoints;
            myMesh.RecalculateNormals();
        }
    }
}