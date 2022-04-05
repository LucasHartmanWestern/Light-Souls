using UnityEngine;
using UnityEngine.SceneManagement;

public class OnEnterExitTriggers : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)//called every new collision
    {
        if (collision.transform.tag == "DoorTravelTrigger")//when player touches the tavern door, they are either sent to level 1 or tavern depending on their current scene
        {
            if (SceneManager.GetActiveScene().name == "Level 1")
                SceneManager.LoadScene("Tavern");

            if (SceneManager.GetActiveScene().name == "Tavern")
                SceneManager.LoadScene("Level 1");
        }

        if (collision.transform.tag == "CartTravelTrigger")//when player touches the hovering cart, they are either sent to level 1 or level 2 depending on their current scene
        {
            if(SceneManager.GetActiveScene().name == "Level 1")
                SceneManager.LoadScene("Level 2");

            if (SceneManager.GetActiveScene().name == "Level 2")
                SceneManager.LoadScene("Level 1");
        }

        if (collision.transform.tag == "CaveTravelTrigger")//when player touches the cave enterance, they are either sent to level 2 or volcano areana depending on their current scene
        {
            if (SceneManager.GetActiveScene().name == "Level 2")
                SceneManager.LoadScene("Volcano");

            if (SceneManager.GetActiveScene().name == "Volcano") 
            {
                SceneManager.LoadScene("Level 2");
                transform.position = new Vector3(51.11f, 20.32f, -142.27f);
            }
                
        }

        if(collision.transform.tag == "DeathTrigger")//if the player clips and falls out of bounds: load the current scene again
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
