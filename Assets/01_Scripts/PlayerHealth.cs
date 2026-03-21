using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHealth = 5;
    private int _currentHealth;

    public Image[] hearts; // array to hold the heart images in the UI
    public Sprite fullHeart;
    public Sprite emptyHeart;
    
    // AUDIO
    [SerializeField] private AudioSource damageAudioSource;
    [SerializeField] private AudioClip[] ouchClips;
    [SerializeField] private AudioClip deadClip;
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;

    void Start()
    {
        _currentHealth = maxHealth;
        UpdateHealthUI();
        
    }

    void Update()
    {
        //DEBUG DELETE LATER!!!
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(1);
            Debug.Log("OUCH");
        }
    }
    
    public void TakeDamage(int damageAmount)
    {
        _currentHealth -= damageAmount;
        UpdateHealthUI();
        int randomIndex = UnityEngine.Random.Range(0, ouchClips.Length);
        damageAudioSource.pitch = UnityEngine.Random.Range(minPitch, maxPitch); // randomize pitch for variety
        damageAudioSource.PlayOneShot(ouchClips[randomIndex]);
        
        if (_currentHealth <= 0)
        {
            _currentHealth = 0; // to prevent health from going negative
            GameManager.Instance.GameOver();
            damageAudioSource.PlayOneShot(deadClip); // play death sound
            
        }
        
    }

    void UpdateHealthUI()
    {
        
        for (int arrayIndex = 0; arrayIndex < hearts.Length; arrayIndex++) // we need to loop through each heart in the array (i means index in the array so start counting at 0 in the array, remember this!!!)
        {
            if (arrayIndex < _currentHealth)
            {
                hearts[arrayIndex].sprite = fullHeart;
            }
            else
            {
                hearts[arrayIndex].sprite = emptyHeart;
            }
        }
        
    }
    
    
}
