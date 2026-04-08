using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator enemyAnimator;
    
    private EnemyState _currentState; // call the state enum
    
    [SerializeField] private Transform[] patrolPoints; // array of points to patrol between
    [SerializeField] private NavMeshAgent agent; // reference to nav mesh created
    [SerializeField] private Transform playerTransform; // reference to the player transform
    [SerializeField] private float chaseDistance;
    [SerializeField] private float giveUpDistance;
    [SerializeField] private float checkDistanceAngle;
    
    private Transform _currentTarget; // current target to move towards
    private bool _isWaiting = false;
    private Vector3 _directionToPlayer; // variable to store the direction from the enemy to the player
    
    // do damage
    [SerializeField] private int damageAmount;
    
    void Start()
    {
        _currentState = EnemyState.Idle; // start in idle state
    }

    private void FixedUpdate()
    {
        
        if (_currentState == EnemyState.Idle)
        {
            enemyAnimator.SetBool("Idle", true);
            
            if(!_isWaiting)
                StartCoroutine(WaitAndGo(5)); // start the coroutine to wait for 2 seconds before going to the next patrol point
            
            if (PlayerInRange() && IsInFOV())
            {
                Debug.Log("Player in range and in FOV"); 
                _currentState = EnemyState.Chasing; // change state to chasing if the player is in the field of view and within chase distance
                enemyAnimator.SetBool("Walk", false);
            }
        }
        
        else if (_currentState == EnemyState.Patrolling)
        {
            Debug.Log("Patrolling");
            enemyAnimator.SetBool("Walk", true);
            if (agent.remainingDistance <= 0.2f) // check if the agent has reached the current target (remaining distance is less than or equal to 0.2 units)
            {
                _currentState = EnemyState.Idle; // change state to idle when reaching the target
                enemyAnimator.SetBool("Walk", false);
            }
            
        }
        
        else if (_currentState == EnemyState.Chasing)
        {
            Debug.Log("Chasing");
            enemyAnimator.SetBool("Chase", true);
            agent.SetDestination(playerTransform.position); // set the destination to the player's position
            
            //give up
            if (PlayerAway())
            {
                _currentState = EnemyState.Idle; // change state to idle if the player has gone away
                enemyAnimator.SetBool("Chase", false);
            }
        }
        
    }
    
    private IEnumerator WaitAndGo (float waitTime) //create coroutine for waiting at points
    {
        _isWaiting = true;
        Debug.Log(_isWaiting);
        yield return new WaitForSeconds(waitTime); // wait for 2 seconds before choosing a new patrol point
        enemyAnimator.SetBool("Idle", false);
        _currentState = EnemyState.Patrolling; // change state to patrol after waiting
        ChoosePatrolPoint();
        _isWaiting = false;
        
    }
    
    private void ChoosePatrolPoint()
    {
        if (patrolPoints.Length <= 0) return;
        _currentTarget = patrolPoints[Random.Range(0, patrolPoints.Length)];
        
        agent.SetDestination(_currentTarget.position); // generate a path and start moving towards the target
       
    }

    private bool PlayerInRange()
    {
        return Vector3.Distance(transform.position, playerTransform.position) <= chaseDistance; // check if the distance between the enemy and the player is less than or equal to the chase distance
        
    }

    private bool PlayerAway()
    {
        return Vector3.Distance(transform.position, playerTransform.position) >= giveUpDistance;
    }

    private bool IsInFOV()
    {
        _directionToPlayer = (playerTransform.position - transform.position).normalized; // normalize the values so we just get the direction of the angle
        return Vector3.Angle(transform.forward, _directionToPlayer) <= checkDistanceAngle; // calculate angle
        
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>(); // check for Idamageable interface
        
        if (damageable != null)
        {
            damageable.TakeDamage(damageAmount);
        }
    }
    
}
