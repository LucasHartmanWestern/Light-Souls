using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float currHealth;
    public float maxHealth;

    public Slider healthBar;

    void Start()
    {
        currHealth = maxHealth;
        healthBar.value = currHealth;
        healthBar.maxValue = maxHealth;
    }

    

}
