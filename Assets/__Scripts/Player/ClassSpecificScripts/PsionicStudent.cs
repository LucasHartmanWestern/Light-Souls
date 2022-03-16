using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsionicStudent : PlayerGeneral
{
    // Handles the special movement the player can perform
    protected override void HandleMovementAbility()
    {
        if (inputManager.specialMoveInput) // Check if player is using the special move input
        {
            Vector3 dashDirection = Vector3.zero; // Track direction player will dash

            if (inputManager.horizontalInput == 0 && inputManager.verticalInput == 0) dashDirection = transform.forward; // If player isn't try to move then dash forwards
            else // If player is trying to move then dash in that direction
            {
                dashDirection = cameraTransform.right * inputManager.horizontalInput;
                dashDirection += cameraTransform.forward * inputManager.verticalInput;
            }

            inputManager.specialMoveInput = false; // Reset back to 

            playerAnimationManager.AplpyRootMotion(true); // Apply the root motion of the animation
            playerAnimationManager.PlayTargetAnimation("Dash Forwards", true); // Play the dash animation
            StartCoroutine("finishDash"); // Start the coroutine to reset the variables after the animation finishes
        }
    }

    IEnumerator finishDash()
    {
        yield return new WaitForSeconds(0.29f); // Wait duration of animation
        playerAnimationManager.AplpyRootMotion(false); // Set Root Motion back to false
        yield return null;
    }
}
