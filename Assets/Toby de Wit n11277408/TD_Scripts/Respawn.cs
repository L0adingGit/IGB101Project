using UnityEngine;
using System.Collections;
using Invector.vCharacterController;

public class ResetPlayer : MonoBehaviour
{
    public Transform spawnPoint;

    private Collider playerCollider;
    private bool needsReset = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCollider = other;
            needsReset = true;
        }
    }

    private void FixedUpdate()
    {
        if (needsReset && playerCollider != null)
        {
            TeleportAndReset(playerCollider);
            needsReset = false;
            playerCollider = null;
        }
    }

    private void TeleportAndReset(Collider other)
    {
        var controller = other.GetComponent<vThirdPersonController>();
        var motor = other.GetComponent<vThirdPersonMotor>();
        var input = other.GetComponent<vThirdPersonInput>();
        var camera = other.GetComponent<vThirdPersonCamera>();
        var animator = other.GetComponent<Animator>();
        var thirdPersonAnimator = other.GetComponent<vThirdPersonAnimator>();
        var charController = other.GetComponent<CharacterController>();
        var rb = other.GetComponent<Rigidbody>();

        // Disable all relevant components
        if (controller != null) controller.enabled = false;
        if (motor != null) motor.enabled = false;
        if (input != null) input.enabled = false;
        if (camera != null) camera.enabled = false;
        if (animator != null) animator.enabled = false;
        if (thirdPersonAnimator != null) thirdPersonAnimator.enabled = false;
        if (charController != null) charController.enabled = false;
        if (rb != null) rb.isKinematic = true;

        // Reset movement state
        if (controller != null)
        {
            controller.ResetMovementState();
            controller.useRootMotion = false;
        }

        // Reset animator state
        if (animator != null)
        {
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, -1, 0f);
            animator.Update(0f);
        }

        // Reset Rigidbody velocity if present
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Teleport player
        other.transform.position = spawnPoint.position;

        // Re-enable CharacterController & Rigidbody
        if (charController != null) charController.enabled = true;
        if (rb != null) rb.isKinematic = false;

        // Start coroutine to re-enable other components after delay
        StartCoroutine(ReenableAfterDelay(controller, motor, input, camera, animator, thirdPersonAnimator));
    }

    private IEnumerator ReenableAfterDelay(params Behaviour[] components)
    {
        yield return null; // wait one frame

        foreach (var comp in components)
        {
            if (comp != null)
            {
                if (comp is vThirdPersonController controller)
                    controller.useRootMotion = true;

                comp.enabled = true;
            }
        }
    }
}