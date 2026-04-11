using UnityEngine;

public interface IDamageable
{ 
    void TakeDamage(int damageAmount); // called when the player takes damage. Allows for different types of damage
    
}
