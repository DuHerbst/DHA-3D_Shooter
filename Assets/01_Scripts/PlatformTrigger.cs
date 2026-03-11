using System;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    
    {
        Debug.Log("Entered platform trigger " + other.name); // check what entered the platform trigger
        
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform.parent); // refers to the parent object of the trigger
        }
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