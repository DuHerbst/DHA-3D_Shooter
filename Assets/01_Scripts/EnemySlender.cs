using System.Collections;
using UnityEngine;

public class EnemySlender : EnemyBase
{
    
    [SerializeField] private float lifeTime = 5f;
    
    //audio
    [SerializeField] private AudioSource spawnSound;
    [SerializeField] private AudioClip spawnClip;

    void Start()
    {
        
        spawnSound.PlayOneShot(spawnClip);
        StartCoroutine(SpawnAndDisappear());

    }
    
    private IEnumerator SpawnAndDisappear()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    
    
}
