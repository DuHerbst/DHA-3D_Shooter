using System;
using UnityEngine;
using UnityEngine.InputSystem; // always use when using the input system

public class PlayerController : MonoBehaviour
{
    private Vector3 _cameraForward;
    private Vector3 _cameraRight;
    private Vector2 _moveInput;
    private Vector3 _moveDirection; // better to store all variables together and call them in
    private Quaternion _targetRotation; // quaternions can also be stored as variables

    [SerializeField] private Camera playerCamera;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float gravity = -9.8f; // gravity value, has to be negative to pull the player downwards
    private Vector3 _velocity;
    [SerializeField] public float jumpVelocity = 5f;

    //GROUND CHECK VARIABLES
    [SerializeField] private Vector3 groundCheckOffset;
    [SerializeField] private float groundCheckRadius = 0.5f;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer; // to specify which layer

    public event Action OnJumpEvent; // event to trigger jump animation
    
    private bool _isGrounded;
    private CharacterController _characterController;

    public bool IsGrounded() // gives access to other scripts to view
    {
        return _isGrounded;
    }

    public Vector3 GetPlayerVelocity()
    {
        return _velocity;
    }

    void Start()
    {

       _characterController = GetComponent<CharacterController>(); // start with getting the character controller component

    }

    void Update()
    {
        CalculateMovement();
        _characterController.Move(_velocity * Time.deltaTime); // Delta time is used in the update method to convert FPS to Realtime seconds

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
    }

    public void OnJump()
    {

        if (_isGrounded)
        {
            
            Debug.Log("JUMP");
            _velocity.y = jumpVelocity;
            OnJumpEvent?.Invoke(); // null check
            
        }

    }

    private void CalculateMovement()
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
