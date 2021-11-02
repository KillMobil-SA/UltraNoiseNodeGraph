using System.Collections.Generic;
using JetBrains.Annotations;
using NoiseUltra.Generators;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    public class PlacementTool : MonoBehaviour
    {
        
        
        
        [SerializeField] [TableList()]
        private List<PlacementItem> generatorItems = new List<PlacementItem>();

        
        [SerializeField]
        public bool useWorldCordinates = true;

        [Header("Modifications")] [SerializeField]
        private bool cordinatesAbs = false;


        [Header  ("Live Preview")]
        [SerializeField,ReadOnly , ShowIf(nameof(showLivePreview))]
        [GUIColor("@UnityEngine.Color.Lerp(UnityEngine.Color.green, UnityEngine.Color.red, Mathf.InverseLerp(0 ," + nameof(MAXPreviewRenderTime) + ", " + nameof(previewRenderTime)+"))")]
        [PropertyOrder(1)]
        private float previewRenderTime;

        private bool showLivePreview;
        private const float MAXPreviewRenderTime = .5f;
        
        private PlacementBounds _myPlacementBounds;

        private Collider _placementAreaCollider;

        private PlacementBounds myPlacementBounds
        {
            get
            {
                
                if (_myPlacementBounds == null)
                    _myPlacementBounds = new PlacementBounds(this, placementAreaCollider);
                return _myPlacementBounds;
            }
        }

        private Collider placementAreaCollider
        {
            get
            {
                if (_placementAreaCollider == null)
                    _placementAreaCollider = GetComponent<Collider>();
                return _placementAreaCollider;
            }
        }

        
        private void OnDrawGizmos()
        {
            if (!showLivePreview) 
                return;
            PerformPlacement(true);
        }

        [Button]
        public void GenerateObjects()
        {
            ClearObjects();
            PerformPlacement(false);
            showLivePreview = false;
            Color red = UnityEngine.Color.Lerp(Color.black, Color.yellow, 0.5f);
        }

        [Button]
        public void ClearObjects()
        {
            for (var i = 0; i < generatorItems.Count; i++) generatorItems[i].plamentHandler.CleanObjects(transform);
        }

        private void PerformPlacement(bool isLivePreview)
        {
            InitPlacement();
            float frameStart = Time.realtimeSinceStartup; 
            for (var i = 0; i < generatorItems.Count; i++)
            {
                if (!generatorItems[i].active) 
                    continue;
                
                var spacing = generatorItems[i].spacing;
                if (spacing == 0)
                { 
                    Debug.LogError("Spacing was set to zero!");
                    return;
                }
                myPlacementBounds.SetSpace(spacing);
                

                for (var x = 0; x < myPlacementBounds.xAmount; x++)
                for (var y = 0; y < myPlacementBounds.yAmount; y++)
                for (var z = 0; z < myPlacementBounds.zAmount; z++)
                {
                    var placementPos = new Vector3(x, y, z);
                    var placedItem = generatorItems[i];
                    placedItem.GenerateObject(myPlacementBounds, placementPos, isLivePreview, transform ,useWorldCordinates ,  cordinatesAbs);

                    if (isLivePreview)
                    {
                        float frameEnd = Time.realtimeSinceStartup;
                        previewRenderTime = frameEnd - frameStart;
                        if (previewRenderTime > MAXPreviewRenderTime && showLivePreview)
                        {
                            Debug.LogError("Cannot Render Preview spacing is set too low");
                            showLivePreview = false;
                            return;
                        }
                    }
                }
            }
        }

        
        
        private void InitPlacement()
        {
            for (var i = 0; i < generatorItems.Count; i++)
                generatorItems[i].plamentHandler.InitializeProperties();
        }


        [Button , HideIf(nameof(showLivePreview))]
        [PropertyOrder(2)]
        private void LivePreview()
        {
            showLivePreview = true;
        }

        [Button, ShowIf(nameof(showLivePreview))]
        [PropertyOrder(2)]
        private void DisableLivePreview()
        {
            showLivePreview = false;
        }


    }
}