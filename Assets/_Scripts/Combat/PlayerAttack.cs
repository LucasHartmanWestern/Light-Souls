using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    InputManager inputManager; // Reference to InputManager class
    [SerializeField] private LayerMask _aimColliderMask; // All layers that player can aim at

    public GameObject projectilePrefab; // Get reference to the projectile prefab
    public Transform spawnBulletPosition; // Point where bullet spawns when fired

    // Called before Start()
    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>(); // Get reference to instance of InputManager
    }

    // Update is called once per frame
    void Update()
    {
        // Handle the combat depending on whether or not character is aiming
        if (inputManager.aimInput) HandleRangedCombat();
        else HandleMeleeCombat(); 
    }

    // Handle the Ranged Combat
    void HandleRangedCombat()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f); // Get position of the center of the screen
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint); // Cast ray from center of the screen forwards
        
        // Triggers if Raycast hits something
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, _aimColliderMask))
        {
            // Player is attacking
            if (inputManager.attackInput)
            { 
                Vector3 aimDirection = (raycastHit.point - spawnBulletPosition.position).normalized; // Determine where the user is aiming relative to the player
                Instantiate(projectilePrefab, spawnBulletPosition.position, Quaternion.LookRotation(aimDirection, Vector3.up)); // Spawn in a bullet
                inputManager.attackInput = false; // Make it so player must press the attack button each time (semi-automatic)
            }  
        }
    }

    // Handles the melee combat
    void HandleMeleeCombat()
    {

    }
}
