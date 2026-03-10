using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public void RespawnAt(Vector3 respawnPosition)
    {
        Debug.Log("RespawnAt called. Target position: " + respawnPosition);
        
        CharacterController characterController = GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterController.enabled =
                false; // disable the character controller to prevent physics issues during teleportation

        }

        transform.position = respawnPosition; // move the player to the respawn position
        
        Debug.Log("Player new position " + respawnPosition);

        if (characterController != null)
        {
            characterController.enabled = true; // re-enable the character controller after teleportation
        }
        
    }
}