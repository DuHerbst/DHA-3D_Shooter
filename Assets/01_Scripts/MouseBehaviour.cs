using UnityEngine;

public class MouseBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        ShowCursor(false);
        
    }
    
    public void ShowCursor(bool value)
    {
        
        Cursor.visible = value;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked; // a single line if statement. ? = is true, : = else
        
    }
}
