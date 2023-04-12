using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UserSelector : MonoBehaviour
{
    public string theName;
    public string IpAddress;
    public GameObject inputField;
    public GameObject ipInputField;
    public GameObject textDisplay;
    public GameObject iptextDisplay;

    public void StoreName()
    {
        theName = inputField.GetComponent<Text>().text;
        textDisplay.GetComponent<Text>().text = "Username: " + theName;
        IpAddress = ipInputField.GetComponent<Text>().text;
        iptextDisplay.GetComponent<Text>().text = "IP: " + IpAddress;

    }
    public void startGame()
    {
        SceneManager.LoadScene("Player Select");
    }

    public void SelectLevel()
    {
        SceneManager.LoadScene("Level Select");
    }
}
