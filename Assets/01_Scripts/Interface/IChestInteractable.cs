using System;
using UnityEngine;
using DG.Tweening;
using Random = System.Random;

public class IChestInteractable : MonoBehaviour, IInteractable
{
   [SerializeField] private Animator anim;

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
      Debug.Log("Interactor close by");
      anim?.SetBool(_isOpenHash, true);
      
      
      Toast.Instance.ShowToast("Press E to open the chest");
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
      Debug.Log ($"Interacted with {gameObject.name}");

      _collectTween = transform.DOScale(0f, 0.5f).SetEase(Ease.InBack).OnComplete(() =>

      {
         Destroy(gameObject);
      });

   }

   void OnDestroy()
   {
      DOTween.Kill(this.gameObject);
   }
}
