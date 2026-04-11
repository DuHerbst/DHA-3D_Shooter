using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerHealth playerHealth;
    
    //Spawn settings
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int healthThreshold;
    [SerializeField] private float spawnCooldown;
    [SerializeField] private float spawnRadius;
    
    //Timers
    [SerializeField] private float spawnTimer;
    
        void Update()
        {
            Debug.Log("Current Health: " + playerHealth.currentHealth + " | Threshold: " + healthThreshold);
            
            if (playerHealth.currentHealth <= healthThreshold)
            {
                spawnTimer += Time.deltaTime;
                
                if (spawnTimer >= spawnCooldown)
                {
                    SpawnEnemy();
                    spawnTimer = 0f;
                }
            }
        }
    
    
        private void SpawnEnemy()
        {
           
            Vector3 spawnPosition = player.transform.position + player.transform.forward * 3f;
            spawnPosition.y = player.transform.position.y + 1.5f;

            GameObject spawned = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            Debug.Log("Spawned: " + spawned.name + " at " + spawnPosition);

        }
    
}
