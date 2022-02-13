using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    EnemyGeneral enemyGeneral; // Reference to EnemyGeneral class
    Transform playerTransform; // Reference to where the player is
    Animator animator; // Reference to the animator of the enemy
    int isMoving; // Reference to the isMoving parameter in the animator

    [Header("Game Objects/Transforms")]
    public GameObject projectilePrefab; // Get reference to the projectile prefab
    public Transform spawnBulletPosition; // Point where bullet spawns when fired
    public Transform rangedWeapon; // Reference to player's ranged weapon
    public Transform handTransform; // Reference to player's hand transform

    [Header("AI Parameters")]
    NavMeshAgent agent; // Reference to the NavMeshAgent script
    public LayerMask whatIsGround, whatIsPlayer; // Reference to the ground and player layers

    [Header("Wander variables")]
    public Vector3 walkPoint; // Where enemy walks to
    bool walkPointSet; // Check if a walk point is already set
    public float walkPointRange; // Check how far to make a new walk point

    [Header("Attacking variables")]
    public float timeBetweenAttacks; // How long to wait between attacks
    bool alreadyAttacked; // Check if enemy already attacked

    [Header("State variables")]
    public float sightRange, attackRange; // How far away player needs to be to be seen or attacked
    public bool playerInSightRange, playerInAttackRange; // Check if player is in the range to be seen or attacked

    // Called before Start()
    private void Awake()
    {
        enemyGeneral = GetComponent<EnemyGeneral>(); // Get EnemyGeneral script attached to the same object as this
        animator = GetComponent<Animator>(); // Get reference of animator attached to enemy
        playerTransform = FindObjectOfType<PlayerGeneral>().transform; // Get reference to player instance transform
        agent = GetComponent<NavMeshAgent>(); // Get reference to the NavMeshAgent attached to this enemy instance

        isMoving = Animator.StringToHash("isMoving"); // Set int to be the value of horizontal in the animator
    }

    // Called after Awake()
    private void Start()
    {
        rangedWeapon.SetParent(handTransform); // Track ranged weapon to hands
    }

    private void Update()
    {
        // Check if player is in the sight or attack range using a physics sphere
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (GetComponent<NavMeshAgent>().enabled == true) // Only perform if the NavMeshAgent is on
        {
            // Call the method relating to the state the enemy is in
            if (!playerInSightRange && !playerInAttackRange) Patrolling();
            else if (playerInSightRange && !playerInAttackRange) Chasing();
            else if (playerInSightRange && playerInAttackRange) Attacking();
        }
    }

    // Find a new walk point to go to
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange); // Calculate random point in range
        float randomX = Random.Range(-walkPointRange, walkPointRange); // Calculate random point in range

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ); // Get new position to go to

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) walkPointSet = true; // Make sure new walk point is in range
    }

    // Make enemy wander around the arena
    private void Patrolling()
    {
        animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0, Time.deltaTime * 10f)); // Make enemy not aim
        animator.SetFloat(isMoving, 0.5f, 0.1f, Time.deltaTime); // Set the isMoving float in the animator

        if (!walkPointSet) SearchWalkPoint(); // Search for a walkPoint until one is found
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint); // Move the agent
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint; // Get the distance from the enemy to the walkPoint
        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false; // Enemy reached walk point and now needs a new one
    }

    // Make enemy chase the player
    private void Chasing()
    {
        animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0, Time.deltaTime * 10f)); // Make enemy not aim
        animator.SetFloat(isMoving, 1, 0.1f, Time.deltaTime); // Set the isMoving float in the animator

        agent.SetDestination(playerTransform.position); // Move agent towards player
    }

    // Make enemy attack the player
    private void Attacking()
    {
        agent.SetDestination(transform.position); // Don't move enemy when they are attacking
        animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 1f, Time.deltaTime * 10f)); // Smoothly transition animation to aiming or not aiming


        transform.LookAt(playerTransform); // Look at the player while attacking

        // Check if enemy already attacked
        if (!alreadyAttacked)
        {
            Instantiate(projectilePrefab, spawnBulletPosition.position, Quaternion.LookRotation(playerTransform.position, Vector3.up), transform); // Spawn in a bullet

            alreadyAttacked = true; // Set that they attacked
            Invoke(nameof(ResetAttack), timeBetweenAttacks); // Reset attack after specified cooldown
        }
    }

    // Reset the attack
    private void ResetAttack()
    {
        alreadyAttacked = false; // Reset the alreadyAttacked bool
    }

    // Draw the sight and attack ranges of the enemy
    private void OnDrawGizmosSelected()
    {
        // Draw red sphere for attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        // Draw yellow sphere for attack range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
