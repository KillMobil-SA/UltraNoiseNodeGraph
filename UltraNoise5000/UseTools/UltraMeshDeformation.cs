    using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;
using NoiseUltra.Nodes;
    using NoiseUltra.Tools.Placement;


namespace NoiseUltra
{
    public class UltraMeshDeformation : MonoBehaviour
    {
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
            
            for (int i = 0; i < meshPoints.Length; i++)
            {
                worldMeshPoints [i] = transform.localToWorldMatrix.MultiplyPoint3x4 (meshPoints [i]);
                noisePlacement.ChechPos(worldMeshPoints[i]);
                worldMeshPoints[i] = noisePlacement.PotisionCalculator(worldMeshPoints[i], 1);
                worldMeshPoints[i].y = 
                    (@base.Sample2D(worldMeshPoints[i].x,  worldMeshPoints[i].z) - heightFloatMinus) * heightMultiplayer;
                meshPoints[i] = transform.worldToLocalMatrix.MultiplyPoint3x4(worldMeshPoints[i]);
            }

            myMesh.vertices = meshPoints;
            myMesh.RecalculateNormals ();
        }
        
        
        
        private MeshFilter _myMeshFilter;

        private MeshFilter myMeshFilter {
            get {
                if (_myMeshFilter == null)
                    _myMeshFilter = GetComponent<MeshFilter> ();
                return _myMeshFilter;
            }   
            set {
                _myMeshFilter = value;
            }
        }


        private Mesh _myMesh;

        private Mesh myMesh {
            get {
                if (_myMesh == null)
                    _myMesh = myMeshFilter.mesh;
                return _myMesh;
            }   
            set {
                _myMesh = value;
            }
        }

        
    }

}
