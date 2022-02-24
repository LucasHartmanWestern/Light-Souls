using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGeneral : MonoBehaviour
{
    PlayerAnimationManager playerAnimationManager; // Reference to enemy animator
    [SerializeField] AudioSource deathSound; // Reference to the audio that plays when the player dies
    protected InputManager inputManager; // InputManager reference
    protected Rigidbody rigidBody; // RigidBody reference
    protected Transform cameraTransform; // Transform of the camera the player sees through

    [Header("Player Stats")]
    public bool isAlive = true; // Track if player is alive
    public float playerMoveSpeed = 1f; // Track how fast player moves
    public float playerLookSpeed = 1f;
    public float playerHealth; // Track player's health
    public float playerExperience; // Track player exp
    public float rangedDamage; // How much damage player's ranged attack does
    public float meleeDamage; // How much damage player's melee attack does

    // Called before Start()
    private void Awake()
    {
        playerAnimationManager = GetComponent<PlayerAnimationManager>(); // Get PlayerAnimationManager attached to player
        inputManager = GetComponent<InputManager>(); // Get InputManager attached to player
        rigidBody = GetComponent<Rigidbody>(); // Get RigidBody attached to player
        cameraTransform = Camera.main.transform; // Get transform of the main camera
    }

    // Called once a frame
    private void Update()
    {
        if (isAlive && GetComponent<PlayerMovement>().isGrounded) HandleMovementAbility(); // Handles the movement ability of the player
    }

    // Called to damage the player
    public void TakeDamage(float damageAmount)
    {
        playerHealth -= damageAmount; // Decrease player's health

        if (playerHealth <= 0)
        {
            isAlive = false; // Show player as no longer alive
            GetComponent<CapsuleCollider>().enabled = false; // Remove capsule collider to avoid player floating
            playerAnimationManager.AplpyRootMotion(); // Apply root motion for death animation only
            playerAnimationManager.PlayTargetAnimation("Death", true); // Play death animation
            StartCoroutine(DestroyPlayer()); // Destroy enemy game obejct
        }
    }

    protected virtual void HandleMovementAbility() { } // Handles the special ability of the player

    // Handle the destroying of the enemy GameObject
    IEnumerator DestroyPlayer()
    {
        deathSound.Play(); // Play the death sound
        yield return new WaitForSeconds(5f); // Wait 5 seconds before destroying the object
        Debug.Log("You Died"); // Destroy the game object
        yield return new WaitForSeconds(2f); // Wait 2 seconds before respawning
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //loads the current scene again
    }
}
