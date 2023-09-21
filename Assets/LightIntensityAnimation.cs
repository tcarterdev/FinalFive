using UnityEngine;

public class LightIntensityAnimation : MonoBehaviour
{
    public Light targetLight;         // Reference to the Light component you want to animate.
    public float minIntensity = 1f;   // Minimum intensity value.
    public float maxIntensity = 5f;   // Maximum intensity value.
    public float animationSpeed = 2f; // Speed of the intensity animation.

    private float currentIntensity;   // Current intensity value.
    private bool increasing = true;   // Flag to track intensity increase or decrease.

    private void Start()
    {
        // Initialize the current intensity to the minimum value.
        currentIntensity = minIntensity;
    }

    private void Update()
    {
        // Update the light intensity based on the animation speed and direction.
        if (increasing)
        {
            currentIntensity += Time.deltaTime * animationSpeed;
            if (currentIntensity >= maxIntensity)
            {
                currentIntensity = maxIntensity;
                increasing = false;
            }
        }
        else
        {
            currentIntensity -= Time.deltaTime * animationSpeed;
            if (currentIntensity <= minIntensity)
            {
                currentIntensity = minIntensity;
                increasing = true;
            }
        }

        // Apply the current intensity to the target Light component.
        targetLight.intensity = currentIntensity;
    }
}
