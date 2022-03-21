using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildBossSon : PlayerGeneral
{
    public ParticleSystem jetPackPS; // Get particle system of the jetpack

    // Handles the special movement the player can perform
    protected override void HandleMovementAbility()
    {
        if (Input.GetButton("Jump") && !playerMovement.isGrounded && playerSpecial > 0) // Check if player is trying to jump in the air
        {
            playerSpecial -= 30 * Time.deltaTime; // Decrease player special meter by 30

            rigidBody.AddForce(Vector3.up * playerMovement.fallingVelocity * 1.5f); // Apply a force opposite to the gravity force

            Instantiate(jetPackPS, transform.position, Quaternion.Euler(new Vector3(90, 0, 0)), transform); // Create a red particle system

            #region Perform movement and rotation despite being in the air
            playerMovement.inAirTimer = 0; // Reset inAirTimer

            Vector3 moveDirection; // Direction player moves

            moveDirection = cameraTransform.forward * inputManager.verticalInput; // Get direction of vertical movement
            moveDirection = moveDirection + cameraTransform.right * inputManager.horizontalInput; // Get direction of horizontal movement
            moveDirection.Normalize(); // Change length of vector to 1
            moveDirection.y = 0; // Player should not move on the y axis

            Vector3 movementVelocity = Vector3.zero; // Determines how fast and in what direction the player is moving
            if (inputManager.moveAmount > 0) movementVelocity = moveDirection * 5; // Velocity of player hovering
            rigidBody.velocity = movementVelocity; // Change the RigidBody attached to the player to match the movementVelocity variable

            Vector3 targetDirection = Vector3.zero; // Start out at (0, 0, 0)

            // Face player at what they're aiming if they're aiming
            if (playerMovement.isAiming)
            {
                Vector3 worldAimTarget = Vector3.zero; // Set the worldAimTarget
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f); // Get position of the center of the screen
                Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint); // Cast ray from center of the screen forwards

                // Triggers if Raycast hits something
                if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f))
                    worldAimTarget = raycastHit.point; // Find where the mouse is aiming at

                Vector3 aimDirection = (worldAimTarget - transform.position).normalized; // Determine where the user is aiming relative to the player
                targetDirection = aimDirection; // Have player look towards where they're aiming
            }
            else
            {
                targetDirection = cameraTransform.forward * inputManager.verticalInput; // Face player in direction of vertical movement
                targetDirection = targetDirection + cameraTransform.right * inputManager.horizontalInput; // Face player in direction of horizontal movement
            }

            targetDirection.Normalize(); // Set magnitude to 1
            targetDirection.y = 0; // Set value on y axis to 0

            if (targetDirection == Vector3.zero) targetDirection = transform.forward; // Keep rotation at position last specified by the player

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection); // Look towards the target direction defined above
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, playerMovement.rotationSpeed * Time.deltaTime); // Get quaternion of the player's roation

            transform.rotation = playerRotation; // Rotate the transform of the player
            #endregion
        }
    }

    // Handles the special combat ability of the GuildBossSon
    protected override void HandleCombatAbility() { }
}
