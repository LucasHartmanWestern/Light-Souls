using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBegin : MonoBehaviour
{
   public void startGame()
   {
      SceneManager.LoadScene("SelectPlayer");
   }
   public void exitGame()
   {
      Application.Quit();
   }
}
