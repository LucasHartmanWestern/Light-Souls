using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBegin : MonoBehaviour
{
   public void startGame()
   {
        SceneManager.LoadScene("Select Mode");
   }
   public void exitGame()
   {
      Application.Quit();
   }
}
