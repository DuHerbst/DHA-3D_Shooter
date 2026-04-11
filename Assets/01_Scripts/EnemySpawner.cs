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
                Debug.Log("Low health condition reached");
                spawnTimer += Time.deltaTime;
                
                if (spawnTimer >= spawnCooldown)
                {
                    Debug.Log("Spawning enemy now");
                    SpawnEnemy();
                    spawnTimer = 0f;
                }
            }
        }
    
    
        private void SpawnEnemy()
        {
           
            Vector3 spawnDirection = player.transform.forward + player.transform.right * Random.Range(-1f, 1f);
            spawnDirection.Normalize();
            
            Vector3 spawnPosition = player.transform.position + spawnDirection * spawnRadius; // set the enemy spawn position to the direction and radius from the player
            spawnPosition.y = 0f; // to spawn on the fround i think...
            
            Debug.Log("Spawn position: " + spawnPosition);
            
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        }
    
}
