using UnityEngine;

public class CrosshairAim : MonoBehaviour

{

    [SerializeField] private PlayerController player;
    [SerializeField] private Canvas crossHairCanvas;
    
     void OnEnable()
     {
         player.OnStateUpdated += HandlePlayerState;
    }

     void OnDisable()
    {
        player.OnStateUpdated -= HandlePlayerState;
    }
     
    private void HandlePlayerState(PlayerState state)
    {
        if (state == PlayerState.AIM)
        {
            crossHairCanvas.enabled = true;
        }
        else
        {
            crossHairCanvas.enabled = false;
        }
    }
    
    void Start()
    {
        crossHairCanvas.enabled = false;
    }
    
}
