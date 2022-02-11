using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager; // Reference to the InputManager

    private Vector3 _cameraFollowVelocity = Vector3.zero; // Vector3 to track the current velocity

    public LayerMask collisionLayers; // All layers the camera should collide with

    [Header("Camera Tracking Variables")]
    public Transform targetTransform; // The object the camera will follow
    public Transform cameraPivot; // The object the camera uses to pivot
    public Transform cameraTransform; // The transform of the main camera object in the scene
    public float cameraFollowSpeed = 0.2f; // How fast the camera will move when following the player
    private Vector3 cameraVectorPosition;

    [Header("Camera Roation Variables")]
    public float lookAngle; // Make camera look up and down
    public float pivotAngle; // Make camera look left and right
    public float cameraLookSpeed = 2; // Speed the camera moves at on x axis
    public float cameraPivotSpeed = 2; // Speed the camera moves at on y axis
    public float minPivotAngle = -35; // The min angle the camera can rotate on the y axis
    public float maxPivotAngle = 50; // The max angle the camera can rotate on the y axis

    [Header("Camera Collision Variables")]
    public float cameraCollisionRadius = 0.2f; // Set the radius to determine when a collision occurs with the camera (to avoid clipping)
    public float cameraCollisionOffset = 0.2f; // Offset the camera will jump off objects it collides with
    public float minCollisionOffset = 0.2f; // Minimum offset for a collision to trigger a jump
    private float defaultPosition; // Default position camera always comes back to

    // Called right before Start() method
    private void Awake()
    {
        targetTransform = FindObjectOfType<PlayerManager>().transform; // Get the transform of the player
        inputManager = FindObjectOfType<InputManager>(); // Get the InputManager
        cameraTransform = Camera.main.transform; // Get the transform of the main camera
        defaultPosition = cameraTransform.localPosition.z; // Get z axis position of the camera's transform
    }

    // Public method to handle the camera movement
    public void HandleAllCameraMovement()
    {
        FollowTarget(); // Move the camera with the player
        RotateCamera(); // Rotate the camera according to the player input
        HandleCameraCollisions(); // Adjust the camera if it collides with an object
        HandleCameraAim(); // Adjust the camera if player is aiming
    }

    // Make camera follow a specified target
    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref _cameraFollowVelocity, cameraFollowSpeed); // Specify where the camera should look and smoothly transition between positions
        
        transform.position = targetPosition; // Update position of the transform on the game object this script is attached to
    }

    // Make the camera rotate according to the player's input
    private void RotateCamera()
    {
        Vector3 rotation; // Specify the rotation of the camera
        Quaternion targetRotation; // Target rotation in a Quaterion form

        lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed); // Set the look angle based on player input
        pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed); // Set the pivot angle based on player input
        pivotAngle = Mathf.Clamp(pivotAngle, minPivotAngle, maxPivotAngle); // Make it so you cannot rotate the pivotAngle forever

        #region Y axis Camera Movement
        rotation = Vector3.zero; // Starting rotation should be (0,0,0)
        rotation.y = lookAngle; // Set the rotation on the y axis to the look angle
        targetRotation = Quaternion.Euler(rotation); // Set the targetRotation variable to match the rotation Vector3 in Euler form
        transform.rotation = targetRotation; // Set the camera's transform to match the target rotation
        #endregion
        #region X axis Camera movment
        rotation = Vector3.zero; // Reset the rotation Vector3 to (0,0,0)
        rotation.x = pivotAngle; // Set the rotation on the x axis to the pivot angle
        targetRotation = Quaternion.Euler(rotation); // Set the targetRotation variable to match the rotation Vector3 in Euler form
        cameraPivot.localRotation = targetRotation; // Set the transform of the camera's pivot to match the target rotation
        #endregion
    }

    private void HandleCameraCollisions()
    {
        float targetPosition = defaultPosition; // Get the position the camera should reset back to after a collision
        RaycastHit hit; // Get info from the Raycast
        Vector3 direction = cameraTransform.position - cameraPivot.position; // Get Vector3 of the position the camera is facing
        direction.Normalize(); // Make magnitude 1

        // Check if there's a collision between the Camera and something in the collision layer specified
        if (Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point); // Get distance between camera's pivot and the thing the camera collided with
            targetPosition =- (distance - cameraCollisionOffset); // Get the new target position based on the collision offset
        }

        // Make camera jump off if it's within the specified limit of a collision offset
        if (Mathf.Abs(targetPosition) < minCollisionOffset)
            targetPosition = targetPosition - minCollisionOffset; // Change the target position based on the min collision offset

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f); // Get the new camera position after the collision
        cameraTransform.localPosition = cameraVectorPosition; // Set the camera's transform
    }

    // Adjust the camera when aiming
    private void HandleCameraAim()
    {
        // Player is aiming
        if (inputManager.aimInput)
        {
            cameraPivot.transform.localPosition = new Vector3(1, 1.25f, 2); // Set new position for camera
            cameraFollowSpeed = 0; // Make camera more responsive
        }
        // Player is not aiming
        else if (!inputManager.aimInput)
        {
            cameraPivot.transform.localPosition = new Vector3(0, 2, 0); // Set new position for camer
            cameraFollowSpeed = 0.2f; // Make camera more smooth
        }
    }
}
