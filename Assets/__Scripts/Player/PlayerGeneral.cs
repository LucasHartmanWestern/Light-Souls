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
    public bool isReloading; // Track whether player is reloading or not

    [Header("Player Stats")]
    public bool isAlive = true; // Track if player is alive
    public float jumpStrenth = 1f; // Track strength of player's jump
    public float playerMoveSpeed = 1f; // Track how fast player moves
    public float playerLookSpeed = 1f;
    public float playerStartingHealth; // Track player's starting health
    public float playerHealth = 0; // Track player's current health
    public float playerStartingSpecial;
    public float playerSpecial = 0; // Track player's special
    public float playerLevel = 1; // Track player's level
    public float expToNextLevel = 1000; // Track exp needed to get to the next level
    public float playerExperience = 0; // Track player exp
    public float rangedDamage = 15; // How much damage player's ranged attack does
    public float meleeDamage = 15; // How much damage player's melee attack does
    public float resistance = 1f; // How much damage player can absorb
    public float playerMaganizeCapacity; // How many bullets player can fire before reloading
    public float playerAmmo; // How many bullets the player currently has loaded
    public float playerFireRate; // Fire rate of player

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
        playerHealth = playerStartingHealth; // Set the player's starting health
        playerSpecial = playerStartingSpecial; // Set the player's starting special meter
        playerAmmo = playerMaganizeCapacity; // Set player's starting ammo to the capacity
    }

    // Called once a frame
    private void Update()
    {
        inputManager.HandleAllInputs(); // Call the HandleAllInputs method in the InputManager
        if (isAlive)
        {
            HandleMovementAbility(); // Handles the movement ability of the player
            HandleCombatAbility(); // Handles the combat ability of the player
            HandleReload(); // Handles the reload ability

            #region Increase Special meter gradually
            // Always be increase the player special meter to 100 (unless their in the air)
            if (playerSpecial < playerStartingSpecial && playerMovement.isGrounded)
                playerSpecial += 5 * Time.deltaTime;
            else if (playerSpecial > playerStartingSpecial) playerSpecial = playerStartingSpecial;
            #endregion

            #region Level up the player
            if (playerExperience >= expToNextLevel)
            {
                playerExperience -= expToNextLevel;
                expToNextLevel += 1000;
                FindObjectOfType<PlayerGeneral>().playerStartingHealth += 10;
                FindObjectOfType<PlayerGeneral>().playerStartingSpecial += 10;
                FindObjectOfType<PlayerGeneral>().playerLevel += 1;
                FindObjectOfType<PlayerGeneral>().playerHealth = FindObjectOfType<PlayerGeneral>().playerStartingHealth;
                FindObjectOfType<PlayerGeneral>().playerSpecial = FindObjectOfType<PlayerGeneral>().playerStartingSpecial;
                playerStartingHealth += 10;
                playerStartingSpecial += 10;
                playerLevel += 1;
                playerHealth = playerStartingHealth;
                playerSpecial = playerStartingSpecial;
            }
            #endregion
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
        FindObjectOfType<PlayerGeneral>().playerHealth -= damageAmount / resistance; // Decrease inherited script playerHealth too

        #region Handle player dying
        if (FindObjectOfType<PlayerGeneral>().playerHealth <= 0)
        {
            isAlive = false; // Show player as no longer alive
            FindObjectOfType<PlayerGeneral>().isAlive = false; // Set child component of isAlive to false
            GetComponent<CapsuleCollider>().enabled = false; // Remove capsule collider to avoid player floating
            GetComponent<Rigidbody>().isKinematic = true; // Make rigid body kinematic
            playerAnimationManager.AplpyRootMotion(true); // Apply root motion for death animation only
            playerAnimationManager.PlayTargetAnimation("Death", true); // Play death animation
            StartCoroutine(DestroyPlayer()); // Destroy enemy game obejct
        }
        #endregion
    }

    // Handles the reload of the ranged weapon
    protected void HandleReload()
    {
        if (isReloading)
        {
            inputManager.aimInput = false; // Set player to not aiming
            playerAnimationManager.PlayTargetAnimation("Reloading", true); // Play reload animation
            FindObjectOfType<PlayerGeneral>().playerAmmo = FindObjectOfType<PlayerGeneral>().playerMaganizeCapacity; // Reset ammo of player
            isReloading = false; // Set that the player is no longer reloading
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
