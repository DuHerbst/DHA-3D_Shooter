using UnityEngine;

public class LavaZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>(); // checks for interface

      if (damageable != null)
      {
          damageable.TakeDamage(1);
      }
    }
    
}
