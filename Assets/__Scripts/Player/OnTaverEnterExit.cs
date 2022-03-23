using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTaverEnterExit : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "DoorExit")
            SceneManager.LoadScene("Level 1");

       if (collision.transform.tag == "DoorEnter")
            SceneManager.LoadScene("Tavern");
    }
}
