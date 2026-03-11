using System.Collections;
using UnityEngine;

public class GhostPlatform : MonoBehaviour
{
   [SerializeField] string playerTag = "Player";
   [SerializeField] float disappearDuration = 3f;
   
   Animator ghostAnimator;

   [SerializeField] private bool canReset;
   [SerializeField] private float resetTime;
   
   private void Start()
   {
       ghostAnimator = GetComponent<Animator>();
       ghostAnimator.SetFloat("ResetTime", 1/resetTime); // divide the speed to the amount of time we want the animation to last
   }

   private void OnTriggerEnter(Collider other)
   {
       if (other.gameObject.CompareTag(playerTag))
       {
           ghostAnimator.SetBool("Trigger", true);
           
           if (canReset)
           {
               StartCoroutine(ResetPlatform());
           }
       }
   }

   private IEnumerator ResetPlatform()

   {
       // Wait for the platform to disappear before resetting it
       yield return new WaitForSeconds(disappearDuration);
       ghostAnimator.SetBool("Trigger", false);
   }
   
}
