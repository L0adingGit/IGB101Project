using UnityEngine;

public class PickUp : MonoBehaviour
{
    GameManager gameManager;
    private AudioSource pickupAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        pickupAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.CompareTag("Player"))
        {
            gameManager.currentPickups += 1;

            // Play the pickup sound
            if (pickupAudio != null)
            {
                pickupAudio.Play();

                // Disable visuals and collider immediately (optional but cleaner)
                GetComponent<Collider>().enabled = false;
                foreach (Renderer r in GetComponentsInChildren<Renderer>())
                    r.enabled = false;

                // Destroy the object after the clip finishes playing
                Destroy(gameObject, pickupAudio.clip.length);
            }
            else
            {
                Destroy(gameObject); // Fallback in case AudioSource is missing
            }
        }
    }
}