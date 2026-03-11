using MagicPigGames;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class FearTracker : MonoBehaviour
{
    [SerializeField] private float maxFear = 100f;
    [SerializeField] private float currentFear;
    [SerializeField] private float fearIncreaseRate;
    [SerializeField] private float fearDecreaseRate;
    
    // AUDIO
    [SerializeField] private AudioSource scareAudio;
    [SerializeField] private AudioSource heartbeatAudio;
    [SerializeField] private float heartAudioThreshold = 0.4f; // percentage of fear at which heartbeat starts
    [SerializeField] private AudioSource breathingAudio;
    [SerializeField] private float breathAudioThreshold = 0.6f;
    [SerializeField] private VerticalProgressBar fearBar;
    
    // LIGHT ZONE COUNTER
    private int _lightZoneCounter;
    private IDamageable _damageable;
    
    // MORE SCARY FEEDBACK
    [SerializeField] private Volume globalVolume;
    private Vignette _vignette;
    
    void Start()
    {
        
        _damageable = GetComponent<IDamageable>();
        globalVolume.profile.TryGet(out _vignette);
        
    }

    void Update()
    {
        // if player is in light zone, decrease fear
        if (_lightZoneCounter > 0)
        {
            currentFear -= fearDecreaseRate * Time.deltaTime;
        }
        
        // if player is not in light zone, increase fear
        else
        { 
            currentFear += fearIncreaseRate * Time.deltaTime;
        }
        
        currentFear = Mathf.Clamp(currentFear, 0, maxFear); // safety check to prevent go into negatives or above max
        
        if (currentFear >= maxFear)
        {
            currentFear = maxFear;

            if (scareAudio != null)
            {
                scareAudio.PlayOneShot(scareAudio.clip);
            }
            
            if (_damageable != null)
            {
                _damageable.TakeDamage(1); // just in case!
            }
            
            currentFear = 0; // reset fear after taking damage
        }
        
        // update progress bar
        float fearPercentage = currentFear / maxFear;
        fearBar.SetProgress(fearPercentage);
        
        // audio management

        if (fearPercentage >= heartAudioThreshold) // if fear is above the threshold, play heartbeat audio
        {
            if (heartbeatAudio != null && !heartbeatAudio.isPlaying)
            {
                heartbeatAudio.Play();
            }
            
        }

        else 
        { 
            if (heartbeatAudio != null && heartbeatAudio.isPlaying) 
            { 
                heartbeatAudio.Stop();
            }
        }
        
        if (fearPercentage >= breathAudioThreshold) // if fear is above the threshold, play breathing audio
        {
            if (breathingAudio != null && !breathingAudio.isPlaying)
            {
                breathingAudio.Play();
            }
        }
        
        else
        {
            if (breathingAudio != null && breathingAudio.isPlaying)
            {
                breathingAudio.Stop();
            }
        }
        
        if (_vignette != null)
        {
            _vignette.intensity.value = fearPercentage * 0.5f;
        }
        
    }
    
    public void EnterLightZone() // logic to increase the counter
    {
        _lightZoneCounter++;
    }
    
    public  void ExitLightZone()
    {
        _lightZoneCounter--;
        if (_lightZoneCounter < 0)
        {
            _lightZoneCounter = 0;
        }
    }

}
