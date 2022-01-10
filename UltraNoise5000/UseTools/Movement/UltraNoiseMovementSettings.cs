using NoiseUltra.Output;
using UnityEngine;
namespace NoiseUltra.Movement
{
    [System.Serializable]
    public class UltraNoiseMovementSettings
    {
        
        [SerializeField]private ExportNode movementNoise;
        [SerializeField]private Vector3 maxMovement;
        [SerializeField]private Vector3 noiseOffeset;

        [SerializeField]private float moveSpeed;
    
        [SerializeField]private Vector3 start;
        public float offset;

        public void SetStart(Vector3 _start)
        {
            start = _start;
        }
    
        public Vector3 CalculateVector()
        {
            var xpos = movementNoise.GetSample(offset + Time.time * moveSpeed + noiseOffeset.x) - 0.5f;
            var ypos = movementNoise.GetSample(offset + Time.time * moveSpeed + noiseOffeset.y) - 0.5f;
            var zpos = movementNoise.GetSample(offset + Time.time * moveSpeed + noiseOffeset.z) - 0.5f;

            var newpos = start + new Vector3(xpos * maxMovement.x, ypos * maxMovement.y, zpos * maxMovement.z);
        
            return newpos;
        }
    }
}