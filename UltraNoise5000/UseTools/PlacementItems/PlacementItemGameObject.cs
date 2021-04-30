using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace NoiseUltra.Tools.Placement
{

    [CreateAssetMenu(fileName = "UltraPlacementItemGameObject", menuName = "KillMobil/UltraNoise/Gameobjects")]
    public class PlacementItemGameObject : PlacementItemBase
    {
        public enum PlacementObjectType
        {
            Linear,
            Value,
            Random
        }

        [Header("GameObjects Placement Type Settings ")]
        [SerializeField]
        private GameObject[] items;
        

<<<<<<< HEAD:UltraNoise5000/UseTools/PlacementItems/PlacementItemGameObject.cs
        [SerializeField]
        private PlacementObjectType placementObjectType = PlacementObjectType.Linear;
        int linearID;
=======
        public PlacementObjectType placementObjectType = PlacementObjectType.Linear;
        private int linearID;
>>>>>>> f5ee208a90c9bc5a5c97c3c815672de79590a885:UltraNoise5000/UseTools/PlacementItems/Classes/UltraPlacementItemGameObject.cs

        public GameObject GetGameObject(float v)
        {
            var objectID = GetObjectID(v);
            var newPoolObject = items[objectID];
            return newPoolObject;
        }

        public override void PlaceObject(Vector3 pos, float v, Transform parent)
        {
            var placemntPos = GetPos(pos, v);
            var placemntScale = GetScale(pos, v);
            var placemntRot = GetRot(pos, v);
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

            newObject.transform.position = placemntPos;
            newObject.transform.localScale = placemntScale;
            newObject.transform.rotation = Quaternion.Euler(placemntRot);
        }

        public override void CleanObjects(Transform parent)
        {
            Debug.Log(string.Format("CleanObjects () - parent {0}", parent));

            var transforms = parent.GetComponentsInChildren<Transform>();

            var reformList = new List<Transform>();
            for (var i = 0; i < transforms.Length; i++)
                if (transforms[i].parent == parent)
                    reformList.Add(transforms[i]);


            for (var i = 0; i < reformList.Count; i++)
            {
#if UNITY_EDITOR
                DestroyImmediate(reformList[i].gameObject);
#else
                DestroyObject (reformList[i].gameObject);
#endif
            }
        }

        private string ObjectNamePrefix(Vector3 pos)
        {
            var prefix = name + "_" + pos.x + "_" + pos.y + "_" + pos.z;
            return prefix;
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
                default:
                case PlacementObjectType.Random:
                    returnID = Random.Range(0, items.Length - 1);
                    break;

                case PlacementObjectType.Value:
                    returnID = Mathf.FloorToInt((items.Length - 1) * v);
                    //Debug.Log(string.Format("returnID {0}", returnID));
                    break;
            }

            return returnID;
        }
    }
}