﻿using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    public class PlacementSettings : ScriptableObject
    {
        [Header("Placement Settings")] [TabGroup("Placement Settings", "Size")] [SerializeField]
        private ScaleSettings placementSettings;

        [TabGroup("Placement Settings", "Rotation")] [SerializeField]
        private RotationSettings placementRotationSettings;

        [TabGroup("Placement Settings", "Position")] [SerializeField]
        private PositionSettings placementPositionsSettings;

        public Color debugColor;
        public float debugSizeMultiplier = 1;

        public bool ChechPos(Vector3 pos) => placementPositionsSettings.ChechPos(pos);
        public Vector3 GetPos(Vector3 pos, float v) => placementPositionsSettings.PotisionCalculator(pos, v);
        public Vector3 GetRot(Vector3 pos, float v) => placementRotationSettings.RotationCalculator(pos, v);
        public Vector3 GetScale(Vector3 pos, float v) => placementSettings.SizeCalculator(pos, v);

        private void OnEnable()
        {
            placementPositionsSettings.OnEnable();
        }

        public virtual void PlaceObject(Vector3 pos, float v, Transform parent)
        {
        }

        public virtual void CleanObjects(Transform parent)
        {
        }

        public void DebugObject(Vector3 pos, float v)
        {
            Vector3 placemntPos = GetPos(pos, v);
            Vector3 placemntScale = GetScale(pos, v) * debugSizeMultiplier;

            Gizmos.color = debugColor;
            Gizmos.DrawCube(placemntPos, placemntScale);
            Gizmos.color = Color.white;
        }
    }
}