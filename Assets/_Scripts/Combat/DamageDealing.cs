using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealing : MonoBehaviour
{
    InputManager inputManager; // Reference to Input Manager
    PlayerAnimationManager playerAnimationManager; // Reference to PlayerAnimationManager
    PlayerGeneral playerGeneral; // Reference to PlayerGeneral

    public bool isProjectile; // Check if script is attached to projectile or not
    public float projectileSpeed = 10f; // Speed projectile travels after it's fired

    private Rigidbody _projectileRigidBody; // Reference to RigidBody of the projectile
    [SerializeField] private Transform psHitGreen; // Red particle system reference
    [SerializeField] private Transform psHitRed; // Red particle system reference

    // Called before Start()
    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>(); // Get reference to instance of Input Manager
        playerAnimationManager = FindObjectOfType<PlayerAnimationManager>(); // Get reference to instance of Player Animation Manager
        playerGeneral = FindObjectOfType<PlayerGeneral>(); // Get reference to instance of Player General script
        _projectileRigidBody = GetComponent<Rigidbody>(); // Get RigidBody attached to game object this script is on
    }

    // Called after Awake()
    private void Start()
    {
        if (isProjectile)
            _projectileRigidBody.velocity = transform.forward * projectileSpeed; // Set the velocity based on the position the projectile spawns in
    }

    // Called when object enters the collider of another game object
    private void OnTriggerEnter(Collider other)
    {
        if (!isProjectile && !playerAnimationManager.AnimationPlaying("Sword Swing")) return; // Don't do anything if player is not attacking

        // Check if hit an enemy
        if (other.GetComponent<EnemyGeneral>() != null)
        {
            Instantiate(psHitRed, transform.position, Quaternion.identity); // Create a red particle system

            if (isProjectile) other.GetComponent<EnemyGeneral>().TakeDamage(playerGeneral.rangedDamage); // Damage enemy the ranged amount
            else other.GetComponent<EnemyGeneral>().TakeDamage(playerGeneral.meleeDamage); // Damage enemy the melee amount
        }
        else // Tiggers if a non-enemy object was hit
        {
            if (isProjectile)
                Instantiate(psHitGreen, transform.position, Quaternion.identity); // Create a green particle system
        }

        if (isProjectile)
            Destroy(gameObject); // Destroy the projectile when it collides with something
    }
}
