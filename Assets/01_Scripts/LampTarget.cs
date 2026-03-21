using UnityEngine;

public class LampTarget : MonoBehaviour, ITriggerTargets
{
    [SerializeField] private Light lampLight;
    [SerializeField] private Collider colliderLight;
    [SerializeField] private ParticleSystem lampParticles;

    void Start()
    {
        lampLight.enabled = false;
        colliderLight.enabled = false;
        lampParticles.Stop();
    }
    
    public void ActivateTargets()
    {
        lampLight.enabled = true;
        colliderLight.enabled = true;
        lampParticles.Play();
    }

    public void DeactivateTargets()
    {
        lampLight.enabled = false;
        colliderLight.enabled = false;
        lampParticles.Stop();
    }
}
