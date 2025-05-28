using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public AudioSource[] audioSources;
    public float audioProximity = 5.0f;

    // Pick up and Level Completion Logic 
    public int currentPickups = 0;
    public int maxPickups = 5;
    public bool levelComplete = false;
    public Text pickupText;

    // Update is called once per frame
    void Update()
    {
        LevelCompleteCheck();
        updateGUI();
        PlayAudioSamples();
    }

    private void updateGUI()
    {
        if (pickupText == null)
        {
            Debug.LogError("pickupText is NULL!");
        }
        pickupText.text = "Pickups: " + currentPickups + "/" + maxPickups;
    }

    private void LevelCompleteCheck()
    {
        if (currentPickups >= maxPickups)
        {
            levelComplete = true;
        }
        else
        {
            levelComplete = false;
        }
    }

    private void PlayAudioSamples()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            AudioSource source = audioSources[i];

            // Check if the audio source has been destroyed
            if (source == null) continue;

            if (Vector3.Distance(player.transform.position, source.transform.position) < audioProximity)
            {
                if (!source.isPlaying)
                {
                    source.Play();
                }
            }
        }
    }

}

