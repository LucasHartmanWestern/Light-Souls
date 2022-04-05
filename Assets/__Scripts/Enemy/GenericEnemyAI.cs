using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GenericEnemyAI : MonoBehaviour
{
    EnemyGeneral enemyGeneral; // Reference to EnemyGeneral class
    Transform playerTargetTransform; // Reference to where the player is
    Animator animator; // Reference to the animator of the enemy
    Vector3 spawnPoint; // Determine where AI should be centered around
    private bool _changingState; // Flags whether or not the state is changing

    [SerializeField]
    AudioSource shootSoundEffect; // Get the audio source attached to the enemy that contains the sound effect for shooting

    [Header("Game Objects/Transforms")]
    public GameObject projectilePrefab; // Get reference to the projectile prefab
    public Transform muzzleFlashPS; // Get reference to the muzzle flash particle system
    public Transform spawnBulletPosition; // Point where bullet spawns when fired
    public Transform rangedWeapon; // Reference to player's ranged weapon
    public Transform handTransform; // Reference to player's hand transform

    [Header("AI Parameters")]
    public bool bossAI; // Check if this is for boss or not
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
    public string enemyState = "Wander"; // Holds state the enemy is in
    public float sightRange, attackRange; // How far away player needs to be to be seen or attacked
    public bool playerInSightRange, playerInAttackRange; // Check if player is in the range to be seen or attacked

    public GameObject GruntDialogue;
    public GameObject BossDialogue;

    // Called after Awake()
    private void Start()
    {
        enemyGeneral = GetComponent<EnemyGeneral>(); // Get EnemyGeneral script attached to the same object as this
        animator = GetComponent<Animator>(); // Get reference of animator attached to enemy
        playerTargetTransform = GameObject.Find("PlayerTarget").transform; // Get reference to player instance transform
        agent = GetComponent<NavMeshAgent>(); // Get reference to the NavMeshAgent attached to this enemy instance
        rangedWeapon.SetParent(handTransform); // Track ranged weapon to hands
        spawnPoint = transform.position;
    }

    // Called once a frame
    private void Update()
    {
        // Hostile behaviour
        if (enemyGeneral.isHostile)
        {
            #region Handle State System of Enemy
            // Check if player is in the sight or attack range using a physics sphere
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (enemyGeneral.isAlive && enemyState == "Attacking")
                animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 1f, Time.deltaTime * 10f)); // Make enemy not aim
            else
                animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0, Time.deltaTime * 10f)); // Make enemy not aim

            // Start coroutines based on changing the states
            if (GetComponent<NavMeshAgent>().enabled == true) // Only perform if the NavMeshAgent is on
            {
                if (!playerInSightRange && !playerInAttackRange) StartCoroutine(ChangeState("Wannder"));
                else if (playerInSightRange && !playerInAttackRange) StartCoroutine(ChangeState("Chasing"));
                else if (playerInSightRange && playerInAttackRange) StartCoroutine(ChangeState("Attacking"));

                // Call the method relating to the state the enemy is in
                switch (enemyState)
                {
                    case "Attacking":
                        Attacking();
                        break;
                    case "Chasing":
                        Chasing();
                        break;
                    default:
                        Patrolling();
                        break;
                }
            }
            #endregion
        }
        else
        {
            // TODO -- Handle non-hostile behavior
            if(playerInAttackRange && Input.GetKeyDown(KeyCode.T) && !bossAI)
            {
                GruntDialogue.SetActive(true);
                if(Input.GetKeyDown(KeyCode.B))
                {
                    GruntDialogue.SetActive(false);
                }
                
            }
            else if(playerInAttackRange && Input.GetKeyDown(KeyCode.T) && bossAI)
            {
                BossDialogue.SetActive(true);
                if(Input.GetKeyDown(KeyCode.B))
                {
                    BossDialogue.SetActive(false);
                }
            }
            else
            {
                GruntDialogue.SetActive(false);
                BossDialogue.SetActive(false);
            }
        }
    }

    // Find a new walk point to go to
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange); // Calculate random point in range
        float randomX = Random.Range(-walkPointRange, walkPointRange); // Calculate random point in range

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ); // Get new position to go to

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround) && Vector3.Distance(spawnPoint, walkPoint) <= 3) walkPointSet = true; // Make sure new walk point is in range
    }

    // Make enemy wander around the arena
    private void Patrolling()
    {
        if (bossAI) transform.Find("EnemyModel").transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0)); // Rotate boss model slightly

        animator.SetFloat("MovingAmount", 0.5f); // Set the isMoving float in the animator
        agent.speed = enemyGeneral.walkSpeed; // Decrease enemy speed

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
        if (bossAI) transform.Find("EnemyModel").transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0)); // Rotate boss model slightly

        animator.SetFloat("MovingAmount", 1f); // Set the isMoving float in the animator
        agent.speed = enemyGeneral.chaseSpeed; // Increase enemy speed

        agent.SetDestination(playerTargetTransform.position); // Move agent towards player
    }

    // Make enemy attack the player
    private void Attacking()
    {
        if (bossAI) transform.Find("EnemyModel").transform.localRotation = Quaternion.Euler(new Vector3(0, 45, 0)); // Rotate boss model slightly

        agent.SetDestination(transform.position); // Don't move enemy when they are attacking
        animator.SetFloat("MovingAmount", 0); // Set the isMoving float in the animator

        Vector3 targetDirection = Vector3.zero; // Start out at (0, 0, 0)

        Vector3 lookAimTarget = (playerTargetTransform.position - transform.position).normalized; // Determine where the enemy should aim
        targetDirection = lookAimTarget; // Have enemy look towards where they're aiming

        targetDirection.Normalize(); // Set magnitude to 1
        targetDirection.y = 0; // Set value on y axis to 0

        if (targetDirection == Vector3.zero) targetDirection = transform.forward; // Keep rotation at previous position

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection); // Look towards the target direction defined above
        Quaternion enemyRotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.deltaTime); // Get quaternion of the enemy's roation

        transform.rotation = enemyRotation; // Rotate the transform of the enemy

        // Check if enemy already attacked
        if (!alreadyAttacked)
        {
            Vector3 aimDirection = (playerTargetTransform.position - spawnBulletPosition.position).normalized; // Determine where the user is aiming relative to the player

            if (animator.GetLayerWeight(2) >= 0.8f)
            {
                Instantiate(muzzleFlashPS, spawnBulletPosition.position, Quaternion.LookRotation(aimDirection, Vector3.up)); // Create a yellow muzzle flash
                shootSoundEffect.Play(); // Play the shooting sound effect
                Instantiate(projectilePrefab, spawnBulletPosition.position, Quaternion.LookRotation(aimDirection, Vector3.up)); // Spawn in a bullet
            }   

            alreadyAttacked = true; // Set that they attacked
            Invoke(nameof(ResetAttack), timeBetweenAttacks); // Reset attack after specified cooldown
        }
    }

    // Reset the attack
    private void ResetAttack()
    {
        alreadyAttacked = false; // Reset the alreadyAttacked bool
    }

    // Changes state of the enemy (used to delay state changes)
    IEnumerator ChangeState(string newState)
    {
        // Only run if not already changing states
        if (!_changingState)
        {
            _changingState = true; // Set the states are changing
            yield return new WaitForSeconds(1); // Wait for a second
            enemyState = newState; // Set the state
            _changingState = false; // Set that states are no longer changing
        }
        yield return null;
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
