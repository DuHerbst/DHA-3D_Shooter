using TMPro;
using UnityEngine;

public class Toast : MonoBehaviour
{
    public static Toast Instance;
    
    [SerializeField] private GameObject toastCanvas;
    [SerializeField] private TMP_Text toastText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        
        Instance = this;
        
    }

    // Update is called once per frame
    void Start()
    {
        toastCanvas.SetActive(false);
    }

    public void ShowToast(string toastValue)
    {
        toastCanvas.SetActive(true);
        toastText.SetText(toastValue);
    }

    public void HideToast()
    {
        toastCanvas.SetActive(false);
    }
}
