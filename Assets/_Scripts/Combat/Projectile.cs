using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 10f; // Speed projectile travels after it's fired

    private Rigidbody _projectileRigidBody; // Reference to RigidBody of the projectile
    [SerializeField] private Transform psHitGreen; // Red particle system reference
    [SerializeField] private Transform psHitRed; // Red particle system reference

    // Called before Start()
    private void Awake()
    {
        _projectileRigidBody = GetComponent<Rigidbody>(); // Get RigidBody attached to game object this script is on

    }

    // Called after Awake()
    private void Start()
    {
        _projectileRigidBody.velocity = transform.forward * projectileSpeed; // Set the velocity based on the position the projectile spawns in
    }

    // Called when object enters the collider of another game object
    private void OnTriggerEnter(Collider other)
    {
        // Check if projectile hit an enemy
        if (other.GetComponent<EnemyGeneral>() != null)
        {
            Instantiate(psHitGreen, transform.position, Quaternion.identity); // Create a green particle system
        }
        else // Tiggers if a non-enemy object was hit
        {
            Instantiate(psHitRed, transform.position, Quaternion.identity); // Create a red particle system
        }

        Destroy(gameObject); // Destroy the projectile when it collides with something
    }
}
