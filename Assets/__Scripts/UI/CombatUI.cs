using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    InputManager inputManager; // Reference to InputManager
    [SerializeField] private GameObject _lockOnIndicator; // Reference to the lock on indicator
    [SerializeField] private Image _crosshair; // Reference to crosshair image
    public GameObject sniperCrosshair; // Reference to the sniper crosshair player 3 uses

    // Start is called after Start()
    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>(); // Reference to instance of InputManager
    }

    // Update is called once per frame
    void Update()
    {
        if (sniperCrosshair.activeSelf == false) // Check if sniper crosshair is enabled
            _crosshair.enabled = inputManager.aimInput; // Only enable the crosshair if the player is aiming
        else
            _crosshair.enabled = false; // Disable crosshair if sniper crosshair is displayed

        _lockOnIndicator.SetActive(inputManager.lockOnFlag); // Only enable the lock on input if player is locked on
    }
}
