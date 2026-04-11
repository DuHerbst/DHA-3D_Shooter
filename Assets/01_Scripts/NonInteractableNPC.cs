using UnityEngine;

public class NonInteractableNPC : NPCBase
{

    protected override void Movement()
    {
        base.Movement();
        Debug.Log("NonInteractableNPC Move");
        Debug.DrawRay(transform.position, Vector3.down, Color.red);
    }
    
}
