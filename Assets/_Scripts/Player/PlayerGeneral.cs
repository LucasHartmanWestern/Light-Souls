using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGeneral : MonoBehaviour
{
    [Header("Player Stats")]
    public float playerMoveSpeed = 1f; // Track how fast player moves
    public float playerLookSpeed = 1f;
    public float playerHealth; // Track player's health
    public float rangedDamage; // How much damage player's ranged attack does
    public float meleeDamage; // How much damage player's melee attack does

    // Called to damage the player
    public void TakeDamage(float damageAmount)
    {
        playerHealth -= damageAmount; // Decrease player's health
    }
}
