using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls; // Reference to the Input System PlayerControls
    PlayerAnimationManager playerAnimationManager; // Reference to PlayerAnimationManager script
    
    [Header("Input from the Input Controller")]
    public Vector2 movementInput; // 2D vector tracking where the player is trying to move
    public Vector2 cameraInput; // 2D vector tracking how the player is trying to adjust the camera

    [Header("Variables used to perform the movement")]
    public float verticalInput; // Track vertical input
    public float horizontalInput; // Track horizontal input
    public float cameraInputX; // Track camera input on the x axes
    public float cameraInputY; // Track the camera input on the y axes

    private float _moveAmount; // Determine the amount to move

    // Called right before Start() method
    private void Awake()
    {
        playerAnimationManager = GetComponent<PlayerAnimationManager>(); // Get the PlayerAnimationManager script attached to player
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
        // Handle Jumping Input
        // Handle Action Input
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

        _moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput)); // Clamp movement between 0 and 1
        playerAnimationManager.UpdateAnimatorValues(0, _moveAmount); // Update the player's movement animation
    }
}
