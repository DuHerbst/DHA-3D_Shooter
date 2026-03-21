using System.Collections;
using UnityEngine;
using DG.Tweening;
 

public class ButtonTrigger : MonoBehaviour, ITrigger
{
    
    [SerializeField] private float activeTime = 5f;
    [SerializeField] private string arrow = "Arrow";
    private bool _isActive = false;
    
    public GameObject[] linkedTargets;
    
    //ANIMATIONS
    [SerializeField] private Transform buttonLocation;
    [SerializeField] private Renderer buttonRenderer;
    [SerializeField] private float pressDepth;
    [SerializeField] private float animationDuration = 0.5f;

    [SerializeField] private Color buttonPressedColor;
    [SerializeField] private Color buttonIdleColour;
    
    private Vector3 _startPosition;
    
    // AUDIO
    [SerializeField] private AudioSource buttonAudioSource;
    [SerializeField] private AudioClip buttonOnClip;
    [SerializeField] private AudioClip buttonOffClip;
    [SerializeField] private AudioClip buttonTimerClip;


    void Start()
    {
        _startPosition = buttonLocation.localPosition;
        buttonRenderer.material.color = buttonIdleColour;
        
    }
        
    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag(arrow) && !_isActive)
        {
            _isActive = true;
            OnActivate();
        }
        
    }

    public void OnActivate()
    {
        // animations

        Vector3 pressedPosition = _startPosition;
        pressedPosition.y -= pressDepth;

        buttonLocation.DOLocalMove(pressedPosition, animationDuration).SetEase(Ease.InOutQuad);
        buttonRenderer.material.DOColor(buttonPressedColor, animationDuration);

        if (buttonAudioSource != null && buttonOnClip != null)
        {
            buttonAudioSource.PlayOneShot(buttonOnClip);
        }
        
        
        foreach (GameObject target in linkedTargets)
        {
            ITriggerTargets targets = target.GetComponent<ITriggerTargets>(); // check if the target has the ITriggerTargets interface
            
            if (targets != null) // if it's not null, then activate the targets
            {
                targets.ActivateTargets();
            }
            
        }
        
        StartCoroutine(ActivationRoutine()); // start the timer coroutine
        
        
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator ActivationRoutine()
    {
        buttonAudioSource.PlayOneShot(buttonTimerClip);
        yield return new WaitForSeconds(activeTime);
        OnDeactivate();
        _isActive = false;
        
    }

    public void OnDeactivate()
    {
        
        buttonLocation.DOLocalMove(_startPosition, animationDuration).SetEase(Ease.InOutQuad);
        buttonRenderer.material.DOColor(buttonIdleColour, animationDuration);
        
        if (buttonAudioSource != null && buttonOffClip != null)
        {
            buttonAudioSource.PlayOneShot(buttonOffClip);
        }
        
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
