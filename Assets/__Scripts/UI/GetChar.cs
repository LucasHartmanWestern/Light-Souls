using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetChar : MonoBehaviour
{

    public GameObject Gun,Sword;

    void Awake()
    {
        
    }


    void Start()
    {
        int getCharacter = Selector_Script.refInt;

        if(getCharacter == 1)
        {
            Gun.SetActive(false);
            Sword.SetActive(false);
        }
        if(getCharacter == 2)
        {
            Gun.SetActive(false);
            Sword.SetActive(true);
        }
        if(getCharacter == 3)
        {
            Gun.SetActive(true);
            Sword.SetActive(false);
        }
        
        

    }

}

