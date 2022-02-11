using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    InputManager inputManager; // Reference to InputManager class
    [SerializeField] private LayerMask _aimColliderMask; // All layers that player can aim at

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
            //transform.position = raycastHit.point;
        }
    }

    // Handles the melee combat
    void HandleMeleeCombat()
    {

    }
}
