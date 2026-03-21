using UnityEngine;

public class LampTarget : MonoBehaviour, ITriggerTargets
{
    [SerializeField] private Light lampLight;
    [SerializeField] private Collider colliderLight;

    void Start()
    {
        lampLight.enabled = false;
        colliderLight.enabled = false;
    }
    
    public void ActivateTargets()
    {
        lampLight.enabled = true;
        colliderLight.enabled = true;
    }

    public void DeactivateTargets()
    {
        lampLight.enabled = false;
        colliderLight.enabled = false;
    }
}
