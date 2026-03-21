using System.Collections;   
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    [SerializeField] private float grabDelay;
    private float timer;
    private CharacterController _characterController;
    
    void OnTriggerEnter(Collider other)
    
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(GrabPlayer(other));
        }
    }
    
    IEnumerator GrabPlayer(Collider other)
    {
        yield return new WaitForSeconds(grabDelay);
        _characterController = other.GetComponent<CharacterController>();
        _characterController.enabled = false;
        
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