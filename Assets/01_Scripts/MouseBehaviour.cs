using UnityEngine;

public class MouseBehaviour : MonoBehaviour
{
    [SerializeField] private Texture2D eyeCursorTexture;
    [SerializeField] private Vector2 eyeCursorHotspot = Vector2.zero;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        ShowCursor(false);
        
    }
    
    public void ShowCursor(bool value)
    {
        
        Cursor.visible = value;
        
        if (value) // if we want to show the cursor, we also want to unlock it and set it to the eye cursor texture
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.SetCursor(eyeCursorTexture, eyeCursorHotspot, CursorMode.Auto);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

    }

    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        
    }
}
