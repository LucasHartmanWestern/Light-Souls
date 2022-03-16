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

    [Header("Required objects")]
    public Transform deathPS; // Death particle system reference
    public Transform lockOnTransform; // Transform that the player can lock on to


    [Header("Enemy Stats")]
    public bool isAlive; // Track if this enemy instance is alive
    public float enemyHealth; // Track health of enemy
    public float expOnDeath; // How much exp enemy gives player when they die
    public float enemyDamage; // Track damage enemy does to player
    public float walkSpeed = 1f; // How fast enemy patrols
    public float chaseSpeed = 8f; // How fast enemy chases

    // Called before Start()
    private void Awake()
    {
        animator = GetComponent<Animator>(); // Get animator attached to this enemy
        playerGeneral = FindObjectOfType<PlayerGeneral>(); // Get reference to Player General instance
        cameraManager = FindObjectOfType<CameraManager>(); // Get reference to Camera Manager instance
        inputManager = FindObjectOfType<InputManager>(); // Get reference to Input Manager instance
    }

    // Called once a frame
    private void Update()
    {
        if (enemyHealth <= 0)
            isAlive = false; // Set that the enemy is dead
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
            
            if (cameraManager.rightLockTarget != null)
            {
                cameraManager.HandleLockOn();
                cameraManager.currentLockOnTarget = cameraManager.rightLockTarget;
            }
            else if (cameraManager.leftLockTarget != null)
            {
                cameraManager.HandleLockOn();
                cameraManager.currentLockOnTarget = cameraManager.leftLockTarget;
            }
            else
            {
                inputManager.lockOnFlag = false;
                cameraManager.ClearLockOnTargets();
            }

            StartCoroutine(DestroyEnemyObject()); // Destroy enemy game obejct
        }
    }

    // Handle the destroying of the enemy GameObject
    IEnumerator DestroyEnemyObject()
    {
        GetComponent<NavMeshAgent>().enabled = false; // Disable the NavMeshAgent
        deathSound.Play(); // Play the death sound

        yield return new WaitForSeconds(5f); // Wait 5 seconds before destroying the object
        Instantiate(deathPS, transform.position, Quaternion.identity); // Create a death particle system

        playerGeneral.playerExperience += expOnDeath; // Increase player exp
        Destroy(gameObject); // Destroy the game object
    }
}
