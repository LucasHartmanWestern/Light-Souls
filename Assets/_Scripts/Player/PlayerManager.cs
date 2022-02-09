using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager; // Input Manager instance
    PlayerMovement playerMovement; // PlayerMovement instance

    private void Awake()
    {
        inputManager = GetComponent<InputManager>(); // Reference to InputManager attached to player
        playerMovement = GetComponent<PlayerMovement>(); // Reference to PlayerMovement script attached to player
    }

    // Runs once every frame
    private void Update()
    {
        inputManager.HandleAllInputs(); // Call the HandleAllInputs method in the InputManager
    }

    // Used for physics updates
    private void FixedUpdate()
    {
        playerMovement.HandleAllPlayerMovement(); // Call the HandleAllPlayerMovement method in the PlayerMovement script
    }
}