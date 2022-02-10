using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager; // Input Manager instance
    PlayerMovement playerMovement; // PlayerMovement instance
    CameraManager cameraManager;

    // Called right before Start() method
    private void Awake()
    {
        inputManager = GetComponent<InputManager>(); // Reference to InputManager attached to player
        playerMovement = GetComponent<PlayerMovement>(); // Reference to PlayerMovement script attached to player
        cameraManager = FindObjectOfType<CameraManager>(); // Reference to the object with the CameraManager script attached to it
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
    }
}