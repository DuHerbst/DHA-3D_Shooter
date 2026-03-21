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
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] shootClips; // need multiple clips to avoid repetition and make it more immersive

    private void Awake()
    {
        _currentPlayerController = GetComponent<PlayerController>();
        
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
        
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
         _currentPlayerController.OnStateUpdated -= StateUpdate;
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

         if (audioSource != null && shootClips.Length > 0)
         {
             
             int randomIndex = UnityEngine.Random.Range(0, shootClips.Length); // get a random index for the shoot clips
             audioSource.PlayOneShot(shootClips[randomIndex]); // play the shoot sound at the random index
             
         }

     }
    
     
}
