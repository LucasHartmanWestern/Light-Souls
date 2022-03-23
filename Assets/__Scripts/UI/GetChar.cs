using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetChar : MonoBehaviour
{
    public GameObject Gun,Sword;

    void Awake()
    {
        int getCharacter = Selector_Script.refInt;

        if (getCharacter == 0)
        {
            Gun.SetActive(true);
            Sword.SetActive(false);
            
        }
        if (getCharacter == 2)
        {
            Debug.Log("hi");
            Gun.SetActive(false);
            Sword.SetActive(false);
        }
        if (getCharacter == 3)
        {
            Gun.SetActive(false);
            Sword.SetActive(true);
            
        }
    }
}

