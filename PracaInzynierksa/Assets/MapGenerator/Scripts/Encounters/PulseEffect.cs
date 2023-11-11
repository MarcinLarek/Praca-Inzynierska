using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseEffect : MonoBehaviour
{
    public float pulseSpeed = 0.8f; // Adjust the speed of the pulse effect
    public float minScale = 6.5f;   // Minimum scale of the sprite
    public float maxScale = 7.5f;   // Maximum scale of the sprite

    private void Update()
    {
        // Calculate the scale factor based on a sine wave to create the pulsating effect
        float scaleFactor = Mathf.Lerp(minScale, maxScale, Mathf.PingPong(Time.time * pulseSpeed, 1.0f));

        // Apply the scale to the sprite
        transform.localScale = new Vector3(scaleFactor, scaleFactor, 1.0f);
    }
}
