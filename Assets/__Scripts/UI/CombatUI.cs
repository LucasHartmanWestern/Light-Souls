using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    InputManager inputManager; // Reference to InputManager
    [SerializeField] private Image _crosshair;
    [SerializeField] private GameObject _lockOnIndicator;

    // Awake is called before Start()
    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>(); // Reference to instance of InputManager
    }

    // Update is called once per frame
    void Update()
    {
        _crosshair.enabled = inputManager.aimInput; // Only enable the crosshair if the player is aiming
        _lockOnIndicator.SetActive(inputManager.lockOnFlag); // Only enable the lock on input if player is locked on
    }
}
