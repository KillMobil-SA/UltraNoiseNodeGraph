using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class UltraPlacementPositionSettings : ShowOdinSerializedPropertiesInInspectorAttribute
{
    public bool hasRandomPositioningSettings;
    public Vector3 randomPositioning;

    [Header("Height Calulations Settings")]
    public HeightPosType heightPosType = HeightPosType.Grid;

    [ShowIf("heightPosType", HeightPosType.Raycast)]
    public RayCastHeightPos rayCastHeightPos = new RayCastHeightPos();

    [ShowIf("heightPosType", HeightPosType.Noise)]
    public NoiseHeightPos noiseHeightPos = new NoiseHeightPos();

    public Vector3 PotisionCalculator(Vector3 pos, float thresHold)
    {
        var sourceHeightCalculation = new Vector3(pos.x, CalculateHeight(pos), pos.z);

        var xPos = Random.Range(-randomPositioning.x, randomPositioning.x);
        var yPos = Random.Range(-randomPositioning.y, randomPositioning.y);
        var zPos = Random.Range(-randomPositioning.z, randomPositioning.z);
        var randomPosition = new Vector3(xPos, yPos, zPos);

        return sourceHeightCalculation + randomPosition;
    }

    private float CalculateHeight(Vector3 pos)
    {
        switch (heightPosType)
        {
            case HeightPosType.Grid:
                return pos.y;
            case HeightPosType.Noise:
                return noiseHeightPos.GetHeightPos(pos);
            case HeightPosType.Raycast:
                return rayCastHeightPos.GetHeightPos(pos);
            default:
                return pos.y;
        }
    }

    public bool ChechPos(Vector3 pos)
    {
        switch (heightPosType)
        {
            case HeightPosType.Grid:
                return true;
            case HeightPosType.Noise:
                return true;
            //return noiseHeightPos.HeightCheck (pos);
            case HeightPosType.Raycast:
                return rayCastHeightPos.HeightCheck(pos);
            default:
                return true;
        }
    }
}