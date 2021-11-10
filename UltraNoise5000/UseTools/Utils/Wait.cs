using UnityEngine;

namespace NoiseUltra.Tools
{
    public static class Wait
    {
        public static WaitForSecondsRealtime CreateWaitRealtime(float realtime) => new WaitForSecondsRealtime(realtime);
    }
}