using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBot : PlayerGeneral
{
    // Handles the special movement the player can perform
    protected override void HandleMovementAbility()
    {
        if (inputManager.specialMoveInput) // Check if player is using the special move input
        {



            inputManager.specialMoveInput = false; // Reset back to false
        }
    }

    // Handles the special combat ability of the BattleBot
    protected override void HandleCombatAbility() 
    {
        // Check if player is aiming and using their special ability input
        if (inputManager.aimInput && inputManager.specialAbilityInput)
        {
            if (Camera.main.fieldOfView == 60) // Check if they are not in scope vision
            {
                Camera.main.fieldOfView = 20; // Zoom in the FOV
                FindObjectOfType<CombatUI>().sniperCrosshair.SetActive(true); // Toggle sniper crosshair
            }
            else // The player is already in scope vision
            {
                Camera.main.fieldOfView = 60; // Zoom out the FOV
                FindObjectOfType<CombatUI>().sniperCrosshair.SetActive(false); // Toggle sniper crosshair
            }
                

            inputManager.specialAbilityInput = false; // Set to false

        }
        // If player isn't aiming then set everything back to normal
        if (!inputManager.aimInput)
        {
            Camera.main.fieldOfView = 60;
            FindObjectOfType<CombatUI>().sniperCrosshair.SetActive(false); // Make sure sniper crosshair is disabled
        }
    }
}
