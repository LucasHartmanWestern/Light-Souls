using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGeneral : MonoBehaviour
{
    PlayerAnimationManager playerAnimationManager; // Reference to enemy animator

    [Header("Player Stats")]
    public bool isAlive; // Track if player is alive
    public float playerMoveSpeed = 1f; // Track how fast player moves
    public float playerLookSpeed = 1f;
    public float playerHealth; // Track player's health
    public float playerExperience; // Track player exp
    public float rangedDamage; // How much damage player's ranged attack does
    public float meleeDamage; // How much damage player's melee attack does

    // Called before Start()
    private void Awake()
    {
        playerAnimationManager = GetComponent<PlayerAnimationManager>(); // Get PlayerAnimationManager attached to player
    }

    // Called to damage the player
    public void TakeDamage(float damageAmount)
    {
        playerHealth -= damageAmount; // Decrease player's health

        if (playerHealth <= 0)
        {
            isAlive = false; // Show player as no longer alive
            GetComponent<CapsuleCollider>().enabled = false; // Remove capsule collider to avoid player floating
            playerAnimationManager.AplpyRootMotion(); // Apply root motion for death animation only
            playerAnimationManager.PlayTargetAnimation("Death", true); // Play death animation
            StartCoroutine(DestroyPlayer()); // Destroy enemy game obejct
        }
    }

    // Handle the destroying of the enemy GameObject
    IEnumerator DestroyPlayer()
    {
        yield return new WaitForSeconds(5f); // Wait 5 seconds before destroying the object
        Debug.Log("You Died"); // Destroy the game object

    }
}
