using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float currentTime = 0f;
    [SerializeField] private float direction = 1f; // every frame we will increase time by this amount
    [SerializeField] private float cycleTime;
    [SerializeField] private float delayTime;
    private float _waitTime;
    private bool _isWaiting;
    
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    
    
    // Update is called once per frame
    void Update()
    {
        if (_isWaiting)
        {
            _waitTime -= Time.deltaTime;
            
            if (_waitTime <= 0f)
            {
                
                _isWaiting = false;
                
            }
            
            return;
        }
        
        currentTime += direction * Time.deltaTime;
        
        if (currentTime >= cycleTime)
        {
            currentTime = cycleTime;
            direction = -1f;
            _isWaiting = true;
            _waitTime = delayTime;
        }
        
        else if (currentTime <= 0f)
        {
            currentTime = 0;
            direction = 1f;
            _isWaiting = true;
            _waitTime = delayTime;
        }
        
        currentTime = Mathf.Clamp(currentTime, 0, cycleTime);
       
        float t = currentTime / cycleTime;
        transform.position = Vector3.Lerp(pointA.position, pointB.position, t);
       
    }
    
}