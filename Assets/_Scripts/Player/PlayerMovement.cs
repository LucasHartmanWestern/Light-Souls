using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputManager; // Input Manager instance

    Vector3 moveDirection; // Direction player moves
    Transform cameraTransform; // Transform of the camera the player sees through
    Rigidbody playerRigidBody; // Reference to player's RigidBody component
    PlayerManager playerManager; // Reference to PlayerManager script attached to this object
    PlayerAnimationManager playerAnimationManager; // Reference to PlayerAnimationManager script attached to this object

    [Header("Falling Settings")]
    public float inAirTimer; // Track how long player is in the air for
    public float leapingVelocity; // Specify how much the player should move forward when they begin to fall
    public float fallingVelocity; // Specify how fast the player falls
    public float rayCastHeightOffset; // Specify how much to offset the height of the origin of the raycast
    public LayerMask groundLayer; // Specify what the player wont fall through

    [Header("Movement Flags")]
    public bool isGrounded; // Check if the player is on the ground
    public bool isSprinting; // Check if player is sprinting
    public bool isJumping; // Check if the player is trying to jump

    [Header("Movement Stats")]
    public float walkingSpeed = 1.5f; // How fast the player can walk
    public float runningSpeed = 5f; // How fast the player can run
    public float sprintingSpeed = 8f; // How fast the player can sprint
    public float rotationSpeed = 15f; // How fast the player can rotate on the y axis

    [Header("Jumping Stats")]
    public float jumpHeight = 3; // Height the player can jump
    public float gravityIntensity = -15; // Speed at which gravity acts on player's jump

    // Called right before Start() method
    private void Awake()
    {
        inputManager = GetComponent<InputManager>(); // Reference to InputManager attached to player
        playerRigidBody = GetComponent<Rigidbody>(); // Reference to RigidBody attached to player
        cameraTransform = Camera.main.transform; // Get transform of the main camera
        playerManager = GetComponent<PlayerManager>(); // Reference to PlayerManager script attached to player
        playerAnimationManager = GetComponent<PlayerAnimationManager>(); // Reference to PlayerAnimation script attached to player
    }

    // Public method to call the other movement functions
    public void HandleAllPlayerMovement()
    {
        HandleFallingAndLanding(); // Handles the falling/landing of the player

        if (playerManager.isInteracting) return; // Disable movement if player is interacting with anything

        HandlePlayerMovement(); // Handles the movement on the x and z axes
        HandlePlayerRotation(); // Handles the rotation of the player
    }

    // Handles the movement for the player in the x and z axes
    private void HandlePlayerMovement()
    {
        if (isJumping) return; // Don't move in the air while jumping

        moveDirection = cameraTransform.forward * inputManager.verticalInput; // Get direction of vertical movement
        moveDirection = moveDirection + cameraTransform.right * inputManager.horizontalInput; // Get direction of horizontal movement
        moveDirection.Normalize(); // Change length of vector to 1
        moveDirection.y = 0; // Player should not move on the y axis

        Vector3 movementVelocity; // Determines how fast and in what direction the player is moving
        // Set the movemenet velocity based on if the player is walking, running, or sprinting
        if (isSprinting) movementVelocity = moveDirection * sprintingSpeed; // Velocity of player sprinting
        else
        {
            if (inputManager.moveAmount >= 0.5f) movementVelocity = moveDirection * runningSpeed; // Velocity of player running
            else movementVelocity = moveDirection * walkingSpeed; // Velocity of player walking
        }

        playerRigidBody.velocity = movementVelocity; // Change the RigidBody attached to the player to match the movementVelocity variable
    }

    // Handles the rotation for the player
    private void HandlePlayerRotation()
    {
        if (isJumping) return; // Don't rotate in the air while jumping

        Vector3 targetDirection = Vector3.zero; // Start out at (0, 0, 0)
        targetDirection = cameraTransform.forward * inputManager.verticalInput; // Face player in direction of vertical movement
        targetDirection = targetDirection + cameraTransform.right * inputManager.horizontalInput; // Face player in direction of horizontal movement
        targetDirection.Normalize(); // Set magnitude to 1
        targetDirection.y = 0; // Set value on y axis to 0

        if (targetDirection == Vector3.zero) targetDirection = transform.forward; // Keep rotation at position last specified by the player

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection); // Look towards the target direction defined above
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // Get quaternion of the player's roation

        transform.rotation = playerRotation; // Rotate the transform of the player
    }

    // Handles the falling and landing animation and physics for the player
    private void HandleFallingAndLanding()
    {
        RaycastHit hit; // Make a Raycast
        Vector3 rayCastOrigin = transform.position; // Initiate the raycast at the feet of the player
        rayCastOrigin.y += rayCastHeightOffset; // Offset the starting height of the raycast

        // Only play falling animation if player is not on the ground and not jumping
        if (!isGrounded && !isJumping)
        {
            if (!playerManager.isInteracting)
            {
                playerAnimationManager.PlayTargetAnimation("Falling", true); // Call the falling animation and don't allow player to break out of it
            }

            inAirTimer += Time.deltaTime; // Increase the timer tracking air time
            playerRigidBody.AddForce(transform.forward * leapingVelocity); // Add a slight forward force to the player
            playerRigidBody.AddForce(-Vector3.up * fallingVelocity * inAirTimer); // Add falling force to player
        }
        
        // Check if player is on top of an object in the groundLayer
        if(Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
        {
            // Make sure player is not on the ground and not interacting
            if(!isGrounded && playerManager.isInteracting)
            {
                playerAnimationManager.PlayTargetAnimation("Land", true); // Call the landing animation and don't allow the player to break out of it
            }

            inAirTimer = 0; // Reset the air time tracker
            isGrounded = true; // Specify that the player is now on the ground
        }
        else { isGrounded = false; } // If raycast doesn't detect the ground then the player is not grounded
    }

    // Handles jumping animation and physics for the player
    public void HandleJumping()
    {
        // Only let player jump if they're on the ground
        if (isGrounded)
        {
            playerAnimationManager.animator.SetBool("isJumping", true); // Set the bool in the animator
            playerAnimationManager.PlayTargetAnimation("Jumping", false); // Play animation in the animator

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight); // Get the velocity for the jump
            Vector3 playerVelocity = moveDirection; // Get the moveDirection variable and set it to the playerVelocity (to keep player movement pre-jump)
            playerVelocity.y = jumpingVelocity; // Add the jumpingVelocity as the velocity in the y axis
            playerRigidBody.velocity += playerVelocity; // Apply the newly calcualted velocity to the RigidBody of the player
        }
    }
}
