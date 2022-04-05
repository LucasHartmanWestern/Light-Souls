using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public GameObject healthBar; // Reference to the enemy health bar script
    private Camera _mainCamera; // Reference to the main camera
    private EnemyGeneral _enemyGeneral; // Reference to the EnemyGeneral script
    [HideInInspector] public float startingHealth;
    [HideInInspector] public float currentHealth;
    public bool worldSpace; // Check if bar is in worldspace or not

    // Awake is called before Start
    void Awake()
    {
        _mainCamera = Camera.main; // Get the main camera instance
        _enemyGeneral = transform.GetComponent<EnemyGeneral>(); // Get the EnemyGeneral script
        startingHealth = _enemyGeneral.enemyHealth; // Get starting health of enemy
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = _enemyGeneral.enemyHealth; // Get current health of enemy
        if (currentHealth < startingHealth)
            healthBar.SetActive(true);
        if (currentHealth < 0)
            healthBar.SetActive(false);

        healthBar.GetComponent<Slider>().value = Mathf.Clamp(currentHealth / startingHealth, 0, 1); // Get the value of the health relative to the max health
        
        if (GetComponent<GenericEnemyAI>() != null && worldSpace)
            healthBar.transform.forward = -_mainCamera.transform.forward; // Make it so health bar always faces the camera

        if (_enemyGeneral.isHostile && transform.name == "BigEnemy") healthBar.SetActive(true);
    }
}
