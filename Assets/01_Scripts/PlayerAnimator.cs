using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{ 
   [SerializeField] private PlayerController playerController;
   [SerializeField] private Animator anim;
   
   Vector3 _playerVelocity;
   
   void Update()
   {
       anim.SetBool("isGrounded", playerController.IsGrounded());
       
       _playerVelocity = playerController.GetPlayerVelocity();
       _playerVelocity.y = 0;
       anim.SetFloat("Velocity", _playerVelocity.sqrMagnitude); // Sqr magnitude is used to get the magnitude of the velocity without the square root - helps with performance
       
   }

   void OnEnable()
   {
       playerController.OnJumpEvent += OnJump;
   }

   void OnDisable()
   {
       playerController.OnJumpEvent -= OnJump; 
   }
   
   private void OnJump()
   {
       anim.SetTrigger("Jump"); // Set trigger is used to trigger
   }
   
}
