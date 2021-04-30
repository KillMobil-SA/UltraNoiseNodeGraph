<<<<<<< HEAD
    using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;
using NoiseUltra.Nodes;
    using NoiseUltra.Tools.Placement;

=======
using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;
>>>>>>> f5ee208a90c9bc5a5c97c3c815672de79590a885

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
                noisePlacement.ChechPos(worldMeshPoints[i]);
                worldMeshPoints[i] = noisePlacement.PotisionCalculator(worldMeshPoints[i], 1);
                worldMeshPoints[i].y =
                    (@base.Sample2D(worldMeshPoints[i].x, worldMeshPoints[i].z) - heightFloatMinus) * heightMultiplayer;
                meshPoints[i] = transform.worldToLocalMatrix.MultiplyPoint3x4(worldMeshPoints[i]);
            }

            myMesh.vertices = meshPoints;
            myMesh.RecalculateNormals();
        }
    }
}