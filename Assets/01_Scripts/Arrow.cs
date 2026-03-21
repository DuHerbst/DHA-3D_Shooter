using UnityEngine;

public class Arrow : MonoBehaviour
{
    
    private Rigidbody _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Invoke(nameof(DestroyAfter), 5f);
    }

    void FixedUpdate()
    {
        _rb.rotation = Quaternion.LookRotation(_rb.linearVelocity);
        //lerp here
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") || other.CompareTag("Interactor"))
        {
            return;
        }
        
        ITrigger trigger = other.GetComponent<ITrigger>();
        
        if (trigger != null)
        {
            return;
        }

        else
        {
            Destroy(gameObject);
        }
        
    }

    void DestroyAfter()
    {
        Destroy(gameObject);
    }
}
