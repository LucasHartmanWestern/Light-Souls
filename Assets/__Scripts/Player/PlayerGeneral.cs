using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGeneral : MonoBehaviour
{
    protected PlayerAnimationManager playerAnimationManager; // Reference to enemy animator
    [SerializeField] AudioSource deathSound; // Reference to the audio that plays when the player dies
    protected InputManager inputManager; // InputManager reference
    protected Rigidbody rigidBody; // RigidBody reference
    protected Transform cameraTransform; // Transform of the camera the player sees through
    protected PlayerMovement playerMovement; // PlayerMovement reference
    protected CameraManager cameraManager; // CameraManager instance
    protected Animator animator; // Animator instance
    public bool isInteracting; // Track whether or not the player is interacting with something

    [Header("Player Stats")]
    public bool isAlive = true; // Track if player is alive
    public float jumpStrenth = 1f; // Track strength of player's jump
    public float playerMoveSpeed = 1f; // Track how fast player moves
    public float playerLookSpeed = 1f;
    public float playerHealth; // Track player's health
    public float playerExperience; // Track player exp
    public float rangedDamage; // How much damage player's ranged attack does
    public float meleeDamage; // How much damage player's melee attack does
    public float resistance = 1f; // How much damage player can absorb

    // Called before Start()
    private void Awake()
    {
        playerAnimationManager = GetComponent<PlayerAnimationManager>(); // Get PlayerAnimationManager attached to player
        inputManager = GetComponent<InputManager>(); // Get InputManager attached to player
        rigidBody = GetComponent<Rigidbody>(); // Get RigidBody attached to player
        playerMovement = GetComponent<PlayerMovement>(); // Get PlayerMovement script attached to player
        cameraTransform = Camera.main.transform; // Get transform of the main camera
        cameraManager = FindObjectOfType<CameraManager>(); // Reference to the object with the CameraManager script attached to it
        animator = GetComponent<Animator>(); // Reference to Animator attached to player
    }

    // Called once a frame
    private void Update()
    {
        inputManager.HandleAllInputs(); // Call the HandleAllInputs method in the InputManager
        if (isAlive && GetComponent<PlayerMovement>().isGrounded)
        {
            HandleMovementAbility(); // Handles the movement ability of the player
            HandleCombatAbility(); // Handles the combat ability of the player
        }
    }

    // Used for physics updates
    private void FixedUpdate()
    {
        playerMovement.HandleAllPlayerMovement(); // Call the HandleAllPlayerMovement method in the PlayerMovement script
    }

    // Called after all the other updates
    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement(); // Make the camera follow the target

        isInteracting = animator.GetBool("isInteracting"); // Set this bool to whatever it is on the animator
        playerMovement.isJumping = animator.GetBool("isJumping"); // Set the PlayerMovement isJumping bool to match the one found in the animator

        animator.SetBool("isGrounded", playerMovement.isGrounded); // Set the isGrounded bool to match the one found in the PlayerMovement script
    }

    // Called to damage the player
    public void TakeDamage(float damageAmount)
    {
        playerHealth -= damageAmount / resistance; // Decrease player's health

        if (playerHealth <= 0)
        {
            isAlive = false; // Show player as no longer alive
            GetComponent<CapsuleCollider>().enabled = false; // Remove capsule collider to avoid player floating
            GetComponent<Rigidbody>().isKinematic = true; // Make rigid body kinematic
            playerAnimationManager.AplpyRootMotion(true); // Apply root motion for death animation only
            playerAnimationManager.PlayTargetAnimation("Death", true); // Play death animation
            StartCoroutine(DestroyPlayer()); // Destroy enemy game obejct
        }
    }

    protected virtual void HandleMovementAbility() {} // Handles the special movement ability of the player

    protected virtual void HandleCombatAbility() {} // Handles the special combat ability of the player

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
