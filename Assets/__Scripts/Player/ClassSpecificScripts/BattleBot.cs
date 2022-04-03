using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBot : PlayerGeneral
{
    public float dashForce = 1f; // Force the player dashes with
    public ParticleSystem rocketTrail; // Rocket trail particle system

    // Handles the special movement the player can perform
    protected override void HandleMovementAbility()
    {
        #region Rocket Dash Ability
        if (inputManager.specialMoveInput && playerMovement.isGrounded && playerSpecial >= 10) // Check if player is using the special move input
        {
            playerSpecial -= 10; // Decrease special ability meter by 10

            Vector3 dashDirection = Vector3.zero; // Track direction player will dash

            if (inputManager.horizontalInput == 0 && inputManager.verticalInput == 0) dashDirection = transform.forward; // If player isn't try to move then dash forwards
            else // If player is trying to move then dash in that direction
            {
                dashDirection = cameraTransform.right * inputManager.horizontalInput;
                dashDirection += cameraTransform.forward * inputManager.verticalInput;
                dashDirection.y = 0.1f;
            }

            inputManager.specialMoveInput = false; // Reset back to false

            // Shrink collider
            this.GetComponent<CapsuleCollider>().height = 0.1f;
            this.GetComponent<CapsuleCollider>().radius = 0.1f;

            playerAnimationManager.animator.SetBool("isJumping", true); // Set the bool in the animator
            playerAnimationManager.PlayTargetAnimation("Jumping", false); // Play animation in the animator

            rigidBody.velocity += dashDirection * dashForce; // Apply the newly calcualted velocity to the RigidBody of the player
            

            StartCoroutine("FinishDash"); // Start the coroutine to reset the variables after the animation finishes
        }

        // Spawn in rocket trail ps while player is dashing
        if (this.GetComponent<CapsuleCollider>().height == 0.1f)
            Instantiate(rocketTrail, transform.Find("PlayerTarget").position, Quaternion.Inverse(transform.localRotation), transform);
        #endregion
    }

    // Handles the special combat ability of the BattleBot
    protected override void HandleCombatAbility() 
    {
        #region Scope Ability
        // Check if player is aiming and using their special ability input
        if (inputManager.aimInput && inputManager.specialAbilityInput)
        {
            if (Camera.main.fieldOfView == 60) // Check if they are not in scope vision
            {
                Camera.main.fieldOfView = 20; // Zoom in the FOV
                combatUI.sniperCrosshair.SetActive(true); // Toggle sniper crosshair
            }
            else // The player is already in scope vision
            {
                Camera.main.fieldOfView = 60; // Zoom out the FOV
                combatUI.sniperCrosshair.SetActive(false); // Toggle sniper crosshair
            }
                

            inputManager.specialAbilityInput = false; // Set to false

        }
        // If player isn't aiming then set everything back to normal
        if (!inputManager.aimInput)
        {
            Camera.main.fieldOfView = 60;
            combatUI.sniperCrosshair.SetActive(false); // Make sure sniper crosshair is disabled
        }
        #endregion
    }

    IEnumerator FinishDash()
    {
        yield return new WaitForSeconds(0.5f); // Wait duration of animation
        // Set collider back to normal
        this.GetComponent<CapsuleCollider>().height = 1.6f;
        this.GetComponent<CapsuleCollider>().radius = 0.28f;
        yield return null;
    }
}
