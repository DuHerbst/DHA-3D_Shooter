using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text; // always use when using the input system

public class PlayerController : MonoBehaviour
{
    private Vector3 _cameraForward;
    private Vector3 _cameraRight;
    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private Vector3 _moveDirection; // better to store all variables together and call them in
    private Quaternion _targetRotation; // quaternions can also be stored as variables

    [SerializeField] private Camera playerCamera;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float gravity = -9.8f; // gravity value, has to be negative to pull the player downwards
    private Vector3 _velocity;
    [SerializeField] public float jumpVelocity = 5f;

    // AIM
    
    [SerializeField] private float moveSpeedAim;
    [SerializeField] private float rotationSpeedAim;
    [SerializeField] private Transform aimTracker;
    [SerializeField] private float maxAimHeight;
    [SerializeField] private float minxAimHeight;
    
    //GROUND CHECK VARIABLES
    [SerializeField] private Vector3 groundCheckOffset;
    [SerializeField] private float groundCheckRadius = 0.5f;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer; // to specify which layer

    public event Action OnJumpEvent; // event to trigger jump animation
    public event Action <PlayerState> OnStateUpdated; // when states change
    
    private bool _isGrounded;
    private Vector3 _defaultAimTrackerPosition;
    private Vector3 _aimTrackerPosition;
    private CharacterController _characterController;

    private PlayerState _currentState; // calls the player state enum that stores teh states

    void Start()
    {
        
       _characterController = GetComponent<CharacterController>();
       
       // set default game state
       _currentState = PlayerState.EXPLORATION;
       OnStateUpdated?.Invoke(_currentState); // on start invoke current exploration state

       _defaultAimTrackerPosition = aimTracker.localPosition; // because this is a child of the player so position is in relation to the player
       

    }
    
    public bool IsGrounded() // gives access to other scripts to view
    {
        return _isGrounded;
    }

    public Vector3 GetPlayerVelocity()
    {
        return _velocity;
    }

    void Update()
    {
        CalculateMovementExplore();
        _characterController.Move(_velocity * Time.deltaTime); // Delta time is used in the update method to convert FPS to Realtime seconds
        
        if (_currentState == PlayerState.EXPLORATION)
        {
            CalculateMovementExplore();
            aimTracker.localPosition = _defaultAimTrackerPosition; // reset the aim tracker position when in exploration mode so when goin into aim mode its not locked to how it was when going out pf it
            
        }

        else if  (_currentState == PlayerState.AIM)
        {
            CalculateMovementAim();
            UpdateAimTrack();
        }

    }

    private void FixedUpdate()
    {
        CheckGrounded();
        
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y -= -0.2f;
        }

    }

    public void OnMove(InputValue value) // when move is triggered
    {
        _moveInput = value.Get<Vector2>(); // get the Vector2 value from the input
        Debug.Log(value);
        
        if (_characterController.enabled == false)
        {
            _characterController.enabled = true;
        }
        
    }

    public void OnLook(InputValue value)
    {
        _lookInput = value.Get<Vector2>();
        Debug.Log("player is looking around");
    }

    public void OnJump()
    {

        if (_isGrounded)
        {
            
            Debug.Log("JUMP");
            _velocity.y = jumpVelocity;
            OnJumpEvent?.Invoke(); // null check
            
            if (_characterController.enabled == false)
            {
                _characterController.enabled = true;
            }
            
        }

    }

    public void OnAim(InputValue value)
    {
        _currentState = value.isPressed ? PlayerState.AIM : PlayerState.EXPLORATION; // if button for aimed is pressed, go to aim state, if not - stay in exploration
        OnStateUpdated?.Invoke(_currentState); // invoke the event

        if (_currentState == PlayerState.AIM)
        {
            _cameraForward = playerCamera.transform.forward; // get the forward direction of the camera
            _cameraForward.y = 0;
            _cameraForward.Normalize();
            transform.rotation = Quaternion.LookRotation(_cameraForward); // rotate the player to face the same direction as the camera
            
        }
        
    }

    private void CalculateMovementExplore()
    {

        _cameraForward = playerCamera.transform.forward; // get the forward direction of the camera
        _cameraRight = playerCamera.transform.right; // get the right direction of the camera
        _cameraForward.y = 0;
        _cameraRight.y = 0;
        _cameraForward.Normalize();
        _cameraRight.Normalize();

        _moveDirection = _cameraRight * _moveInput.x + _cameraForward * _moveInput.y; // calculate the movement direction based on the camera's orientation and input

        if (_moveDirection.sqrMagnitude > 0.01f) // used
        {
            _targetRotation = Quaternion.LookRotation(_moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Compose velocity: horizontal from input, vertical from gravity/jump
        Vector3 horizontalVelocity = _moveDirection * moveSpeed;
        _velocity = new Vector3(horizontalVelocity.x, _velocity.y, horizontalVelocity.z);

        // Apply gravity
        _velocity.y += gravity * Time.deltaTime;
    }

    private void CalculateMovementAim()
    {
        
        transform.Rotate(Vector3.up, rotationSpeedAim * _lookInput.x * Time.deltaTime); 
        
        _moveDirection = _moveInput.x * transform.right + _moveInput.y * transform.forward;
        _velocity = _velocity.y * Vector3.up + moveSpeedAim * _moveDirection; // keep the vertical velocity from gravity and jump, but add the horizontal movement from the input
        _velocity.y += gravity * Time.deltaTime;
        
    }

    private void UpdateAimTrack() // go up and down the aim tracker based on the vertical look input, and clamp it to a certain range so it doesn't go too high or too low
    {
        _aimTrackerPosition = aimTracker.localPosition;
        _aimTrackerPosition.y += _lookInput.y * rotationSpeedAim * Time.deltaTime;
        _aimTrackerPosition.y = Mathf.Clamp(_aimTrackerPosition.y, minxAimHeight, maxAimHeight); // clamp the aim tracker position to prevent it from going too high or too low
        aimTracker.localPosition = _aimTrackerPosition;
    }

    private void CheckGrounded()
    {
        // Use CheckSphere to test if the ground layer is within groundCheckRadius at the offset position.
        Vector3 checkPos = transform.position + groundCheckOffset;
        _isGrounded = Physics.CheckSphere(checkPos, groundCheckRadius, groundLayer, QueryTriggerInteraction.Ignore);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + groundCheckOffset, groundCheckRadius);
    }
}
