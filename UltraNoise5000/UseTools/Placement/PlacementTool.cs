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
            ClearObjects();
            PerformPlacement(false);
            m_ShowLivePreview = false;
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

        #endregion

        #region Private


        private void PerformPlacement(bool isLivePreview)
        {
            float frameStart = Time.realtimeSinceStartup;
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

                for (var x = 0; x < xAmount; x++)
                {
                    for (var y = 0; y < yAmount; y++)
                    {
                        for (var z = 0; z < zAmount; z++)
                        {
                            Vector3 placementPos = new Vector3(x, y, z);
                            PlacementItem placedItem = placementItems[i];
                            placedItem.GenerateObject(myPlacementBounds, placementPos, isLivePreview, transform,
                                useWorldCoordinates, useAbsoluteCoordinates);

                            if (isLivePreview)
                            {
                                float frameEnd = Time.realtimeSinceStartup;
                                previewRenderTime = frameEnd - frameStart;
                                if (previewRenderTime > MAX_PREVIEW_RENDER_TIME && m_ShowLivePreview)
                                {
                                    Debug.LogError("Cannot Render Preview spacing is set too low");
                                    m_ShowLivePreview = false;
                                    return;
                                }
                            }
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

            PerformPlacement(true);
        }

        #endregion

        #region LivePreview
        private const float MAX_PREVIEW_RENDER_TIME = .5f;

        [Header(LIVE_PREVIEW_NAME)]
        [SerializeField]
        [ReadOnly]
        [ShowIf(nameof(m_ShowLivePreview))]
        [GUIColor(GUI_COLOR_PREVIEW + nameof(MAX_PREVIEW_RENDER_TIME) + ", " + nameof(previewRenderTime) + "))")]
        [PropertyOrder(1)]
        private float previewRenderTime;
        private bool m_ShowLivePreview;

        [Button]
        [HideIf(nameof(m_ShowLivePreview))]
        [PropertyOrder(2)]
        private void LivePreview()
        {
            m_ShowLivePreview = true;
        }

        [Button]
        [ShowIf(nameof(m_ShowLivePreview))]
        [PropertyOrder(2)]
        private void DisableLivePreview()
        {
            m_ShowLivePreview = false;
        }

        #endregion
    }
}