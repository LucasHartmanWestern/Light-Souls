using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager; // Reference to the InputManager
    PlayerGeneral playerGeneral; // Reference to PlayerGeneral
    public bool noMenu = true; // Check if menu is showing

    private Vector3 _cameraFollowVelocity = Vector3.zero; // Vector3 to track the current velocity

    public LayerMask collisionLayers; // All layers the camera should collide with

    [Header("Camera Tracking Variables")]
    public Transform targetTransform; // The object the camera will follow
    public Transform cameraPivot; // The object the camera uses to pivot
    public Transform cameraTransform; // The transform of the main camera object in the scene
    public float cameraFollowSpeed = 0.2f; // How fast the camera will move when following the player
    private Vector3 cameraVectorPosition;

    [Header("Camera Lock On Varaibles")]
    public float maximumLockOnDistance = 30; // Max distance enemy can be from player to lock on
    List<EnemyGeneral> availableTargets = new List<EnemyGeneral>(); // List to hold all possible targets the player can lock on to
    public Transform nearestLockOnTarget; // Find nearest target to lock on to
    public Transform currentLockOnTarget; // Holds transform of enemy player is locked on to
    public Transform leftLockTarget; // Holds target transform of enemy to the left of the current target
    public Transform rightLockTarget; // Holds target transform of enemy to the right of the current target

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

    
    // Called right after Start() method
    private void Start()
    {
        targetTransform = FindObjectOfType<PlayerGeneral>().transform; // Get the transform of the player
        inputManager = FindObjectOfType<InputManager>(); // Get the InputManager
        cameraTransform = Camera.main.transform; // Get the transform of the main camera
        defaultPosition = cameraTransform.localPosition.z; // Get z axis position of the camera's transform
        playerGeneral = FindObjectOfType<PlayerGeneral>(); // Get reference to PlayerGeneral script
    }

    void Update()
    {
        if (playerGeneral == null) playerGeneral = FindObjectOfType<PlayerGeneral>(); // Get reference to PlayerGeneral script
        if (targetTransform == null) targetTransform = FindObjectOfType<PlayerGeneral>().transform; // Get the transform of the player
        if (inputManager == null) inputManager = FindObjectOfType<InputManager>(); // Get the InputManager
        if (cameraTransform == null) cameraTransform = Camera.main.transform; // Get the transform of the main camera
    }

    // Public method to handle the camera movement
    public void HandleAllCameraMovement()
    {
        if (!noMenu) return; // Don't run if menu is active

        FollowTarget(); // Move the camera with the player
        RotateCamera(); // Rotate the camera according to the player input
        HandleCameraCollisions(); // Adjust the camera if it collides with an object
        if (playerGeneral.isAlive) HandleCameraAim(); // Adjust the camera if player is aiming
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
        if (!inputManager.lockOnFlag && currentLockOnTarget == null && !FindObjectOfType<EquipableItems>().aimbotLockOn) // Check player is not locked on
        {
            Vector3 rotation; // Specify the rotation of the camera
            Quaternion targetRotation; // Target rotation in a Quaterion form

            lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed * playerGeneral.playerLookSpeed); // Set the look angle based on player input
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
        else // Player is locked on
        {
            float velocity = 0;

            Vector3 direction = currentLockOnTarget.position - transform.position; // Get direction to point camera
            direction.Normalize(); // Normalize direction
            direction.y = 0; // Set y value to 0
            Quaternion targetRotation = Quaternion.LookRotation(direction); // Get look rotation to face the direction of the target
            transform.rotation = targetRotation; // Set the rotation of the camera's transform to look at the target

            direction = currentLockOnTarget.position - cameraPivot.position; // Get direction from camera pivot
            direction.Normalize(); // Normalize direction
            targetRotation = Quaternion.LookRotation(direction); // Get a new look rotation to face the target
            Vector3 eulerAngles = targetRotation.eulerAngles; // Get the euler angles of the new rotation
            eulerAngles.y = 0; // Set y to 0
            cameraPivot.localEulerAngles = eulerAngles; // Rotate camera pivot towards target
        }
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
            cameraPivot.transform.localPosition = new Vector3(1, 1.25f, 2); // Set new position for 
            cameraFollowSpeed = 0; // Make camera more responsive
            cameraLookSpeed = 0.1f; // Make camera move slower
            cameraPivotSpeed = 0.1f; // Make camera move slower
        }
        // Player is not aiming
        else if (!inputManager.aimInput)
        {
            cameraPivot.transform.localPosition = new Vector3(0, 2, 0); // Set new position for camer
            cameraFollowSpeed = 0.2f; // Make camera more smooth
            cameraLookSpeed = 0.25f; // Make camera move faster
            cameraPivotSpeed = 0.25f; // Make camera move faster
        }
    }

    // Adjust the camera to lock onto a specific enemy
    public void HandleLockOn()
    {
        float shortestDistance = Mathf.Infinity; // Measure distance between targets and player and pick the shortest one
        float shortestDistanceOfLeftTarget = Mathf.Infinity; // Measure distance between targets and current target and pick the shortest one on the left
        float shortestDistanceOfRightTarget = Mathf.Infinity; // Measure distance between targets and current target and pick the shortest one on the right

        Collider[] colliders = Physics.OverlapSphere(targetTransform.position, 26); // Search around the player a distance of 26 units for colliders

        // Loop through everything detected in the sphere
        for(int i = 0; i < colliders.Length; i++)
        {
            EnemyGeneral enemy = colliders[i].GetComponent<EnemyGeneral>(); // Search through colliders for an EnemyGeneral script
            if (enemy != null && cameraTransform != null)
            {
                Vector3 lockTargetDirection = enemy.transform.position - targetTransform.position; // Get Vector3 to hold the direction of the target compared to the player
                float distanceFromTarget = Vector3.Distance(targetTransform.position, enemy.transform.position); // Get distance from player to target
                float viewableAngle = Vector3.Angle(lockTargetDirection, cameraTransform.forward); // Detect angle between the target and the current forward of the camera

                // Check that the enemy is on screen and within an acceptable distance
                if (enemy.transform.root != targetTransform.transform.root && viewableAngle > -50 && viewableAngle < 50 && distanceFromTarget <= maximumLockOnDistance && enemy.isAlive && enemy.isHostile)
                    availableTargets.Add(enemy); // Add enemy as potential target
            }
        }

        // Loop through available targets for player to lock on to
        for (int j = 0; j < availableTargets.Count; j++)
        {
            // Get distacne from player
            float distanceFromTarget = Vector3.Distance(targetTransform.position, availableTargets[j].transform.position);

            if (distanceFromTarget < shortestDistance)
            {
                shortestDistance = distanceFromTarget; // Find closest distance
                nearestLockOnTarget = availableTargets[j].lockOnTransform; // Set enemy as closest target
            }

            if (inputManager.lockOnFlag)
            {
                Vector3 relativeEnemyPosition = currentLockOnTarget.InverseTransformPoint(availableTargets[j].transform.position); // Find position of enemies relative to the current lock on target
                var distanceFromLeftTarget = currentLockOnTarget.transform.position.x - availableTargets[j].transform.position.x; // Find distance from the target on the left side
                var distanceFromRightTarget = currentLockOnTarget.transform.position.x + availableTargets[j].transform.position.x; // Find distance from the target on the right side

                // Find closest enemy to left of current enemy
                if (relativeEnemyPosition.x > 0.00 && distanceFromLeftTarget < shortestDistanceOfLeftTarget)
                {
                    shortestDistanceOfLeftTarget = distanceFromLeftTarget;
                    leftLockTarget = availableTargets[j].lockOnTransform;
                }

                // Find closest enemy to right of current enemy
                if (relativeEnemyPosition.x < 0.00 && distanceFromRightTarget < shortestDistanceOfRightTarget)
                {
                    shortestDistanceOfRightTarget = distanceFromRightTarget;
                    rightLockTarget = availableTargets[j].lockOnTransform;
                }
            }
        }
    }

    // Clear the possible lock on target
    public void ClearLockOnTargets()
    {
        foreach(EnemyGeneral enemy in availableTargets)
        {
            if (enemy.transform.GetComponent<EnemyHealthBar>().currentHealth == enemy.transform.GetComponent<EnemyHealthBar>().startingHealth && enemy.transform.GetComponent<EnemyHealthBar>().healthBar.activeSelf)
                enemy.transform.GetComponent<EnemyHealthBar>().healthBar.SetActive(false);
        }
        availableTargets.Clear();
        nearestLockOnTarget = null;
        currentLockOnTarget = null;
    }
}
