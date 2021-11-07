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
            ClearObjects();
            PerformPlacement();
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

        private void Initialize()
        {
            for (var i = 0; i < placementItems.Count; i++)
            {
                PlacementItem item = placementItems[i];
                PlacementSettings itemSettings = item.settings;
                itemSettings.Initialize(this);
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

                float xAmount = myPlacementBounds.xAmount;
                float yAmount = myPlacementBounds.yAmount;
                float zAmount = myPlacementBounds.zAmount;

                int index = 0;
                m_Positions = new Vector3[(int)(xAmount * zAmount)];
                m_Scale = new Vector3[(int)(xAmount * zAmount)];
                for (var x = 0; x < xAmount; x++)
                {
                    for (var y = 0; y < yAmount; y++)
                    {
                        for (var z = 0; z < zAmount; z++)
                        {
                            Vector3 placementPos = new Vector3(x, y, z);
                            PlacementSettings settings = item.settings;

                            if (settings.exportNode == null || settings == null)
                            {
                                continue;
                            }

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
                            
                            if (!settings.IsPositionValid(pos))
                            {
                                continue;
                            }

                            m_Positions[index] = settings.GetPos(pos, sample);
                            m_Scale[index] = settings.GetScale(pos, sample);

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

            int count = m_Positions.Length;
            for (int j = 0; j < count; ++j)
            {
                var pos = m_Positions[j];
                var scale = m_Scale[j];

                Gizmos.DrawCube(pos, scale);
            }
        }

        #endregion

        #region LivePreview
        private const float MAX_PREVIEW_RENDER_TIME = 1.5f;

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
            PerformPlacement();
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