using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    InputManager inputManager; // Reference to InputManager class
    PlayerGeneral playerGeneral; // Reference to the PlayerGeneral class
    PlayerAnimationManager playerAnimationManager; // Reference to the PlayerAnimationManager class
    [SerializeField] private LayerMask _aimColliderMask; // All layers that player can aim at
    [SerializeField] AudioSource shootSoundEffect; // Get the audio source attached to the player that contains the sound effect for shooting
    [SerializeField] AudioSource swingSoundEffect; // Get the audio source attached to the player that contains the sound effect for swinging their melee weapon

    [Header("Game Objects/Transforms")]
    public GameObject projectilePrefab; // Get reference to the projectile prefab
    public Transform muzzleFlashPS; // Get reference to the muzzle flash prefab
    public Transform spawnBulletPosition; // Point where bullet spawns when fired
    public Transform meleeWeapon; // Reference to player's melee weapon
    public Transform rangedWeapon; // Reference to player's ranged weapon
    public Transform handTransform; // Reference to player's hand transform

    // Called before Start()
    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>(); // Get reference to instance of InputManager
        playerGeneral = GetComponent<PlayerGeneral>(); // Get refernce to instance of PlayerGeneral attached to player
        playerAnimationManager = GetComponent<PlayerAnimationManager>(); // Get reference to instance of PlayerAnimationManager
    }

    // Called after Awake()
    private void Start()
    {
        meleeWeapon.SetParent(handTransform); // Track melee weapon to hands
        rangedWeapon.SetParent(handTransform); // Track ranged weapon to hands
    }

    // Update is called once per frame
    void Update()
    {
        // Only handle combat if player is alive
        if (playerGeneral.isAlive)
        {
            // Handle the combat depending on whether or not character is aiming
            if (inputManager.aimInput) HandleRangedCombat();
            else HandleMeleeCombat();
        }

        HandleWeaponModels(); // Move and select weapons appropriately
    }

    // Only let 1 weapon be active at a time and track models to player's hands
    private void HandleWeaponModels()
    {
        // Only have 1 weapon enabled at a time and have it switch depending on the player's input
        if (inputManager.aimInput)
        {
            meleeWeapon.gameObject.SetActive(false);
            rangedWeapon.gameObject.SetActive(true);
        }
        else
        {
            meleeWeapon.gameObject.SetActive(true);
            rangedWeapon.gameObject.SetActive(false);
        }
    }

    // Handle the Ranged Combat
    void HandleRangedCombat()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f); // Get position of the center of the screen
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint); // Cast ray from center of the screen forwards
        
        // Triggers if Raycast hits something
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, _aimColliderMask))
        {
            // Player is attacking
            if (inputManager.attackInput)
            { 
                Vector3 aimDirection = (raycastHit.point - spawnBulletPosition.position).normalized; // Determine where the user is aiming relative to the player
                Instantiate(muzzleFlashPS, spawnBulletPosition.position, Quaternion.LookRotation(aimDirection, Vector3.up)); // Create a yellow muzzle flash
                Instantiate(projectilePrefab, spawnBulletPosition.position, Quaternion.LookRotation(aimDirection, Vector3.up)); // Spawn in a bullet
                shootSoundEffect.Play(); // Play the shooting sound effect
                inputManager.attackInput = false; // Make it so player must press the attack button each time (semi-automatic)
            }  
        }
    }

    // Handles the melee combat
    void HandleMeleeCombat()
    {
        // Make sure player is not on the ground and not interacting
        if (inputManager.attackInput)
        {
            playerAnimationManager.PlayTargetAnimation("Sword Swing", true); // Call the landing animation and don't allow the player to break out of it
            swingSoundEffect.Play(); // Play the sound effect for a swing
            inputManager.attackInput = false; // Make it so player must press the attack button each time

            gameObject.GetComponent<Rigidbody>().velocity = transform.forward * 3;
        }
    }
}
