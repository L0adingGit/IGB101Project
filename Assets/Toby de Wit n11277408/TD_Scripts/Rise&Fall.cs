using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public float height = 3f;                 // How high the platform moves up
    public float cycleDuration = 2f;         // Time to go up and down (excluding buffer)
    public float bufferTime = 1f;            // Pause at top and bottom

    private Vector3 startPos;
    private float totalCycleTime;
    private float timer;

    void Start()
    {
        startPos = transform.position;
        totalCycleTime = (cycleDuration * 2f) + (bufferTime * 2f);
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = timer % totalCycleTime;

        float yOffset = 0f;

        if (t < cycleDuration) // Going up
        {
            float normalizedT = t / cycleDuration;
            yOffset = Mathf.Lerp(0f, height, normalizedT);
        }
        else if (t < cycleDuration + bufferTime) // Pause at top
        {
            yOffset = height;
        }
        else if (t < (2f * cycleDuration) + bufferTime) // Going down
        {
            float normalizedT = (t - cycleDuration - bufferTime) / cycleDuration;
            yOffset = Mathf.Lerp(height, 0f, normalizedT);
        }
        else // Pause at bottom
        {
            yOffset = 0f;
        }

        transform.position = startPos + new Vector3(0f, yOffset, 0f);
    }
}