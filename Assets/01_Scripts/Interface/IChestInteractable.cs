using System;
using UnityEngine;
using DG.Tweening;
using Random = System.Random;

public class IChestInteractable : MonoBehaviour, IInteractable
{
   [SerializeField] private Animator anim;
   [SerializeField] private Transform interactPoint;
   
   [SerializeField] private AudioSource audioSource;
   [SerializeField] private AudioClip collectClip;
   
   [SerializeField] private bool isOpen;

   private int _isOpenHash;
   private Tween _bouncyTween;
   private Tween _collectTween;

   private void Start()
   {
      
      if (!anim) return;
      _isOpenHash = Animator.StringToHash("IsOpen"); // use a hash instead of a name
      transform.DOScale(1.25f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
   }

   public void OnHover()
   {
      
      anim?.SetBool(_isOpenHash, true);
      
      if (Toast.Instance != null && interactPoint != null)
      {
         Toast.Instance.ShowToast("Press E", interactPoint);
      }
      
   }

   public void OnHoverOut()
   {
      Debug.Log("Interactor away");
      anim?.SetBool("IsOpen", false);
      
      // to do: Hide UI
      Toast.Instance.HideToast();
      
   }

   public void OnInteract()
   {
      
      _collectTween = transform.DOScale(0f, 0.5f).SetEase(Ease.InBack).OnComplete(() => Destroy(gameObject));
      _collectTween.Play();
      GameManager.Instance.LevelComplete(); // for now, on assignment 3 we will create a win condition on a separate script since we will need 3 chests to open the door
      Destroy(gameObject);
      
   }

   void OnDestroy()
   {
      audioSource.PlayOneShot(collectClip);
      DOTween.Kill(this.gameObject);
   }
}
