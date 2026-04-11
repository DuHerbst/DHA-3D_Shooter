using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected NavMeshAgent agent; // reference to nav mesh created
    
    //enemy health
    [SerializeField] private int enemyMaxHealth;
    private int _enemyCurrentHealth;
    private bool _isDead;
    
    //audio
    [SerializeField] private AudioSource deathAudioSource;
    [SerializeField] private AudioClip deathClip;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _enemyCurrentHealth = enemyMaxHealth; //set health on start
    }
    
    public void TakeDamage(int damageAmount)
    { 
        if (_isDead) return;
        
        _enemyCurrentHealth -= damageAmount; 
        
        if (_enemyCurrentHealth <= 0)
            
        {
          Die();
        }
      
    }
    
    private void Die()
    {
        if (_isDead)
        {
            return;
        }
        
        _isDead = true;
        agent.enabled = false;

        if (deathAudioSource != null && deathClip != null) // add death sound!
        {
            deathAudioSource.PlayOneShot(deathClip);
        }
        
        transform.DOScale(0f, 0.5f).SetEase(Ease.InBack).OnComplete(() => // use tweenery to add death animations to all enemies for now
        {
            Destroy(gameObject, 1f); // sound has to have time to finishi playing
        });
        
    }

    protected void AttackFeedback()
    {
        transform.DOPunchScale(Vector3.one * 0.2f, 0.2f).SetEase(Ease.InOutQuad);
    }

}
