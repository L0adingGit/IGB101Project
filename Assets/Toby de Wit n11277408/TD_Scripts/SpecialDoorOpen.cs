using UnityEngine;

public class DoorTestSpecial : MonoBehaviour
{
    [Header("Proximity Settings")]
    public Transform player;             // Reference to player (can assign manually or auto)
    public float activationDistance = 3f; // Distance within which the player can open the door

    private Animation animation;

    void Start()
    {
        animation = GetComponent<Animation>();

        // Auto-find player if not set
        if (player == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && player != null)
        {
            float distance = Vector3.Distance(player.position, transform.position);

            if (distance <= activationDistance)
            {
                animation.Play();
            }
        }
    }
}