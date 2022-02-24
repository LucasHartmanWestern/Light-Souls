using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBot : PlayerGeneral
{
    // Handles the special movement the player can perform
    protected override void HandleMovementAbility()
    {
        if (GetComponent<InputManager>().specialMoveInput) // Check if player is using the special move input
        {



            GetComponent<InputManager>().specialMoveInput = false; // Reset back to false
        }
    }
}
