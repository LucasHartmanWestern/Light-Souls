using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGeneral : MonoBehaviour
{
    Animator animator; // Reference to enemy animator
    PlayerGeneral playerGeneral; // Reference to PlayerGeneral script
    [SerializeField] AudioSource deathSound; // Reference to the audio that plays when the enemy dies
    CameraManager cameraManager; // Reference to the CameraManager
    InputManager inputManager; // Reference to the InputManager
    NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent class

    [Header("Required objects")]
    public Transform deathPS; // Death particle system reference
    public GameObject firePS; // Particle system of fire effect
    public Transform lockOnTransform; // Transform that the player can lock on to

    [Header("Enemy Stats")]
    public bool isAlive; // Track if this enemy instance is alive
    public float enemyHealth; // Track health of enemy
    public float expOnDeath; // How much exp enemy gives player when they die
    public float enemyDamage; // Track damage enemy does to player
    public float walkSpeed = 1f; // How fast enemy patrols
    public float chaseSpeed = 8f; // How fast enemy chases
    public bool isOnFire; // Track if enemy is on fire

    public bool isInAir; // Checks if enemy is in the air or not

    // Called before Start()
    private void Awake()
    {
        animator = GetComponent<Animator>(); // Get animator attached to this enemy
        playerGeneral = FindObjectOfType<PlayerGeneral>(); // Get reference to Player General instance
        cameraManager = FindObjectOfType<CameraManager>(); // Get reference to Camera Manager instance
        inputManager = FindObjectOfType<InputManager>(); // Get reference to Input Manager instance
        navMeshAgent = GetComponent<NavMeshAgent>(); // Get reference to the NavMeshAgent instance attached to this enemy
    }

    // Called once a frame
    private void Update()
    {
        if (isAlive) HandleFireDamage(); // Handles the fire damage effect
        if (enemyHealth <= 0) isAlive = false; // Set that the enemy is dead
    }

    // Damage the enemy
    public void TakeDamage(float damageAmount)
    {
        enemyHealth -= damageAmount; // Decrease health

        if (enemyHealth <= 0)
        {
            animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f)); // Smoothly transition animation to aiming or not aiming
            animator.CrossFade("Death", 0.2f); // Play death animation

            // Change what the player is locked on to
            inputManager.lockOnFlag = false;
            cameraManager.ClearLockOnTargets();

            StartCoroutine(DestroyEnemyObject()); // Destroy enemy game obejct
        }
    }

    // Handle the destroying of the enemy GameObject
    IEnumerator DestroyEnemyObject()
    {
        GetComponent<NavMeshAgent>().enabled = false; // Disable the NavMeshAgent
        deathSound.Play(); // Play the death sound

        yield return new WaitForSeconds(5f); // Wait 5 seconds before destroying the object
        Instantiate(deathPS, transform.position, Quaternion.identity); // Create a death particle 

        playerGeneral.playerExperience += expOnDeath; // Increase player exp
        Destroy(gameObject); // Destroy the game object
    }

    // Fling enemy towards target
    public void FlingForwards(Transform target)
    {
        GetComponent<EnemyAI>().enabled = false;
        navMeshAgent.enabled = false;

        Vector3 lookDirection = new Vector3(target.position.x, transform.position.y, target.position.z); // Get directino of player
        transform.LookAt(lookDirection); // Make enemy look at the player

        animator.SetLayerWeight(2, 0); // Make it so aiming layer weight is 0
        animator.CrossFade("FlingForwards", 0.2f); // Play fling animation
    }

    // Fling the enemy backwards from the target
    public void FlingBackwards(Transform target)
    {
        GetComponent<EnemyAI>().enabled = false;
        navMeshAgent.enabled = false;

        Vector3 lookDirection = new Vector3(target.position.x, 0, target.position.z); // Get directino of player
        transform.LookAt(lookDirection); // Make enemy look at the player

        animator.SetLayerWeight(2, 0); // Make it so aiming layer weight is 0
        animator.CrossFade("FlingBackwards", 0.2f); // Play death animation
    }

    // Takes fire damage for a certain amount of time
    public void HandleFireDamage()
    {
        if (isOnFire && !firePS.activeSelf) // Check if player just got set on fire
        {
            firePS.SetActive(true); // Enable the fire particle systen
            StartCoroutine("EndFireDamage"); // Start the coroutine to end the fire damage
        }

        if (isOnFire) // Check if player is on fire 
            TakeDamage(5 * Time.deltaTime); // Damage the player
    }

    // End the fire damage after 5 seconds
    private IEnumerator EndFireDamage()
    {
        yield return new WaitForSeconds(5);
        isOnFire = false;
        firePS.SetActive(false);
    }
}
