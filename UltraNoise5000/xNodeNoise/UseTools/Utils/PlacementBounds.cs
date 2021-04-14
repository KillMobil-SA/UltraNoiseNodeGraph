using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoiseUltra
{
    public class PlacementBounds
    {

        private UltraNoisePlacementTool ultraNoisePlacementTool;

        public Collider placementCollider;

        public PlacementBounds(UltraNoisePlacementTool _ultraNoisePlacementTool, Collider _placementCollider)
        {
            this.ultraNoisePlacementTool = _ultraNoisePlacementTool;
            this.placementCollider = _placementCollider;
        }

        public float xAmount
        {
            get
            {
                var v =  (placementCollider.bounds.size.x) / spacing;
                if (v == 0) v = 1;
                return v;
            }
        }

        public int xAmountInt
        {
            get
            {
                return Mathf.RoundToInt(xAmount);
            }
        }
        

        public float yAmount
        {
            get
            {
                var v =  (placementCollider.bounds.size.y) / spacing;
                if (v == 0) v = 1;
                return v;
            }
        }
        
        public int yAmountInt
        {
            get
            {
                return Mathf.RoundToInt(yAmount);
            }
        }

        public float zAmount
        {
            get
            {
                var v =  (placementCollider.bounds.size.z) / spacing;
                if (v == 0) v = 1;
                return v;
            }
        }
        
        public int zAmountInt
        {
            get
            {
                return Mathf.RoundToInt(zAmount);
            }
        }


        public bool heightIs2D
        {
            get
            {
                if (yAmount <= 1) return true;
                else return false;
            }
        }


        public Vector3 GetPosVector(int x, int y, int z)
        {
            float xPos = x - xAmount / 2;
            float yPos = y - yAmount / 2;
            float zPos = z - zAmount / 2;
            Vector3 pos = (new Vector3(xPos, yPos, zPos) * spacing) + center;
            return pos;
        }
        
        
        
        public Vector3 center
        {
            get
            {
                Vector3 center = placementCollider.bounds.center;
                Vector3 centerRound = new Vector3(Mathf.Round(center.x / spacing) * spacing, Mathf.Round(center.y),
                        Mathf.Round(center.z / spacing) * spacing); 
                return centerRound;
            }
        }

        float spacing
        {
            get { return ultraNoisePlacementTool.spacing; }
        }

        private bool useWorldPos
        {
            get { return ultraNoisePlacementTool.useWorldPos; }
        }
        
        void ColliderBoundsPrintOut()
        {

            Vector3 m_Center;
            Vector3 m_Size, m_Min, m_Max;

            //Fetch the center of the Collider volume
            m_Center = placementCollider.bounds.center;
            //Fetch the size of the Collider volume
            m_Size = placementCollider.bounds.size;
            //Fetch the minimum and maximum bounds of the Collider volume
            m_Min = placementCollider.bounds.min;
            m_Max = placementCollider.bounds.max;
            //Output this data into the console
            //Output to the console the center and size of the Collider volume
            Debug.Log("Collider Center : " + m_Center);
            Debug.Log("Collider Size : " + m_Size);
            Debug.Log("Collider bound Minimum : " + m_Min);
            Debug.Log("Collider bound Maximum : " + m_Max);
        }

    }
}