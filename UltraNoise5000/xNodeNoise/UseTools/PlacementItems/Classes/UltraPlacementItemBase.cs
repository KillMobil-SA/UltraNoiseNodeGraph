using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class UltraPlacementItemBase : SerializedScriptableObject {
    [Header ("Placement Settings")]

    [TabGroup ("Placement Settings", "Size")]
    public UltraPlacementScaleSettings placementSettings;
    [TabGroup ("Placement Settings", "Rotation")]
    public UltraPlacementRotationSettings placementRotationSettings;
    [TabGroup ("Placement Settings", "Position")]
    public UltraPlacementPositionSettings placementPositionsSettings;
    [TabGroup ("Placement Settings", "Coloring")]
    public UltraPlacementColorSettings placementColorSettings;

    public Color debugColor;
    public float debugSizeMultiplier = 1;

    public bool ChechPos (Vector3 pos) => placementPositionsSettings.ChechPos (pos);
    public Vector3 GetPos (Vector3 pos, float v) => placementPositionsSettings.PotisionCalculator (pos, v);
    public Vector3 GetRot (Vector3 pos, float v) => placementRotationSettings.RotationCalculator (pos, v);
    public Vector3 GetScale (Vector3 pos, float v) => placementSettings.SizeCalculator (pos, v);

    public virtual void PlaceObject (Vector3 pos, float v, Transform parent) { }
    public virtual void CleanObjects (Transform parent) { }

    public void DebugObject (Vector3 pos, float v) {
        Vector3 placemntPos = GetPos (pos, v);
        Vector3 placemntScale = GetScale (pos, v) * debugSizeMultiplier;

        Gizmos.color = debugColor;
        Gizmos.DrawCube (placemntPos, placemntScale);
        Gizmos.color = Color.white;

    }

}