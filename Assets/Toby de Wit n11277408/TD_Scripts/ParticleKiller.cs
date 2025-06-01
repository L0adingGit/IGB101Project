using UnityEngine;

public class DisableParticlesOnWoolooDeath : MonoBehaviour
{
    private ParticleSystem particles;
    private bool woolooGone = false;

    void Start()
    {
        particles = GetComponent<ParticleSystem>();

        if (particles == null)
        {
            Debug.LogWarning("No ParticleSystem found on this GameObject.");
        }
    }

    void Update()
    {
        if (!woolooGone)
        {
            GameObject wooloo = GameObject.Find("Wooloo");

            if (wooloo == null || !wooloo.activeInHierarchy)
            {
                woolooGone = true;
                if (particles != null && particles.isPlaying)
                {
                    particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                    particles.Clear();  // Ensures all live particles are removed immediately
                }
            }
        }
    }
}