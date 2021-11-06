using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    public interface IHeightBase
    {
        bool HeightCheck(Vector3 pos);
        float GetHeightPos(Vector3 pos);
    }
}