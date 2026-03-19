using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour

{
    [SerializeField] private InputAction shootInput;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform aimPoint;
    [SerializeField] private GameObject arrowObject;
    [SerializeField] private float shootForce;
    
    private GameObject _arrow;
    private Vector3 _shootDirection;
    private PlayerState _currentPlayerState;
    private PlayerController _currentPlayerController;

    private void Awake()
    {
        _currentPlayerController = GetComponent<PlayerController>();
        
    }

    void OnEnable()
    {
        shootInput.Enable();
        shootInput.performed += Shoot;

        _currentPlayerController.OnStateUpdated += StateUpdate;

    }

    void StateUpdate(PlayerState state)
    {
        _currentPlayerState = state;
    }
     
     void OnDisable()
     {
         shootInput.performed -= Shoot;
     }
     
     private void Shoot(InputAction.CallbackContext context)
     {
         if (_currentPlayerState != PlayerState.AIM) // is the player is not in aim mode, return
         {
             return;
         }
         
         //calculate arrow direction
         _shootDirection = aimPoint.position - shootPoint.position; // direction is the difference between the aim point and the shoot point
         _shootDirection.Normalize(); // normalize the direction to get a unit vector
         
         GameObject arrow = Instantiate(arrowObject, shootPoint.position, Quaternion.LookRotation(_shootDirection)); // instantiate the arrow at the shoot point position, with the rotation of the shoot direction
         
         //add force to the arrow being shot
         arrow.GetComponent<Rigidbody>().AddForce(shootForce * _shootDirection, ForceMode.Impulse);

     }
    
     // shoot at targets to open door
     // shoot at skeletons
     // shoot at stuff around - destroy lamps?
     
}
