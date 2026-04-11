using UnityEngine;

public class Arrow : MonoBehaviour

{
    [SerializeField] private int arrowDamage = 1;
    private Rigidbody _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
    }

    void FixedUpdate()
    {
        _rb.rotation = Quaternion.LookRotation(_rb.linearVelocity);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // if other is not player and is not enemy, destroy game object after 2 seconds

        if (other.CompareTag("Player"))
        {
            return;
        }

        if (other.CompareTag("Enemy"))
            
        {
            IDamageable target = other.gameObject.GetComponent<IDamageable>();
            target?.TakeDamage(arrowDamage);
            
        }
        
        Destroy(gameObject, 2f);
        
    }
    
}