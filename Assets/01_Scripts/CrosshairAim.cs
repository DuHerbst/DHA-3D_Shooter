using UnityEngine;

public class CrosshairAim : MonoBehaviour

{

    [SerializeField] private PlayerController player;
    [SerializeField] private Canvas crossHairCanvas;
    
     void OnEnable()
    {
        player.OnStateUpdated += PlayerState;
    }

    private void PlayerState(PlayerState obj)
    {
        throw new System.NotImplementedException();
    }

    void OnDisable()
    {
        player.OnStateUpdated -= PlayerState;
    }

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        crossHairCanvas.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
