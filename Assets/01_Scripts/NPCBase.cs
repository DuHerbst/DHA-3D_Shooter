using UnityEngine;

public abstract class NPCBase : MonoBehaviour
{
    protected virtual void Movement()
    {
        Debug.Log("Im moving awaaaay");
    }

    void Interact()
    {
        Debug.Log("Im interacting");
    }

    void DamageNPC()
    {
        Debug.Log("Im getting damaged");
    }

    protected virtual void Damage(float damageValue, GameObject dropObject)
    {
        
    }
    
    
}
