using TMPro;
using UnityEngine;

public class Toast : MonoBehaviour
{
    [SerializeField] private Camera worldCamera;
    [SerializeField] private RectTransform toastTransform;
    [SerializeField] private Vector3 toastOffset;
    [SerializeField] private GameObject toastCanvas;
    [SerializeField] private TMP_Text toastText;
    
    private Transform _currentTarget;
    
    public static Toast Instance;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        
    }
    
    void Start()
    {
        toastCanvas.SetActive(false);
    }

    void LateUpdate() // used to create updates after everything is set on hte world
    {
        if (_currentTarget == null || worldCamera == null || toastTransform == null)
        {
            toastCanvas.SetActive(false);
            return;
        }

        Vector3 screenPos = worldCamera.WorldToScreenPoint(_currentTarget.position + toastOffset);

        if (screenPos.z < 0)
        {
            toastCanvas.SetActive(false);
            return;
        }

        toastCanvas.SetActive(true);
        toastTransform.position = screenPos;
    }
    

    public void ShowToast(string toastValue, Transform target)
    {
        
        toastCanvas.SetActive(true);
        Vector3 screenPos = worldCamera.WorldToScreenPoint(target.position + toastOffset);
        
        toastTransform.position = screenPos;
        _currentTarget = target;
        toastText.SetText(toastValue);
        
    }

    public void HideToast()
    {
        toastCanvas.SetActive(false);
        _currentTarget = null;
        
    }
}
