using UnityEngine;

public class LavaZone : MonoBehaviour
{
    [SerializeField] private Transform lavaRespawnPoint;
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log( "Touched lava " + other.gameObject.name); // check what touched the lava
        
        IDamageable damageable = other.GetComponent<IDamageable>(); // checks for interface

      if (damageable != null)
      {
          damageable.TakeDamage(1);
      }
      
      PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();
      
      if (playerRespawn != null)
      {
          playerRespawn.RespawnAt(lavaRespawnPoint.position);
      }
      
    }
    
}
