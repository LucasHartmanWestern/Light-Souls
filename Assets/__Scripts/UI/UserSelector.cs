using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UserSelector : MonoBehaviour
{
    public string theName;
    public string serveraddy;
    public GameObject inputField;
    public GameObject sinputField;
    public GameObject textDisplay;
    public GameObject sTextDisplay;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StoreName()
    {
        theName = inputField.GetComponent<Text>().text;
        textDisplay.GetComponent<Text>().text = "Username: " + theName;
        serveraddy = sinputField.GetComponent<Text>().text;
        sTextDisplay.GetComponent<Text>().text = "Server: " + serveraddy;

    }
    public void startGame()
    {
        SceneManager.LoadScene("multiplayerselect");
    }
    public void startsingle()
    {
        SceneManager.LoadScene("Player Select");
    }
}
