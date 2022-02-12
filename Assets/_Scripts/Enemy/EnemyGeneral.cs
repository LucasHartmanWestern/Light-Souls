using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneral : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float enemyHealth; // Track health of enemy

    // Damage the enemy
    public void TakeDamage(float damageAmount)
    {
        enemyHealth -= damageAmount; // Decrease health
    }
}
