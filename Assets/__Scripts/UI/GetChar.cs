using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetChar : MonoBehaviour
{
    public GameObject Gun,Sword,Snipe;
    public static GameObject Instance = null;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = gameObject;
            DontDestroyOnLoad(gameObject); // Don't destroy this object
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        int getCharacter = Selector_Script.refInt;

        if (getCharacter == 0)
        {
            Gun.SetActive(true);
            Sword.SetActive(false);
            Snipe.SetActive(false);
        }
        if (getCharacter == 2)
        {
            Snipe.SetActive(true);
            Gun.SetActive(false);
            Sword.SetActive(false);
        }
        if (getCharacter == 3)
        {
            Gun.SetActive(false);
            Sword.SetActive(true);
            Snipe.SetActive(false);
        }
    }
}

