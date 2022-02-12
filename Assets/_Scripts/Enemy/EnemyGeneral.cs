using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneral : MonoBehaviour
{
    Animator animator; // Reference to enemy animator

    public Transform deathPS; // Death particle system reference

    [Header("Enemy Stats")]
    public float enemyHealth; // Track health of enemy

    // Called before Start()
    private void Awake()
    {
        animator = GetComponent<Animator>(); // Get animator attached to this enemy
    }

    // Damage the enemy
    public void TakeDamage(float damageAmount)
    {
        enemyHealth -= damageAmount; // Decrease health

        if (enemyHealth <= 0)
        {
            animator.CrossFade("Death", 0.2f); // Play death animation
            StartCoroutine(DestroyEnemyObject()); // Destroy enemy game obejct
        }
    }

    // Handle the destroying of the enemy GameObject
    IEnumerator DestroyEnemyObject()
    {
        yield return new WaitForSeconds(5f); // Wait 5 seconds before destroying the object
        Instantiate(deathPS, transform.position, Quaternion.identity); // Create a death particle system

        Destroy(gameObject); // Destroy the game object
    }
}
