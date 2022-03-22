using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour
{
    public float curExp;
    public float maxExp;

    public Slider ExpBar;

    void Start()
    {
        curExp = maxExp;
        ExpBar.value = curExp;
        ExpBar.maxValue = maxExp;
    }

    void Update()
    {
        curExp = ExpBar.value;
    }
    
}
