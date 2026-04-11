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
    
    // fire rate
    [SerializeField] private float shootCooldown;
    private float _fireTimer; 

    private void Awake()
    {
        _currentPlayerController = GetComponent<PlayerController>();
        
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
        
    }

    private void Update()
    {
        _fireTimer -= Time.deltaTime;
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
         if (!context.performed) // is the action is not performed
         {
             return;
         }
         
         if (arrowObject == null)
         {
             return;
         }
         
         if (_fireTimer > 0)
         {
             return;
         }
         
         if (_currentPlayerState != PlayerState.AIM) // is the player is not in aim mode, return
         {
             return;
         }
         
         _shootDirection = aimPoint.position - shootPoint.position; // direction is the difference between the aim point and the shoot point
         _shootDirection.Normalize(); // normalize the direction to get a unit vector
         
         GameObject arrow = Instantiate(arrowObject, shootPoint.position, Quaternion.LookRotation(_shootDirection));
         arrow.GetComponent<Rigidbody>().AddForce(shootForce * _shootDirection, ForceMode.Impulse);
         
         _fireTimer = shootCooldown; // reset the fire timer to the cooldown time after instantiating and shooting 

         if (audioSource != null && shootClips.Length > 0)
         {
             int randomIndex = Random.Range(0, shootClips.Length); // get a random index for the shoot clips
             audioSource.PlayOneShot(shootClips[randomIndex]); // play the shoot sound at the random index
         }

     }
    
     
}
