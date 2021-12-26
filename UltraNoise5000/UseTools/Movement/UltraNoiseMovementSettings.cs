using NoiseUltra.Output;
using UnityEngine;
namespace NoiseUltra.Movement
{
    [System.Serializable]
    public class UltraNoiseMovementSettings
    {
        public ExportNode movementNoise;
        public Vector3 maxMovement;
        public Vector3 noiseOffeset;

        public float moveSpeed;
    
        public Vector3 start;


        public void SetStart(Vector3 _start)
        {
            start = _start;
        }
    
        public Vector3 CalculateVector()
        {
            var xpos = movementNoise.GetSample(Time.time * moveSpeed + noiseOffeset.x) - 0.5f;
            var ypos = movementNoise.GetSample(Time.time * moveSpeed + noiseOffeset.y) - 0.5f;
            var zpos = movementNoise.GetSample(Time.time * moveSpeed + noiseOffeset.z) - 0.5f;

            var newpos = start + new Vector3(xpos * maxMovement.x, ypos * maxMovement.y, zpos * maxMovement.z);
        
            return newpos;
        }
    }
}