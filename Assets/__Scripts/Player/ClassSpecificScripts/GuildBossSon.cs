using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildBossSon : PlayerGeneral
{
    public ParticleSystem jetPackPS; // Get particle system of the jetpack
    public float dashForce = 1f; // Force the player dashes with

    // Handles the special movement the player can perform
    protected override void HandleMovementAbility()
    {
        #region Hover Ability
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
        #endregion

        #region Jump Boost Ability
        if (inputManager.specialMoveInput && playerMovement.isGrounded && playerSpecial >= 30) // Check if player is using the special move input
        {
            playerSpecial -= 30; // Decrease special ability meter by 30

            Vector3 dashDirection = Vector3.zero; // Track direction player will dash

            if (inputManager.horizontalInput == 0 && inputManager.verticalInput == 0) dashDirection = Vector3.up; // If player isn't try to move then dash forwards
            else // If player is trying to move then dash in that direction
            {
                dashDirection = cameraTransform.right * inputManager.horizontalInput;
                dashDirection += cameraTransform.forward * inputManager.verticalInput;
                dashDirection.y = 1f;

                dashDirection.x = Mathf.Clamp(dashDirection.x, 0, 0.1f);
                dashDirection.z = Mathf.Clamp(dashDirection.z, 0, 0.1f);
            }

            inputManager.specialMoveInput = false; // Reset back to false

            // Shrink collider
            this.GetComponent<CapsuleCollider>().height = 0.1f;
            this.GetComponent<CapsuleCollider>().radius = 0.1f;

            playerAnimationManager.animator.SetBool("isJumping", true); // Set the bool in the animator
            playerAnimationManager.PlayTargetAnimation("Jumping", false); // Play animation in the animator

            rigidBody.velocity += dashDirection * dashForce; // Apply the newly calcualted velocity to the RigidBody of the player

            StartCoroutine("FinishBoost"); // Start the coroutine to reset the variables after the animation finishes
        }

        // Spawn in rocket trail ps while player is dashing
        if (this.GetComponent<CapsuleCollider>().height == 0.1f)
            Instantiate(jetPackPS, transform.Find("PlayerTarget").position, Quaternion.Euler(new Vector3(90, 0, 0)), transform);
        #endregion
    }

    // Handles the special combat ability of the GuildBossSon
    protected override void HandleCombatAbility()
    {
        if (inputManager.specialAbilityInput && playerSpecial >= 50) // Check if player is using the special ability input
        {
            playerSpecial -= 50; // Decrease special ability meter by 10

            inputManager.specialAbilityInput = false; // Reset back to false

            playerAmmo = playerMaganizeCapacity; // Reload without playing animation
        }
    }

    IEnumerator FinishBoost()
    {
        yield return new WaitForSeconds(0.5f); // Wait duration of animation
        // Set collider back to normal
        this.GetComponent<CapsuleCollider>().height = 1.6f;
        this.GetComponent<CapsuleCollider>().radius = 0.28f;
        yield return null;
    }
}
