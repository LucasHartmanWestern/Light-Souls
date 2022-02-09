using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls; // Reference to the Input System PlayerControls
    PlayerAnimationManager playerAnimationManager; // Reference to PlayerAnimationManager script
    public Vector2 movementInput; // 2D vector tracking where the player is trying to move
    private float _moveAmount; // Determine the amount to move

    public float verticalInput; // Track vertical input
    public float horizontalInput; // Track horizontal input

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

            // Record player input into the Vector2 movementInput
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
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

        _moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput)); // Clamp movement between 0 and 1
        playerAnimationManager.UpdateAnimatorValues(0, _moveAmount); // Update the player's movement animation
    }
}
