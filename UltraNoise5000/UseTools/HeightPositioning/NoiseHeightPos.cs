using NoiseUltra.Nodes;
using UnityEngine;
[System.Serializable]
public class NoiseHeightPos {

    public NodeBase height;
    public float heightAmount;
    public bool HeightCheck (Vector3 pos) {
        return true;
    }

    public float GetHeightPos (Vector3 pos) {
        if (height) {
            float v = height.Sample2D (pos.x, pos.z);
            return v * heightAmount;
        } else
            return 0;

    }

}