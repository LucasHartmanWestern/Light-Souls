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
    protected override void HandleCombatAbility() { }
}
