using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class UltraPlacementScaleSettings
{

    [Header("Scaling V2")] public float size = 1;
    public bool hasIndividualScale;
    public ScaleRange dynamicScaleRange ;
    public ScaleRange randomScaleRange;
    

    public Vector3 SizeCalculator(Vector3 pos, float thresHold)
    {
        Vector3 dynamicScale = Vector3.one;
        Vector3 randomScale = Vector3.one;
        
        if (hasIndividualScale)
        {
            // Dynamic Scale
            dynamicScale = dynamicScaleRange.GetPercSizeVector3(thresHold);
            //Random Scale
            randomScale = RandomScale();
        }
        else
        {
            // Dynamic Scale
            float size = dynamicScaleRange.GetPercSizeFloat(thresHold);
            dynamicScale = new Vector3(size, size, size);
            //Random Scale
            randomScale =  randomScaleRange.GetPercSizeVector3(Random.value) ;
        }
        
        Vector3 ScaleResult = (dynamicScale + randomScale) * size;
        return ScaleResult;
    }


    Vector3 RandomScale()
    {
        float xRandomScale = randomScaleRange.GetPercSizeFloat(Random.value , 0);
        float yRandomScale = randomScaleRange.GetPercSizeFloat(Random.value , 1);
        float zRandomScale = randomScaleRange.GetPercSizeFloat(Random.value , 2);
        return new Vector3(xRandomScale, yRandomScale, zRandomScale);
    }
    
    
    [System.Serializable]
    public class ScaleRange
    {
        public Vector3 minSizeV3 = Vector3.one;
        public Vector3 maxSizeV3 = Vector3.one;
        public float GetPercSizeFloat(float v , int axis = 0 )
        {
            return Mathf.Lerp (minSizeV3[axis], maxSizeV3[axis], v);
        }
        public Vector3 GetPercSizeVector3(float v)
        {
            return Vector3.Lerp( minSizeV3, maxSizeV3 , v);
        }
    }
    
}




    
/*
[Header ("Size Settings")]
public float size = 1;
public bool dynamicSize;

[Space (5)]
public bool randomfactor;
public float randomSizeAmount;

[Space (5)]
public bool hasIndvRandom;
public Vector3 indvRandomSize;
public bool hasMinMaxRandomSize;
[ShowIf ("hasMinMaxRandomSize")]
public Vector3 indvMinRandomSize;

public Vector3 SizeCalculatorVold (Vector3 pos, float thresHold) {

    float addSizeFactor = size;
    if (dynamicSize) {
        //float percSize = 0;//Mathf.InverseLerp(threshold.x, threshold.y, thresHold);
        float restofSize = (addSizeFactor) * thresHold; // (placementSettings.varFactor.Evaluate(1 - percSize));
        addSizeFactor = restofSize;
    }

    if (randomfactor) {
        addSizeFactor += Random.Range (0, randomSizeAmount * 100) / 100;
    }

    Vector3 randomScale = Vector3.one;
    if (hasIndvRandom) {

        float xMinScale = 0;
        float yMinScale = 0;
        float zMinScale = 0;

        if (hasMinMaxRandomSize) {
            xMinScale = indvMinRandomSize.x;
            yMinScale = indvMinRandomSize.y;
            zMinScale = indvMinRandomSize.z;
        }

        float xScale = Random.Range (xMinScale, indvRandomSize.x);
        float yScale = Random.Range (yMinScale, indvRandomSize.y);
        float zScale = Random.Range (zMinScale, indvRandomSize.z);
        randomScale = new Vector3 (xScale, yScale, zScale);
    }

    return (Vector3.one + randomScale) * addSizeFactor;
}
*/