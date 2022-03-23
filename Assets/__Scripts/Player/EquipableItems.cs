using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableItems : MonoBehaviour
{
    PlayerGeneral playerGeneral; // Reference to PlayerGeneral script

    public GameObject fireParticleSystem; // Reference to the fire effect Particle System

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
        playerGeneral = FindObjectOfType<PlayerGeneral>(); // Get reference to instance of playerGeneral object
        ApplyEffects(); // Apply the effects
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
        if (!bigMagazine && bigMagazinePrev) { playerGeneral.playerMaganizeCapacity -= 10; playerGeneral.playerAmmo -= 10; }// Decrease magazine capacity by 10
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

    void HandleFireEffect()
    {
        // Activate the fire particle system if the equipped item is selected
        if (gasoline)
            fireParticleSystem.SetActive(true);
        else
            fireParticleSystem.SetActive(false);
    }

    // Returns either 1 or 2 depending on crit chance
    public float GetFourLeafDamage()
    {
        if (fourLeafClover)
            return (Random.Range(1, 5) == 1) ? 2 : 1;
        else return 1;
    }
}
