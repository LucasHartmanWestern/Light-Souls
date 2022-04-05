using UnityEngine;
using UnityEngine.SceneManagement;

public class OnEnterExitTriggers : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)//called every new collision
    {
        if (collision.transform.tag == "DoorTravelTrigger")//when player touches the tavern door, they are either sent to level 1 or tavern depending on their current scene
        {
            if (SceneManager.GetActiveScene().name == "Level 1")
            {
                SceneManager.LoadScene("Tavern");
                FindObjectOfType<PlayerGeneral>().transform.position = new Vector3(31.5349998f, 0.361999989f, -9.38000011f);
            }

            if (SceneManager.GetActiveScene().name == "Tavern")
            {
                SceneManager.LoadScene("Level 1");
                FindObjectOfType<PlayerGeneral>().transform.position = new Vector3(25.2740002f, 5.12099981f, -38.9309998f);
            }
        }

        if (collision.transform.tag == "CartTravelTrigger")//when player touches the hovering cart, they are either sent to level 1 or level 2 depending on their current scene
        {
            if(SceneManager.GetActiveScene().name == "Level 1")
            {
                SceneManager.LoadScene("Level 2");
                FindObjectOfType<PlayerGeneral>().transform.position = new Vector3(40.3600006f, 20.6970005f, 78.5550003f);
            }

            if (SceneManager.GetActiveScene().name == "Level 2")
            {
                SceneManager.LoadScene("Level 1");
                FindObjectOfType<PlayerGeneral>().transform.position = new Vector3(52.875f, -3.43000007f, -123.492996f);
            }
        }

        if (collision.transform.tag == "CaveTravelTrigger")//when player touches the cave enterance, they are either sent to level 2 or volcano areana depending on their current scene
        {
            if (SceneManager.GetActiveScene().name == "Level 2")
            {
                SceneManager.LoadScene("Volcano");
                FindObjectOfType<PlayerGeneral>().transform.position = new Vector3(-17.8700008f, -7.76999998f, -2.32999992f);
            }

            if (SceneManager.GetActiveScene().name == "Volcano") 
            {
                SceneManager.LoadScene("Level 2");
                FindObjectOfType<PlayerGeneral>().transform.position = new Vector3(40.3600006f, 22.3299999f, -115.889999f);
            }
                
        }

        if(collision.transform.tag == "DeathTrigger")//if the player clips and falls out of bounds: load the current scene again
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
