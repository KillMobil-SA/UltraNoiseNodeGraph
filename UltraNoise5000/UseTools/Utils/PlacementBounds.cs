using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    public sealed class PlacementBounds
    {
        private readonly Collider m_PlacementCollider;
        private float m_Spacing;

        public PlacementBounds(Collider placementCollider)
        {
            m_PlacementCollider = placementCollider;
        }

        public float xAmount
        {
            get
            {
                var v = m_PlacementCollider.bounds.size.x / m_Spacing;
                //TODO: proper comparison between float variables
                if (v == 0)
                {
                    v = 1;
                }
                return v;
            }
        }
        
        public float yAmount
        {
            get
            {
                var v = m_PlacementCollider.bounds.size.y / m_Spacing;
                //TODO: proper comparison between float variables
                if (v == 0)
                {
                    v = 1;
                }
                return v;
            }
        }

        public float zAmount
        {
            get
            {
                float v = m_PlacementCollider.bounds.size.z / m_Spacing;
                //TODO: proper comparison between float variables
                if (v == 0)
                {
                    v = 1;
                }
                return v;
            }
        }

        public float Volume
        {
            get
            {
                return (xAmount * yAmount * zAmount);
            }
        }
        
        public int VolumeInt
        {
            get
            {
                return  Mathf.RoundToInt(Volume);
            }
        }

        public bool heightIs2D => yAmount <= 1;
        
        public Vector3 center
        {
            get
            {
                Vector3 boundsCenter = m_PlacementCollider.bounds.center;
                float x = Mathf.Round(boundsCenter.x / m_Spacing) * m_Spacing;
                float y = Mathf.Round(boundsCenter.y);
                float z = Mathf.Round(boundsCenter.z / m_Spacing) * m_Spacing;
                Vector3 centerRound = new Vector3(x, y, z);
                return centerRound;
            }
        }

        public void SetSpace(float spacing)
        {
            m_Spacing = spacing;
        }


        public Vector3 GetPosVector(Vector3 pos)
        {
            return GetPosVector(pos.x, pos.y, pos.z);
        }


        public Vector3 GetPosVector(float x, float y, float z)
        {
            float xPos = x - xAmount / 2;
            float yPos = y - yAmount / 2;
            float zPos = z - zAmount / 2;
            Vector3 pos = new Vector3(xPos, yPos, zPos) * m_Spacing + center;
            return pos;
        }


        private void Print()
        {
            Vector3 center = m_PlacementCollider.bounds.center;
            Vector3 size = m_PlacementCollider.bounds.size;
            Vector3 min = m_PlacementCollider.bounds.min;
            Vector3 max = m_PlacementCollider.bounds.max;
            string msg = $"Collider Center : {center}" +
                         $"Collider Size : {size}" +
                         $"Collider bound Minimum : {min}" +
                         $"Collider bound Maximum : {max}";
            Debug.Log(msg);
        }
    }
}