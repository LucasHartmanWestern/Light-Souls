using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls; // Reference to the Input System PlayerControls
    PlayerMovement playerMovement; // Reference to the PlayerMovement script
    PlayerAnimationManager playerAnimationManager; // Reference to PlayerAnimationManager script
    
    [Header("Input from the Input Controller")]
    public Vector2 movementInput; // 2D vector tracking where the player is trying to move
    public Vector2 cameraInput; // 2D vector tracking how the player is trying to adjust the camera

    [Header("Variables used to perform the movement")]
    public float verticalInput; // Track vertical input
    public float horizontalInput; // Track horizontal input
    public float cameraInputX; // Track camera input on the x axes
    public float cameraInputY; // Track the camera input on the y axes

    public float moveAmount; // Determine the amount to move
    public bool sprintInput; // Check if player is trying to sprint
    public bool jumpInput; // Check if player is trying to jump
    public bool aimInput; // Check if player is trying to aim
    public bool attackInput; // Check if player is trying to attack

    // Called right before Start() method
    private void Awake()
    {
        playerAnimationManager = GetComponent<PlayerAnimationManager>(); // Get the PlayerAnimationManager script attached to player
        playerMovement = GetComponent<PlayerMovement>(); // Get the PlayerMovement script attached to player
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
            playerControls.PlayerActions.JumpButton.canceled += i => jumpInput = false; // Set jumpInput to false when the JumpButton is no longer pressed

            playerControls.PlayerActions.AimButton.performed += i => aimInput = true; // Set aimInput to true when the AimButton is pressed
            playerControls.PlayerActions.AimButton.canceled += i => aimInput = false; // Set aimInput to false when the AimButton is no longer pressed

            playerControls.PlayerActions.AttackButton.performed += i => attackInput = true; // Set attackInput to true when the AttackButton is pressed
            playerControls.PlayerActions.AttackButton.canceled += i => attackInput = false; // Set attackInput to false when the AttackButton is no longer pressed
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
        // Set whether or not the player is sprinting in the PlayerMovement script
        if (sprintInput && moveAmount > 0.5f) playerMovement.isSprinting = true;
        else playerMovement.isSprinting = false;

        // If player tries to jump, set the input to false (to only jump once) then call the HandleJump method
        if (jumpInput)
        {
            jumpInput = false;
            playerMovement.HandleJumping();
        }

        playerMovement.isAiming = aimInput; // Make player face where they're aiming
    }
}
