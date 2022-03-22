using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreenButtons : MonoBehaviour
{
    public void gameStart()
    {
        SceneManager.LoadScene("Level 1");

    }
   public void gameExit()
   {
        Application.Quit();
   }
}
