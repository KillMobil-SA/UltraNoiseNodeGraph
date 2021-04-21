using System.Collections;
using System.Collections.Generic;
using NoiseUltra;
using UnityEngine;
[System.Serializable]
public class NoiseHeightPos {

    public NoiseNodeBase heightNoise;
    public float heightAmount;
    public bool HeightCheck (Vector3 pos) {
        return true;
    }

    public float GetHeightPos (Vector3 pos) {
        if (heightNoise) {
            float v = heightNoise.Sample2D (pos.x, pos.z);
            return v * heightAmount;
        } else
            return 0;

    }

}