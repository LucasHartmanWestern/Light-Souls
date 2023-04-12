using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToChar : MonoBehaviour
{
    public void goBack()
    {
        SceneManager.LoadScene("Player Select");
    }
    public void goBackMulti()
    {
        SceneManager.LoadScene("multiplayerselect");
    }

}
