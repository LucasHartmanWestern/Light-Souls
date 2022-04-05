using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechBossEnemyAI : MonoBehaviour
{
    private int _cannonNum = 1; // Cannon number that will be fired

    [Header("Game Objects/Transforms")]
    public Transform bodyTransform; // Get transform of the body of the mech
    public Transform playerTransform; // Get the transform of the player
    public RuntimeAnimatorController animationController;

    [SerializeField] AudioSource shootSoundEffect; // Get the audio source attached to the enemy that contains the sound effect for shooting

    public Transform smallMuzzleFlashPS; // Get reference to the muzzle flash particle system
    public Transform bigMuzzleFlashPS; // Get reference to the big muzzle flash particle system
    public Transform missilePS; // Get reference to the missile particle system
    public GameObject smallProjectilePrefab; // Get reference to the projectile prefab
    public GameObject bigProjectilePrefab; // Get reference to the big projectile prefab
    public GameObject missilePrefab; // Get reference to the missile prefab

    public Transform spawnBulletPosition1; // Point where bullet spawns when fired
    public Transform spawnBulletPosition2; // Point where bullet spawns when fired
    public Transform spawnBulletPosition3; // Point where bullet spawns when fired
    public Transform spawnBulletPosition4; // Point where bullet spawns when fired

    public Transform missileSpawnPosition; // Point where missile spawns when fired

    [Header("Attacking variables")]
    public float timeBetweenAttacks; // How long to wait between attacks
    bool alreadyAttacked = true; // Check if enemy already attacked
    bool alreadyMissileAttacked = true; // Check if enemy already attacked

    // Called before Start
    private void Awake()
    {
        Invoke(nameof(ResetAttack), 2); // Delay start of attacks by 2 seconds
        Invoke(nameof(ResetMissileAttack), 2); // Delay start of attacks by 2 seconds
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Selector_Cust>().mechDead)
        {
            Destroy(gameObject);
            return;
        }
        if (playerTransform == null) playerTransform = FindObjectOfType<PlayerGeneral>().transform.Find("PlayerTarget").transform;
        
        bodyTransform.LookAt(playerTransform);
        bodyTransform.rotation = Quaternion.Euler(bodyTransform.rotation.eulerAngles + new Vector3(0, 0, -90));

        Attacking();
    }

    // Attach the animation controller to the mech to play the death animation
    public void AttachAnimationController()
    {
        GetComponent<Animator>().runtimeAnimatorController = animationController;
    }

    void Attacking()
    {
        if (!alreadyAttacked)
        {
            Transform currentSpawnPos;
            Transform muzzleFlashPS;
            GameObject projectilePrefab;

            #region Get the spot to spawn the bullet and type of bullet to spawn
            switch (_cannonNum)
            {
                case 1:
                    currentSpawnPos = spawnBulletPosition1;
                    projectilePrefab = bigProjectilePrefab;
                    muzzleFlashPS = bigMuzzleFlashPS;
                    break;
                case 2:
                    currentSpawnPos = spawnBulletPosition2;
                    projectilePrefab = bigProjectilePrefab;
                    muzzleFlashPS = bigMuzzleFlashPS;
                    break;
                case 3:
                    currentSpawnPos = spawnBulletPosition3;
                    projectilePrefab = smallProjectilePrefab;
                    muzzleFlashPS = smallMuzzleFlashPS;
                    break;
                case 4:
                    currentSpawnPos = spawnBulletPosition4;
                    projectilePrefab = smallProjectilePrefab;
                    muzzleFlashPS = smallMuzzleFlashPS;
                    break;
                default:
                    currentSpawnPos = spawnBulletPosition1;
                    projectilePrefab = bigProjectilePrefab;
                    muzzleFlashPS = bigMuzzleFlashPS;
                    break;
            }
            _cannonNum++;
            if (_cannonNum == 5)
                _cannonNum = 1;
            #endregion

            currentSpawnPos.LookAt(playerTransform);

            Instantiate(muzzleFlashPS, currentSpawnPos.position, Quaternion.LookRotation(currentSpawnPos.forward, Vector3.up)); // Create a yellow muzzle flash
            shootSoundEffect.Play(); // Play the shooting sound effect
            Instantiate(projectilePrefab, currentSpawnPos.position, Quaternion.LookRotation(currentSpawnPos.forward, Vector3.up)); // Spawn in a bullet        

            alreadyAttacked = true; // Set that they attacked
            Invoke(nameof(ResetAttack), timeBetweenAttacks); // Reset attack after specified cooldown
        }  
        if (!alreadyMissileAttacked)
        {
            alreadyMissileAttacked = true; // Set that they attacked
            Instantiate(missilePS, missileSpawnPosition.position, Quaternion.LookRotation(Vector3.up, Vector3.up)); // Create a yellow muzzle flash
            Instantiate(missilePrefab, missileSpawnPosition.position, Quaternion.LookRotation(Vector3.up, Vector3.up)); // Spawn in a missile
            Invoke(nameof(ResetMissileAttack), timeBetweenAttacks * 20); // Reset missile attack after specified cooldown
        }
    }

    // Reset the attack
    private void ResetAttack()
    {
        alreadyAttacked = false; // Reset the alreadyAttacked bool
    }
    // Reset the missile attack
    private void ResetMissileAttack()
    {
        alreadyMissileAttacked = false; // Reset the alreadyMissileAttacked bool
    }
}
