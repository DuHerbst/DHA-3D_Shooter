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
            }
        }

    }
    
    
}
