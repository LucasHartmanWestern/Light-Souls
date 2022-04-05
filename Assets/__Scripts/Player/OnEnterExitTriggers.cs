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

            }

            if (SceneManager.GetActiveScene().name == "Level 2")
            {
                SceneManager.LoadScene("Level 1");

            }
        }

        if (collision.transform.tag == "CaveTravelTrigger")//when player touches the cave enterance, they are either sent to level 2 or volcano areana depending on their current scene
        {
            if (SceneManager.GetActiveScene().name == "Level 2")
            {
                SceneManager.LoadScene("Volcano");

            }

            if (SceneManager.GetActiveScene().name == "Volcano") 
            {
                SceneManager.LoadScene("Level 2");
                
            }
                
        }

        if(collision.transform.tag == "DeathTrigger")//if the player clips and falls out of bounds: load the current scene again
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
