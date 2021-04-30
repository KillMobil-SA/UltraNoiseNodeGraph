﻿using UnityEngine;

namespace NoiseUltra
{
    public class PlacementBounds
    {
        public Collider placementCollider;

        private readonly UltraNoisePlacementTool ultraNoisePlacementTool;

        public PlacementBounds(UltraNoisePlacementTool _ultraNoisePlacementTool, Collider _placementCollider)
        {
            ultraNoisePlacementTool = _ultraNoisePlacementTool;
            placementCollider = _placementCollider;
        }

        public float xAmount
        {
            get
            {
                var v = placementCollider.bounds.size.x / spacing;
                if (v == 0) v = 1;
                return v;
            }
        }

        public int xAmountInt => Mathf.RoundToInt(xAmount);


        public float yAmount
        {
            get
            {
                var v = placementCollider.bounds.size.y / spacing;
                if (v == 0) v = 1;
                return v;
            }
        }

        public int yAmountInt => Mathf.RoundToInt(yAmount);

        public float zAmount
        {
            get
            {
                var v = placementCollider.bounds.size.z / spacing;
                if (v == 0) v = 1;
                return v;
            }
        }

        public int zAmountInt => Mathf.RoundToInt(zAmount);


        public bool heightIs2D
        {
            get
            {
                if (yAmount <= 1) return true;
                return false;
            }
        }


        public Vector3 center
        {
            get
            {
                var center = placementCollider.bounds.center;
                var centerRound = new Vector3(Mathf.Round(center.x / spacing) * spacing, Mathf.Round(center.y),
                    Mathf.Round(center.z / spacing) * spacing);
                return centerRound;
            }
        }

        private float spacing => ultraNoisePlacementTool.spacing;

        private bool useWorldPos => ultraNoisePlacementTool.useWorldPos;


        public Vector3 GetPosVector(float x, float y, float z)
        {
            var xPos = x - xAmount / 2;
            var yPos = y - yAmount / 2;
            var zPos = z - zAmount / 2;
            var pos = new Vector3(xPos, yPos, zPos) * spacing + center;
            return pos;
        }

        private void ColliderBoundsPrintOut()
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