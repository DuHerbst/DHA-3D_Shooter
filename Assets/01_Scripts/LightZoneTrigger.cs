using UnityEngine;

public class LightZoneTrigger : MonoBehaviour
{
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FearTracker fearTracker = other.gameObject.GetComponent<FearTracker>();

            if (fearTracker != null) // check check check always check!
            {
                fearTracker.EnterLightZone(); // call the method from tracker script
                Debug.Log(gameObject.name + " entered light zone trigger "); // check what entered the light zone trigger
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FearTracker fearTracker = other.gameObject.GetComponent<FearTracker>();

            if (fearTracker != null)
            {
                fearTracker.ExitLightZone();
                Debug.Log(gameObject.name + " exited light zone trigger ");
            }
        }

    }
    
    
}
