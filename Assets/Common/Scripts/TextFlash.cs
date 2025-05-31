using UnityEngine;
using UnityEngine.UI;

public class FlashingText : MonoBehaviour
{
    [Header("Flash Timing Settings")]
    [Tooltip("How long the text stays visible (in seconds)")]
    public float visibleDuration = 1.0f;

    [Tooltip("How long the text stays hidden (in seconds)")]
    public float hiddenDuration = 1.0f;

    private Text textComponent;
    private float timer = 0f;
    private bool isVisible = true;

    void Start()
    {
        textComponent = GetComponent<Text>();
        if (textComponent == null)
        {
            Debug.LogError("FlashingText script requires a UnityEngine.UI.Text component.");
        }

        // Ensure the text starts visible
        isVisible = true;
        textComponent.enabled = true;
    }

    void Update()
    {
        if (textComponent == null) return;

        timer += Time.deltaTime;

        if (isVisible && timer >= visibleDuration)
        {
            textComponent.enabled = false;
            isVisible = false;
            timer = 0f;
        }
        else if (!isVisible && timer >= hiddenDuration)
        {
            textComponent.enabled = true;
            isVisible = true;
            timer = 0f;
        }
    }
}