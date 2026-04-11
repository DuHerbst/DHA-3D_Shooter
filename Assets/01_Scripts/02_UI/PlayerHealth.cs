using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHealth = 5;
    public int currentHealth;

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
        currentHealth = maxHealth;
        UpdateHealthUI();
        
    }
    
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthUI();
        int randomIndex = Random.Range(0, ouchClips.Length);
        damageAudioSource.pitch = Random.Range(minPitch, maxPitch); // randomize pitch for variety
        damageAudioSource.PlayOneShot(ouchClips[randomIndex]);
        
        if (currentHealth <= 0)
        {
            currentHealth = 0; // to prevent health from going negative
            GameManager.instance.GameOver();
            damageAudioSource.PlayOneShot(deadClip); // play death sound
            
        }
        
    }

    void UpdateHealthUI()
    {
        
        for (int arrayIndex = 0; arrayIndex < hearts.Length; arrayIndex++) // we need to loop through each heart in the array (i means index in the array so start counting at 0 in the array, remember this!!!)
        {
            if (arrayIndex < currentHealth)
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
