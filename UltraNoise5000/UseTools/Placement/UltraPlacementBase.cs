using System;
using System.Collections.Generic;
using System.Security;
using Sirenix.OdinInspector;
using UnityEngine;


namespace NoiseUltra.Tools.Placement
{
    public class UltraPlacementBase  : MonoBehaviour
    {
        #region Members
        public List<PlacementSettings> placementList;
        
        [SerializeField]
        public bool useWorldCoordinates = true;
        
        private PlacementBounds m_MyPlacementBounds;
        public PlacementBounds myPlacementBounds
        {
            get
            {
                if (m_MyPlacementBounds == null)
                    m_MyPlacementBounds = new PlacementBounds(placementAreaCollider);
                
                return m_MyPlacementBounds;
            }
        }

        private Collider m_PlacementAreaCollider;
        public Collider placementAreaCollider
        {
            get
            {
                if (m_PlacementAreaCollider == null)
                    m_PlacementAreaCollider = GetComponent<Collider>();
                return m_PlacementAreaCollider;
            }
        }


        
        #endregion

        #region Public

        [Button]
        public virtual void GenerateObjects()
        {
            m_ShowLivePreview = false;
            ClearObjects();
            PerformPlacement();
        }

        [Button]
        public void ClearObjects()
        {
            for (var i = 0; i < placementList.Count; i++)
                placementList[i].CleanObjects(transform);
        }

        public virtual void Initialize()
        {
            for (var i = 0; i < placementList.Count; i++)
                placementList[i].Initialize();
        }

        #endregion

        #region Private

        public virtual void PerformPlacement()
        { 
        }

        
        public void PlaceItem(int  index , Vector3 placementPos , PlacementSettings settings)
        {
            Vector3 pos = myPlacementBounds.GetPosVector(placementPos);
                            
            var noiseSamplingPos = !useWorldCoordinates ? pos - myPlacementBounds.center : pos;
                            
            bool is2D = myPlacementBounds.heightIs2D;
            float sample = is2D ? settings.GetSample(noiseSamplingPos.x, noiseSamplingPos.z) : settings.GetSample(noiseSamplingPos);

            if (sample == 0 || !settings.IsPositionValid(pos)) return;

            if (m_ShowLivePreview)
                settings.PlacePreviewGizmo(pos , sample ,index );
            else
                settings.PlaceObject(pos, sample, transform);
        }
        #endregion




        public bool CheckSettignsCondition(PlacementSettings settings)
        {
            if (settings == null)
            {
                Debug.LogError("Placement Settings was null");
                return false;
            }
            
            if (settings.spacing == 0)
            {
                Debug.LogError("Spacing was set to zero!");
                return false;
            }
               
            if (settings.exportNode == null)
            {
                Debug.LogError("No Settings Defined in Placement Settings SO");
                return false;
            }

            return true;
        } 
        
        
        
        #region LivePreview
        public void OnDrawGizmos()
        {
            if (!m_ShowLivePreview)
                return;
            
            for (var i = 0; i < placementList.Count; i++)
                placementList[i].DebugObject();
        }
        
        
        [HideInInspector]
        public bool m_ShowLivePreview;
        
        
        [Button]
        [HideIf(nameof(m_ShowLivePreview))]
        [PropertyOrder(2)]
        private void EnableGizmosPreview()
        {
            m_ShowLivePreview = true;
            PerformPlacement();
        }

        [Button]
        [ShowIf(nameof(m_ShowLivePreview))]
        [PropertyOrder(2)]
        private void RefreshGizmosPreview()
        {
            m_ShowLivePreview = true;
            PerformPlacement();
        }

        [Button]
        [ShowIf(nameof(m_ShowLivePreview))]
        [PropertyOrder(2)]
        public void DisableGizmosPreview()
        {
            m_ShowLivePreview = false;
        }
        #endregion
    }
}