 using System.Collections;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour, ITrigger
{
    
    [SerializeField] private float activeTime = 5f;
    [SerializeField] private string arrow = "Arrow";
    private bool _isActive = false;
    
    public GameObject[] linkedTargets;
        
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        
        if (other.CompareTag(arrow) && !_isActive)
        {
            Debug.Log(other.gameObject.name);
            _isActive = true;
            OnActivate();
        }
        
    }

    public void OnActivate()
    {
            
        foreach (GameObject target in linkedTargets)
        {
            ITriggerTargets targets = target.GetComponent<ITriggerTargets>(); // check if the target has the ITriggerTargets interface
            
            if (targets != null) // if it's not null, then activate the targets
            {
                targets.ActivateTargets();
                Debug.Log("Activated "); // check what targets are activated
            }
            
        }
        
        StartCoroutine(ActivationRoutine()); // start the timer coroutine
        Debug.Log("Courotine started ");
        
        
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator ActivationRoutine()
    {
        yield return new WaitForSeconds(activeTime);
        OnDeactivate();
        _isActive = false;
        
    }

    public void OnDeactivate()
    {
        foreach (GameObject target in linkedTargets)
        {
            ITriggerTargets targets = target.GetComponent<ITriggerTargets>(); 
            
            if (targets != null)
            {
                targets.DeactivateTargets();
            }
            
        }
    }
    
    
}
