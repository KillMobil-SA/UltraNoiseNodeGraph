using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using Unity.EditorCoroutines.Editor;
using UnityEditor;

#endif

namespace NoiseUltra.Tools.Placement
{
    [CreateAssetMenu(fileName = "UltraPlacementItemGameObject", menuName = "KillMobil/UltraNoise/Gameobjects")]
    public class PlacementItemGameObject : PlacementSettings
    {
        [Header("GameObjects Placement Type Settings ")] [SerializeField]
        private GameObject[] items;


        [SerializeField] private PlacementObjectType placementObjectType = PlacementObjectType.Linear;
        int linearID;

        public GameObject GetGameObject(float v)
        {
            int objectID = GetObjectID(v);
            GameObject newPoolObject = items[objectID];
            return newPoolObject;
        }

        private int GetObjectID(float v)
        {
            v = Mathf.Clamp01(v);
            int returnID = 0;
            switch (placementObjectType)
            {
                case PlacementObjectType.Linear:
                    returnID = linearID;
                    linearID++;
                    if (linearID >= items.Length)
                        linearID = 0;
                    break;
                default:
                case PlacementObjectType.Random:
                    returnID = Random.Range(0, items.Length - 1);
                    break;

                case PlacementObjectType.Value:
                    returnID = Mathf.FloorToInt((float) (items.Length - 1) * v);
                    //Debug.Log(string.Format("returnID {0}", returnID));
                    break;
            }

            return returnID;
        }
        
        
        
        private string ObjectNamePrefix(Vector3 pos)
        {
            string prefix = this.name + "_" + pos.x.ToString() + "_" + pos.y.ToString() + "_" + pos.z.ToString();
            return prefix;
        }
        
        public override void PlaceObject(Vector3 pos, float v, Transform parent)
        {
            var placementPos = GetPos(pos, v);
            var placementScale = GetScale(pos, v);
            var placementRot = GetRot(pos, v);
            var sourceGO = GetGameObject(v);
            GameObject newObject;

#if UNITY_EDITOR
            newObject = PrefabUtility.InstantiatePrefab(sourceGO, parent) as GameObject;
#else
        newObject = Instantiate (placementItem.GetGameObject (v)) as GameObject;
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
                if (transforms[i].parent == parent)
                    reformList.Add(transforms[i]);


            for (int i = 0; i < reformList.Count; i++)
            {
#if UNITY_EDITOR
                DestroyImmediate(reformList[i].gameObject);
#else
                DestroyObject (reformList[i].gameObject);
#endif
            }
        }




        private enum PlacementObjectType
        {
            Linear,
            Value,
            Random
        }
    }
}