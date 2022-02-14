using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGeneral : MonoBehaviour
{
    Animator animator; // Reference to enemy animator

    public Transform deathPS; // Death particle system reference

    [Header("Enemy Stats")]
    public bool isAlive; // Track if this enemy instance is alive
    public float enemyHealth; // Track health of enemy
    public float enemyDamage; // Track damage enemy does to player

    // Called before Start()
    private void Awake()
    {
        animator = GetComponent<Animator>(); // Get animator attached to this enemy
    }

    // Called once a frame
    private void Update()
    {
        if (enemyHealth <= 0)
            isAlive = false; // Set that the enemy is dead
    }

    // Damage the enemy
    public void TakeDamage(float damageAmount)
    {
        enemyHealth -= damageAmount; // Decrease health

        if (enemyHealth <= 0)
        {
            animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f)); // Smoothly transition animation to aiming or not aiming
            animator.CrossFade("Death", 0.2f); // Play death animation

            StartCoroutine(DestroyEnemyObject()); // Destroy enemy game obejct
        }
    }

    // Handle the destroying of the enemy GameObject
    IEnumerator DestroyEnemyObject()
    {
        GetComponent<NavMeshAgent>().enabled = false; // Disable the NavMeshAgent

        yield return new WaitForSeconds(5f); // Wait 5 seconds before destroying the object
        Instantiate(deathPS, transform.position, Quaternion.identity); // Create a death particle system

        Destroy(gameObject); // Destroy the game object
    }
}
