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
        Debug.Log("Entered platform trigger " + other.name); // check what entered the platform trigger
        
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
        Debug.Log("Disabled character controller of " + other.name);
        
        other.transform.SetParent(transform.parent); // refers to the parent object of the trigger
    }
    
    void OnTriggerExit(Collider other)
    
    {
        Debug.Log("Exited platform trigger " + other.name);
        
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null); // the parent of the player is now null so it can get off the platform trigger
        }
    }
    
}