using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [Header("Scene Transition Settings")]
    [Tooltip("Name of the scene to load after pressing Spacebar")]
    public string nextSceneName;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogWarning("No scene name specified in the inspector.");
            }
        }
    }
}