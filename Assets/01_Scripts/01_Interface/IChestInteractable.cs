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
      anim?.SetBool(_isOpenHash, false);

      if (Toast.Instance != null)
      {
         Toast.Instance.HideToast();
      }
      
   }

   public void OnInteract()
   {
      
      _collectTween = transform.DOScale(0f, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
      {
         Destroy(gameObject);
      });
      
   }

   void OnDestroy()
   {
      audioSource.PlayOneShot(collectClip);
      DOTween.Kill(this.gameObject);
   }
}
