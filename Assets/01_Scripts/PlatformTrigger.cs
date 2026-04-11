using System.Collections;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    [SerializeField] private float grabDelay;
    private float _timer;
    private CharacterController _characterController;
    
    void OnTriggerEnter(Collider other)
    
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(GrabPlayer(other));
        }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator GrabPlayer(Collider other)
    {
        yield return new WaitForSeconds(grabDelay);
        other.transform.SetParent(transform.parent); // refers to the parent object of the trigger
    }
    
    void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null); // the parent of the player is now null so it can get off the platform trigger
        }
    }
    
}