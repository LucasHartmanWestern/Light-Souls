using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetChar : MonoBehaviour
{
    public GameObject Gun,Sword,Snipe;
    public static GameObject Instance = null;


    void Awake()
    {
        #region Only have 1 instance of this object at a time
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
        #endregion

        int getCharacter = Selector_Script.refInt; // Get the refInt

        #region Spawn in correct character based on what player selected
        if (getCharacter == 1) // Gunman
        {
            Gun.SetActive(true);
            Sword.SetActive(false);
            Snipe.SetActive(false);
        }
        if (getCharacter == 2) // Battlebot
        {
            Snipe.SetActive(true);
            Gun.SetActive(false);
            Sword.SetActive(false);
        }
        if (getCharacter == 3) // Psionic student
        {
            Gun.SetActive(false);
            Sword.SetActive(true);
            Snipe.SetActive(false);
        }
        #endregion
    }
}

