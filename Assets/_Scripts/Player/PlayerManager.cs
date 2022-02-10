using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager; // Input Manager instance
    PlayerMovement playerMovement; // PlayerMovement instance
    CameraManager cameraManager; // CameraManager instance
    Animator animator; // Animator instance

    public bool isInteracting; // Track whether or not the player is interacting with something

    // Called right before Start() method
    private void Awake()
    {
        inputManager = GetComponent<InputManager>(); // Reference to InputManager attached to player
        playerMovement = GetComponent<PlayerMovement>(); // Reference to PlayerMovement script attached to player
        cameraManager = FindObjectOfType<CameraManager>(); // Reference to the object with the CameraManager script attached to it
        animator = GetComponent<Animator>(); // Reference to Animator attached to player
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

    // Called after all the other updates
    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement(); // Make the camera follow the target

        isInteracting = animator.GetBool("isInteracting"); // Set this bool to whatever it is on the animator
        playerMovement.isJumping = animator.GetBool("isJumping"); // Set the PlayerMovement isJumping bool to match the one found in the animator

        animator.SetBool("isGrounded", playerMovement.isGrounded); // Set the isGrounded bool to match the one found in the PlayerMovement script
    }
}