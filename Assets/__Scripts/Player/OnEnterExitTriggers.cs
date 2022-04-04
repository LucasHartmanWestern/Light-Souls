using UnityEngine;
using UnityEngine.SceneManagement;

public class OnEnterExitTriggers : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "DoorTravelTrigger")
        {
            if (SceneManager.GetActiveScene().name == "Level 1")
                SceneManager.LoadScene("Tavern");

            if (SceneManager.GetActiveScene().name == "Tavern")
                SceneManager.LoadScene("Level 1");
        }

        if (collision.transform.tag == "CartTravelTrigger")
        {
            if(SceneManager.GetActiveScene().name == "Level 1")
                SceneManager.LoadScene("Level 2");

            if (SceneManager.GetActiveScene().name == "Level 2")
                SceneManager.LoadScene("Level 1");
        }

        if (collision.transform.tag == "CaveTravelTrigger")
        {
            if (SceneManager.GetActiveScene().name == "Level 2")
                SceneManager.LoadScene("Volcano");

            if (SceneManager.GetActiveScene().name == "Volcano")
                SceneManager.LoadScene("Level 2");
        }

    }
}
