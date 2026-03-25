using System.Collections;
using UnityEngine;

public class GhostPlatform : MonoBehaviour
{
   [SerializeField] private string playerTag = "Player";
   [SerializeField] private float disappearDuration = 3f;

   private Animator _ghostAnimator;

   [SerializeField] private bool canReset;
   [SerializeField] private float resetTime;
   
   private void Start()
   {
       _ghostAnimator = GetComponent<Animator>();
       _ghostAnimator.SetFloat("DisappearTime", 1/resetTime);
   }

   private void OnTriggerEnter(Collider other)
   {
       if (other.CompareTag(playerTag)) // Check the colliding object's tag
       {
           _ghostAnimator.SetBool("Trigger", true);
       }
   }

   public void TriggerReset()
   {
       if (canReset)
       {
           StartCoroutine(ResetPlatform());
       }
   }

   private IEnumerator ResetPlatform()

   {
       // Wait for the platform to disappear before resetting it
       yield return new WaitForSeconds(disappearDuration);
       _ghostAnimator.SetBool("Trigger", false);
   }
   
}
