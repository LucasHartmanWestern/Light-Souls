using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls; // Reference to the Input System PlayerControls
    public Vector2 movementInput; // 2D vector tracking where the player is trying to move

    public float verticalInput; // Track vertical input
    public float horizontalInput; // Track horizontal input

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
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
    }
}
