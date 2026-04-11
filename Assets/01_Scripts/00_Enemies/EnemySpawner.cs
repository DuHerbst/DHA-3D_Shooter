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
    [SerializeField] private int maxEnemies;
    private int _currentEnemies;
    
    //Timers
    [SerializeField] private float spawnTimer;
    
        void Update()
        {
            
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
            if (_currentEnemies >= maxEnemies)
            {
                return; // don't spawn if we have reached the maximum number of enemies
            }
           
            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius; // make sure spawns inside a circular radius

            Vector3 spawnPosition = player.transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);
            spawnPosition.y = player.transform.position.y + 1.5f;

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        }
    
}
