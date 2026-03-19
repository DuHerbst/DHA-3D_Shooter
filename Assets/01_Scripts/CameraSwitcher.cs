using Unity.Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    // this script will control switching the cameras when aiming with right click
    
    [SerializeField] private CinemachineCamera exploreCamera;
    [SerializeField] private CinemachineCamera aimCamera;
    [SerializeField] private PlayerController playerController;

    void OnEnable()
    {
        playerController.OnStateUpdated += SwitchCamera;
    }

    void OnDisable()
    {
        playerController.OnStateUpdated -= SwitchCamera;
    }

    private void SwitchCamera(PlayerState state)
    {
        
        switch (state)
        {
            case PlayerState.EXPLORATION:
                // what happens when player goes to exploration
                exploreCamera.Prioritize();
                break;
            case PlayerState.AIM:
                // what happens when player is in aiming mode
                aimCamera.Prioritize();
                break;
            default:
                break;
        }
        
    }
    
    
}
