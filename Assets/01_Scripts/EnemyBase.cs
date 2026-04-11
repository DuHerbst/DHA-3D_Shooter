using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected NavMeshAgent agent; // reference to nav mesh created
    
    //enemy health
    [SerializeField] private int enemyMaxHealth;
    private int _enemyCurrentHealth;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _enemyCurrentHealth = enemyMaxHealth; //set health on start
    }
    
    public void TakeDamage(int damageAmount)
    { 
        _enemyCurrentHealth -= damageAmount; 
        
        if (_enemyCurrentHealth <= 0)
            
        {
          Die();
        }
      
    }
    
    private void Die()
    {
        agent.enabled = false;
        Destroy(gameObject, 2f);
    }
    
}
