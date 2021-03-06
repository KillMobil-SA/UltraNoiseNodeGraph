using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace NoiseUltra.Tools.Placement
{
    public enum PlacementObjectType
    {
        Linear,
        Value,
        Random
    }

    [CreateAssetMenu(fileName = "UltraPlacementItemGameObject", menuName = "KillMobil/UltraNoise/Gameobjects")]
    public class PlacementItemGameObject : PlacementSettings
    {
        [TitleGroup("GameObjects Settings")]
        [EnumToggleButtons , HideLabel]
        [SerializeField] private PlacementObjectType placementObjectType = PlacementObjectType.Linear;
        [AssetsOnly, SerializeField, Space]
        private GameObject[] items;

        
        private int linearID;


        #region Private
        private GameObject GetGameObject(float v)
        {
            var objectID = GetObjectID(v);
            var newPoolObject = items[objectID];
            return newPoolObject;
        }

        private int GetObjectID(float v)
        {
            v = Mathf.Clamp01(v);
            var returnID = 0;
            switch (placementObjectType)
            {
                case PlacementObjectType.Linear:
                    returnID = linearID;
                    linearID++;
                    if (linearID >= items.Length)
                        linearID = 0;
                    break;
                case PlacementObjectType.Value:
                    returnID = Mathf.FloorToInt((items.Length - 1) * v);
                    //Debug.Log(string.Format("returnID {0}", returnID));
                    break;
                case PlacementObjectType.Random:
                default:
                    returnID = Random.Range(0, items.Length - 1);
                    break;
            }

            return returnID;
        }

        private string ObjectNamePrefix(Vector3 pos)
        {
            var prefix = name + "_" + pos.x + "_" + pos.y + "_" + pos.z;
            return prefix;
        }
        
        #endregion

        #region Public
        public override void PlaceObject(Vector3 pos, float v, Transform parent)
        {
            Vector3 placementPos = GetPos(pos, v);
            Vector3 placementScale = GetScale(pos, v);
            if (placementScale.magnitude <= 0)
            {
                return;
            }
            
            Vector3 placementRot = GetRot(pos, v);
            GameObject sourceGO = GetGameObject(v);
            GameObject newObject = null;

#if UNITY_EDITOR
            newObject = PrefabUtility.InstantiatePrefab(sourceGO, parent) as GameObject;
#else
            newObject = Instantiate (sourceGO) as GameObject;
            newObject.transform.parent = parent;
#endif

            newObject.SetActive(true);
            newObject.name = ObjectNamePrefix(pos);

            newObject.transform.position = placementPos;
            newObject.transform.localScale = placementScale;
            newObject.transform.rotation = Quaternion.Euler(placementRot);
        }

        public override void CleanObjects(Transform parent)
        {
            Transform[] transforms = parent.GetComponentsInChildren<Transform>();
            List<Transform> reformList = new List<Transform>();
            for (int i = 0; i < transforms.Length; i++)
            {
                if (transforms[i].parent == parent)
                {
                    reformList.Add(transforms[i]);
                }
            }
            
            for (var i = 0; i < reformList.Count; i++)
            {
#if UNITY_EDITOR
                DestroyImmediate(reformList[i].gameObject);
#else
                DestroyObject (reformList[i].gameObject);
#endif
            }
        }
        #endregion
    }
}