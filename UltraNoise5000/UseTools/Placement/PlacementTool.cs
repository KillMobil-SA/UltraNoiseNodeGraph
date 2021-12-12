using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    public class PlacementTool : MonoBehaviour
    {
        private const string LIVE_PREVIEW_NAME = "Live Preview";
        private const string MODIFIERS_NAME = "Modifications";
        private const string GUI_COLOR_PREVIEW =
            "@UnityEngine.Color.Lerp(UnityEngine.Color.green, UnityEngine.Color.red, Mathf.InverseLerp(0 ,";

        #region Members

        [TableList]
        [SerializeField]
        private List<PlacementItem> placementItems = new List<PlacementItem>();
   

    
        [SerializeField]
        private bool useWorldCoordinates = true;

        [Header(MODIFIERS_NAME)]
        [SerializeField]
        private bool useAbsoluteCoordinates = false;

        private PlacementBounds m_MyPlacementBounds;

        private Vector3[] m_Positions;
        private Vector3[] m_Scale;

        private PlacementBounds myPlacementBounds
        {
            
            get
            {
                if (m_MyPlacementBounds == null)
                {
                    m_MyPlacementBounds = new PlacementBounds(placementAreaCollider);
                }

                return m_MyPlacementBounds;
            }
        }

        private Collider m_PlacementAreaCollider;

        private Collider placementAreaCollider
        {
            get
            {
                if (m_PlacementAreaCollider == null)
                {
                    m_PlacementAreaCollider = GetComponent<Collider>();
                }

                return m_PlacementAreaCollider;
            }
        }

        #endregion

        #region Public

        [Button]
        public void GenerateObjects()
        {
            m_ShowLivePreview = false;
            ClearObjects();
            PerformPlacement();
            
        }

        [Button]
        public void ClearObjects()
        {
            for (var i = 0; i < placementItems.Count; i++)
            {
                PlacementItem item = placementItems[i];
                PlacementSettings itemSettings = item.settings;
                itemSettings.CleanObjects(transform);
            }
            
        }

        private void Initialize()
        {
            for (var i = 0; i < placementItems.Count; i++)
            {
                PlacementItem item = placementItems[i];
                PlacementSettings itemSettings = item.settings;
                itemSettings.Initialize();
            }
        }

        #endregion

        #region Private


        public void PerformPlacement()
        {
            Initialize();
            int totalPlacementItems = placementItems.Count;
            for (var i = 0; i < totalPlacementItems; i++)
            {
                PlacementItem item = placementItems[i];
                if (!item.active)
                {
                    continue;
                }

                float spacing = item.spacing;
                if (spacing == 0)
                {
                    Debug.LogError("Spacing was set to zero!");
                    return;
                }

                myPlacementBounds.SetSpace(spacing);

                int xAmount = (int) myPlacementBounds.xAmount;
                int yAmount = (int) myPlacementBounds.yAmount;
                int zAmount = (int) myPlacementBounds.zAmount;

                if (xAmount < 1)
                {
                    xAmount = 1;
                }

                if (yAmount < 1)
                {
                    yAmount = 1;
                }

                if (zAmount < 1)
                {
                    zAmount = 1;
                }

                int index = 0;
                m_Positions = new Vector3[xAmount * yAmount * zAmount];
                m_Scale = new Vector3[xAmount * yAmount * zAmount];

                PlacementSettings settings = item.settings;
                if (settings.exportNode == null || settings == null)
                {
                    continue;
                }

                settings.SetPositions(ref m_Positions);
                settings.SetScale(ref m_Scale);

                for (var x = 0; x < xAmount; x++)
                {
                    for (var y = 0; y < yAmount; y++)
                    {
                        for (var z = 0; z < zAmount; z++)
                        {
                            Vector3 placementPos = new Vector3(x, y, z);
                            Vector3 pos = myPlacementBounds.GetPosVector(placementPos);

                            if (useAbsoluteCoordinates)
                            {
                                pos = new Vector3(Mathf.Abs(pos.x), Mathf.Abs(pos.y), Mathf.Abs(pos.z));
                            }

                            if (!useWorldCoordinates)
                            {
                                pos -= myPlacementBounds.center;
                            }

                            bool is2D = myPlacementBounds.heightIs2D;
                            float sample = is2D ? settings.GetSample(pos.x, pos.z) : settings.GetSample(pos);

                            if (sample == 0) continue;
                            
                            if (!settings.IsPositionValid(pos))
                            {
                                continue;
                            }

                            if (m_ShowLivePreview)
                            {
                                m_Positions[index] = settings.GetPos(pos, sample);
                                m_Scale[index] = settings.GetScale(pos, sample) * settings.livePreviewSizeMultiplier; 
                            }
                            else
                            {
                                settings.PlaceObject(pos, sample, transform);
                            }

                            index++;
                        }
                    }
                }
            }
        }

        public void OnDrawGizmos()
        {
            if (!m_ShowLivePreview)
            {
                return;
            }

            int countPos = m_Positions.Length;
            int countSettings = placementItems.Count;
            for (int i = 0; i < countSettings; ++i)
            {
                PlacementItem item = placementItems[i];
                for (int index = 0; index < countPos; ++index)
                {
                    item.settings.DebugObject(index);
                }
            }
            
        }

 
        #endregion

        #region LivePreview
        private bool m_ShowLivePreview;

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
        private void DisableGizmosPreview()
        {
            m_ShowLivePreview = false;
        }
        #endregion
    }
}