using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetChar : MonoBehaviour
{

    public GameObject Gun,Sword;



    void Start()
    {
        int getCharacter = Selector_Script.refInt;

        if(getCharacter == 1)
        {
            Sword.SetActive(false);
            Gun.SetActive(true);
        }
        if(getCharacter == 2)
        {
            Debug.Log("No Character Yet");
            Destroy(Sword);
            Destroy(Gun);
        }
        if(getCharacter == 3)
        {
            Gun.SetActive(false);
            Sword.SetActive(true);
        }
        
        

    }

}

