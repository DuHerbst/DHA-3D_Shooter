using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float currentTime = 0f;
    [SerializeField] private float direction = 1f; // every frame we will increase time by this amount
    [SerializeField] private float cycleTime;
    
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    
    
    // Update is called once per frame
    void Update()
    {
        currentTime += direction * Time.deltaTime;
        
        if (currentTime >= cycleTime)
        {
            currentTime = cycleTime;
            direction = -1f;
        }
        
        else if (currentTime <= 0f)
        {
            currentTime = 0;
            direction = 1f;
        }

        Vector3 currentPosition = transform.position; // the current position of this game object
       
        currentTime = Mathf.Clamp(currentTime, 0, cycleTime);
       
        float t = currentTime / cycleTime;
        transform.position = Vector3.Lerp(pointA.position, pointB.position, t);
       
    }
    
}