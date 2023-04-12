using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableItems : MonoBehaviour
{
    PlayerGeneralMultiplayer playerGeneral; // Reference to PlayerGeneral script
    CameraManager cameraManager; // Reference to the CameraManager script

    public GameObject fireParticleSystem; // Reference to the fire effect Particle System

    public bool aimbotLockOn; // Flag for lockon with aimbot

    [Header("Equipable Items")]
    public bool bigMagazine; // Increase magazine capacity of equipped gun by 15%
    public bool gasoline; // Increase damage to melee weapon and apply tic damage
    public bool rocketBoots; // Increase jump height of player
    public bool highCalBullets; // Increase damage of ranged weapons
    public bool energyDrink; // Increase movement speed of player
    public bool specialSerum; // Increase players total health
    public bool bodyArmor; // Decrease all damage player takes
    public bool aimbotChip; // Crosshair snaps to enemy if you aim near them (basically just aim assist)
    public bool lowCalBullet; // Increase rate of fire of ranged weapons
    public bool fourLeafClover; // 1/4 change player's attack does double damage

    #region Previous Equip Tracker
    bool bigMagazinePrev;
    bool gasolinePrev;
    bool rocketBootsPrev;
    bool highCalBulletsPrev;
    bool energyDrinkPrev;
    bool specialSerumPrev;
    bool bodyArmorPrev;
    bool aimbotChipPrev;
    bool lowCalBulletPrev;
    bool fourLeafCloverPrev;
    #endregion

    // Called when script is instantiated
    private void Awake()
    {
        playerGeneral = FindObjectOfType<PlayerGeneralMultiplayer>(); // Get reference to instance of playerGeneral object
        cameraManager = FindObjectOfType<CameraManager>(); // Get reference to the CameraManager object
        ApplyEffects(); // Apply the effects
    }

    // Called once a frame
    private void Update()
    {
        if (playerGeneral == null) playerGeneral = FindObjectOfType<PlayerGeneralMultiplayer>(); // Get reference to instance of playerGeneral object
        if (cameraManager == null) cameraManager = FindObjectOfType<CameraManager>(); // Get reference to the CameraManager object

        HandleAimBot(); // Handle the aimbot feature
    }

    // Finds which items are equipped and applies their effect
    public void ApplyEffects()
    {
        HandleStatModifications(); // Handle the effects that just modify player stats
        HandleFireEffect(); // Handle the fire effects
    }

    void HandleStatModifications()
    {
        #region Modify Stats based on what is equpped
        if (rocketBoots && !rocketBootsPrev) playerGeneral.jumpStrenth *= 1.5f; // Increase jump strength by 50%
        if (!rocketBoots && rocketBootsPrev) playerGeneral.jumpStrenth /= 1.5f; // Decrease jump strength by 50%

        if (highCalBullets && !highCalBulletsPrev) playerGeneral.rangedDamage *= 1.5f; // Increase ranged damage by 50%
        if (!highCalBullets && highCalBulletsPrev) playerGeneral.rangedDamage /= 1.5f; // Decrease ranged damage by 50%

        if (energyDrink && !energyDrinkPrev) playerGeneral.playerMoveSpeed *= 1.5f; // Increase movement speed by 50%
        if (!energyDrink && energyDrinkPrev) playerGeneral.playerMoveSpeed /= 1.5f; // Decrease movement speed by 50%

        if (specialSerum && !specialSerumPrev) playerGeneral.playerStartingHealth += 100; // Increase player health by 100hp
        if (!specialSerum && specialSerumPrev) playerGeneral.playerStartingHealth -= 100; // Decrease player health by 100hp

        if (bodyArmor && !bodyArmorPrev) playerGeneral.resistance *= 1.5f; // Increase player resistance by 50%
        if (!bodyArmor && bodyArmorPrev) playerGeneral.resistance /= 1.5f; // Decrease player resistance by 50%

        if (bigMagazine && !bigMagazinePrev) playerGeneral.playerMaganizeCapacity += 10; // Increase magazine capacity by 10
        if (!bigMagazine && bigMagazinePrev) { playerGeneral.playerMaganizeCapacity -= 10; playerGeneral.playerAmmo -= 10; } // Decrease magazine capacity by 10

        if (lowCalBullet && !lowCalBulletPrev) playerGeneral.playerFireRate /= 1.5f; // Increase fire rate by 50%
        if (!lowCalBullet && lowCalBulletPrev) playerGeneral.playerFireRate *= 1.5f; // Decrease fire rate by 50%
        #endregion

        #region Set Prev Values
        bigMagazinePrev = bigMagazine;
        gasolinePrev = gasoline;
        rocketBootsPrev = rocketBoots;
        highCalBulletsPrev = highCalBullets;
        energyDrinkPrev = energyDrink;
        specialSerumPrev = specialSerum;
        bodyArmorPrev = bodyArmor;
        aimbotChipPrev = aimbotChip;
        lowCalBulletPrev = lowCalBullet;
        fourLeafCloverPrev = fourLeafClover;
        #endregion

    }

    // Handles the fire particle system
    void HandleFireEffect()
    {
        // Activate the fire particle system if the equipped item is selected
        if (gasoline)
            fireParticleSystem.SetActive(true);
        else
            fireParticleSystem.SetActive(false);
    }

    // Handles the aim assist feature
    void HandleAimBot()
    {
        if (aimbotChip && FindObjectOfType<InputManager>().aimInput)
        {
            #region Get Targets to Lock On to
            List<EnemyGeneral> availableTargets = new List<EnemyGeneral>(); // List to hold all possible targets the player can lock on to
            Transform nearestLockOnTarget = null;
            float shortestDistance = Mathf.Infinity; // Measure distance between targets and player and pick the shortest one
            Collider[] colliders = Physics.OverlapSphere(cameraManager.targetTransform.position, 50); // Search around the player a distance of 50 units for colliders

            // Loop through everything detected in the sphere
            for (int i = 0; i < colliders.Length; i++)
            {
                EnemyGeneral enemy = colliders[i].GetComponent<EnemyGeneral>(); // Search through colliders for an EnemyGeneral script
                if (enemy != null)
                {
                    Vector3 lockTargetDirection = enemy.transform.position - cameraManager.targetTransform.position; // Get Vector3 to hold the direction of the target compared to the player
                    float distanceFromTarget = Vector3.Distance(cameraManager.targetTransform.position, enemy.transform.position); // Get distance from player to target
                    float viewableAngle = Vector3.Angle(lockTargetDirection, cameraManager.cameraTransform.forward); // Detect angle between the target and the current forward of the camera

                    // Check that the enemy is on screen and within an acceptable distance
                    if (enemy.transform.root != cameraManager.targetTransform.transform.root && viewableAngle > -50 && viewableAngle < 50 && distanceFromTarget <= 60 && enemy.isAlive && enemy.isHostile)
                        availableTargets.Add(enemy); // Add enemy as potential target
                }
            }

            // Check if any enemies are still available to lockon to
            if (availableTargets.Count == 0)
            {
                aimbotLockOn = false;
                cameraManager.ClearLockOnTargets(); // Clear the lockon targets if player is still aiming and there's no enemies left
                cameraManager.cameraTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0)); // Set camera back to normal
                return; // Don't run if no targets available
            }

            aimbotLockOn = true; // Flag the aimbot lockon it being used

            // Loop through available targets for player to lock on to
            for (int j = 0; j < availableTargets.Count; j++)
            {
                // Get distacne from player
                float distanceFromTarget = Vector3.Distance(cameraManager.targetTransform.position, availableTargets[j].transform.position);

                if (distanceFromTarget < shortestDistance)
                {
                    shortestDistance = distanceFromTarget; // Find closest distance
                    nearestLockOnTarget = availableTargets[j].lockOnTransform; // Set enemy as closest target
                }
            }
            #endregion
            cameraManager.cameraTransform.LookAt(nearestLockOnTarget); // Make camera look directly at the target
            cameraManager.currentLockOnTarget = nearestLockOnTarget; // Set the lockon target to the nearest available one
        }
        else
        {
            #region Reset everything
            aimbotLockOn = false; // Set the flag back to false

            if (!FindObjectOfType<InputManager>().lockOnFlag)
                cameraManager.ClearLockOnTargets(); // Clear all targets

            cameraManager.cameraTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0)); // Set camera back to normal
            #endregion
        }
    }

    // Returns either 1 or 2 depending on crit chance
    public float GetFourLeafDamage()
    {
        if (fourLeafClover)
            return (Random.Range(1, 5) == 1) ? 2 : 1;
        else return 1;
    }
}
