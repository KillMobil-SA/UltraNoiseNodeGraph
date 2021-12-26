using System.Collections;
using System.Collections.Generic;
using NoiseUltra.Output;
using UnityEngine;

namespace NoiseUltra.Movement
{
    public class UltraNoiseMovement : MonoBehaviour
    {
        // Start is called before the first frame update

        public UltraNoiseMovementSettings movementSettings;
        public UltraNoiseMovementSettings rotationSettings;

        void Start()
        {
            movementSettings.SetStart(transform.position);
            rotationSettings.SetStart(transform.rotation.eulerAngles);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            transform.position = movementSettings.CalculateVector();
            transform.rotation = Quaternion.Euler(rotationSettings.CalculateVector());
        }
    }
}

