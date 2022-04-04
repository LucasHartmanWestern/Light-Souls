using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBegin : MonoBehaviour
{
   public void startGame()
   {
        SceneManager.LoadScene("Player Select");
   }
   public void exitGame()
   {
      Application.Quit();
   }
}
