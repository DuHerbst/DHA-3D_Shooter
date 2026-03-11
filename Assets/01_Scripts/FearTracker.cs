using MagicPigGames;
using UnityEngine;

public class FearTracker : MonoBehaviour
{
    [SerializeField] private float maxFear = 100f;
    [SerializeField] private float currentFear;
    [SerializeField] private float fearIncreaseRate;
    [SerializeField] private float fearDecreaseRate;
    
    // AUDIO
    [SerializeField] private AudioSource scareAudio;
    [SerializeField] private VerticalProgressBar fearBar;
    
    // LIGHT ZONE COUNTER
    private int _lightZoneCounter;
    private IDamageable _damageable;
    
    void Start()
    {
        
        _damageable = GetComponent<IDamageable>();
        
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
                scareAudio.Play();
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
