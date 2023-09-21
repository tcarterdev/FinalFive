using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [Header("Bobbing Parameters")]
    public float bobFrequency = 1.5f;     // Frequency of the headbob motion.
    public float bobAmplitude = 0.1f;     // Amplitude of the headbob motion.

    [Header("Movement Parameters")]
    public float moveThreshold = 0.1f;    // Minimum movement required to trigger headbob.

    private Vector3 initialPosition;
    private float timer = 0f;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        // Get the player's input for movement.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the magnitude of the player's movement input.
        float movementMagnitude = new Vector2(horizontalInput, verticalInput).magnitude;

        // Check if the player is moving and the movement magnitude exceeds the threshold.
        if (movementMagnitude > moveThreshold)
        {
            // Calculate the headbob motion based on time.
            float bobX = Mathf.Sin(timer * bobFrequency) * bobAmplitude;
            float bobY = Mathf.Cos(timer * bobFrequency * 2f) * bobAmplitude * 0.5f;

            // Apply the headbob motion to the camera's local position.
            transform.localPosition = initialPosition + new Vector3(bobX, bobY, 0f);

            // Increment the timer for the bobbing effect.
            timer += Time.deltaTime;
        }
        else
        {
            // Reset the timer and the camera's local position when not moving.
            timer = 0f;
            transform.localPosition = initialPosition;
        }
    }
}
