using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls; // Reference to the Input System PlayerControls
    PlayerMovement playerMovement; // Reference to the PlayerMovement script
    PlayerGeneral playerGeneral; // Reference to the PlayerGeneral script
    PlayerAnimationManager playerAnimationManager; // Reference to PlayerAnimationManager script
    CameraManager cameraManager; // Reference to the CameraManager script
    
    [Header("Input from the Input Controller")]
    public Vector2 movementInput; // 2D vector tracking where the player is trying to move
    public Vector2 cameraInput; // 2D vector tracking how the player is trying to adjust the camera

    [Header("Variables used to perform the movement")]
    public float verticalInput; // Track vertical input
    public float horizontalInput; // Track horizontal input
    public float cameraInputX; // Track camera input on the x axes
    public float cameraInputY; // Track the camera input on the y axes

    [Header("Boolean variables to track inputs")]
    public float moveAmount; // Determine the amount to move
    public bool sprintInput; // Check if player is trying to sprint
    public bool jumpInput; // Check if player is trying to jump
    public bool aimInput; // Check if player is trying to aim
    public bool attackInput; // Check sif player is trying to attack
    public bool specialMoveInput; // Check if player is trying to use their special moving abilitiy
    public bool lockOnInput; // Check if player is trying to lock onto an enemy
    public bool lockOnLeftInput; // Check if player is trying to lock onto a different enemy
    public bool lockOnRightInput; // Check if player is trying to lock onto a different enemy
    public bool specialAbilityInput; // Check if player is trying to use their special ability
    public bool reloadInput; // Check if player is trying to reload

    [Header("Toggled flags for certain inputs")]
    public bool lockOnFlag; // Check if player should be locked on

    // Called right before Start() method
    private void Awake()
    {
        playerAnimationManager = GetComponent<PlayerAnimationManager>(); // Get the PlayerAnimationManager script attached to player
        playerMovement = GetComponent<PlayerMovement>(); // Get the PlayerMovement script attached to player
        playerGeneral = GetComponent<PlayerGeneral>(); // Get the PlayerGeneral script attached to player
        cameraManager = FindObjectOfType<CameraManager>(); // Get the CameraManager script
    }

    // Run when object script is attached to becomes enabled
    private void OnEnable()
    {
        if (playerControls == null)
        {
            // Instantiate instance of PlayerControls class
            playerControls = new PlayerControls();

            // Record player input into the Vector2 movementInput and cameraInput
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            #region Action Input Setting
            playerControls.PlayerActions.SprintButton.performed += i => sprintInput = true; // Set sprintInput to true when the SprintButton is pressed
            playerControls.PlayerActions.SprintButton.canceled += i => sprintInput = false; // Set sprintInput to false when the SprintButton is no longer pressed

            playerControls.PlayerActions.JumpButton.performed += i => jumpInput = true; // Set jumpInput to true when the JumpButton is pressed

            playerControls.PlayerActions.AimButton.performed += i => aimInput = true; // Set aimInput to true when the AimButton is pressed
            playerControls.PlayerActions.AimButton.canceled += i => aimInput = false; // Set aimInput to false when the AimButton is no longer pressed

            playerControls.PlayerActions.AttackButton.performed += i => attackInput = true; // Set attackInput to true when the AttackButton is pressed
            playerControls.PlayerActions.AttackButton.canceled += i => attackInput = false; // Set attackInput to false when the AttackButton is no longer pressed

            playerControls.PlayerActions.SpecialMoveButton.performed += i => specialMoveInput = true; // Set specialMoveInput to true when the SpecialMoveButton is pressed
            playerControls.PlayerActions.SpecialMoveButton.canceled += i => specialMoveInput = false; // Set specialMoveInput to false when the SpecialMoveButton is no longer pressed

            playerControls.PlayerActions.SpecialAbilityButton.performed += i => specialAbilityInput = true; // Set specialAbilityInput to true when the SpecialAbilityButton is pressed
            playerControls.PlayerActions.SpecialAbilityButton.canceled += i => specialAbilityInput = false; // Set specialAbilityInput to false when the SpecialAbilityButton is no longer pressed

            playerControls.PlayerActions.LockOnButton.performed += i => lockOnInput = true; // Set lockOnInput to true when the lockOnButton is pressed
            playerControls.PlayerActions.LockOnTargetLeft.performed += i => lockOnLeftInput = true; // Set lockOnLeftInput to true when the LockOnTargetLeft is pressed
            playerControls.PlayerActions.LockOnTargetRight.performed += i => lockOnRightInput = true; // Set lockOnRightInput to true when the LockOnTargetRight is pressed

            playerControls.PlayerActions.ReloadButton.performed += i => reloadInput = true; // Set reloadInput to true when the ReloadButton is pressed
            #endregion
        }

        // Enable the PlayerControls instance
        playerControls.Enable();
    }

    // Runs when object script is attached to becomes disabled
    private void OnDisable()
    {
        // Disable the PlayerControls instance
        playerControls.Disable();
    }

    // Handles all inputs for the player
    public void HandleAllInputs()
    {
        HandleMovementInput(); // Handle movement inputs
        HandleActionInput(); // Handle the action inputs
    }

    // Handles all of the movement inputs
    private void HandleMovementInput()
    {
        // Get the horizontal and vertial inputs
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        // Get the inputs for the camera on both axes
        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput)); // Clamp movement between 0 and 1
        playerAnimationManager.UpdateAnimatorValues(0, moveAmount, sprintInput); // Update the player's movement animation
    }

    // Handles all of the inputs related to actions
    private void HandleActionInput()
    {
        #region Sprint Input
        // Set whether or not the player is sprinting in the PlayerMovement script
        if (sprintInput && moveAmount > 0.5f) playerMovement.isSprinting = true;
        else playerMovement.isSprinting = false;
        #endregion

        #region Jump Input
        // If player tries to jump, set the input to false (to only jump once) then call the HandleJump method
        if (jumpInput)
        {
            jumpInput = false;
            playerMovement.HandleJumping();
        }
        #endregion

        #region Reload Input
        // If player tries to jump, set the input to false (to only jump once) then call the HandleJump method
        if (reloadInput)
        {
            reloadInput = false;
            playerGeneral.isReloading = true;
        }
        #endregion

        #region Aim Input
        playerMovement.isAiming = aimInput; // Make player face where they're aiming
        if (aimInput)
        {
            playerAnimationManager.PlayerAim(1f); // Make the player animate to aim if player is aiming
            cameraManager.ClearLockOnTargets(); // Don't let player aim and lock on
            lockOnInput = false;
            lockOnFlag = false;
        }
        else playerAnimationManager.PlayerAim(0f);
        #endregion

        #region Lock On Input and Lock On Flag
        if (lockOnInput && !lockOnFlag) // If player tries to lock on and they aren't currently locked on, set the input to false then call the HandleLockOn method
        {
            lockOnInput = false;
            cameraManager.HandleLockOn();
            if (cameraManager.nearestLockOnTarget != null)
            {
                cameraManager.currentLockOnTarget = cameraManager.nearestLockOnTarget; // By default, lock onto closest target
                cameraManager.currentLockOnTarget.GetComponentInParent<EnemyHealthBar>().healthBar.SetActive(true); // Enable the health bar when locked onto
                lockOnFlag = true; // Flag that player is locked on
            }
        }
        else if (lockOnInput && lockOnFlag) // If player tries to lock on and they are currently locked on, set the input to false then call the HandleLockOff method
        {
            lockOnInput = false;
            lockOnFlag = false;
            cameraManager.ClearLockOnTargets(); // Clear the possible lock on targets
        }
        if (lockOnFlag && lockOnLeftInput) // Lock onto the target left to the current target
        {
            lockOnLeftInput = false;
            cameraManager.HandleLockOn();
            if (cameraManager.leftLockTarget != null)
            {
                cameraManager.currentLockOnTarget = cameraManager.leftLockTarget;
                cameraManager.currentLockOnTarget.GetComponentInParent<EnemyHealthBar>().healthBar.SetActive(true); // Enable the health bar when locked onto
            }
        }
        if (lockOnFlag && lockOnRightInput) // Lock onto the target right to the current target
        {
            lockOnRightInput = false;
            cameraManager.HandleLockOn();
            if (cameraManager.rightLockTarget != null)
            {
                cameraManager.currentLockOnTarget = cameraManager.rightLockTarget;
                cameraManager.currentLockOnTarget.GetComponentInParent<EnemyHealthBar>().healthBar.SetActive(true); // Enable the health bar when locked onto
            }
        }
        #endregion
    }
}
