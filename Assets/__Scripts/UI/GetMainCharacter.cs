using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMainCharacter : MonoBehaviour
{

    public GameObject gunner, sniper, swordman;
    
    private readonly string selectedCharacter = "SelectedCharacter";
    
    
    void Awake()
    {

    }


    void Start()
    {
        int getCharater;

        getCharater = PlayerPrefs.GetInt(selectedCharacter);

        switch(getCharater)
        {
            case 1:
            sniper.SetActive(true);
            gunner.SetActive(false);
            swordman.SetActive(false);
                break;
            case 2:
            sniper.SetActive(false);
            gunner.SetActive(false);
            swordman.SetActive(true);
                break;
            case 3:
            sniper.SetActive(false);
            gunner.SetActive(true);
            swordman.SetActive(false);
                break;
            default:
            sniper.SetActive(false);
            gunner.SetActive(true);
            swordman.SetActive(false);
                break;
        }
    }

    
}
