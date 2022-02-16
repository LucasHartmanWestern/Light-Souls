using UnityEngine;
using UnityEngine.SceneManagement;

public class AllEnemiesAreDead : MonoBehaviour
{
    GameObject enemies;//the gameobject that all enemies spawn under
    void Start()
    {
        enemies = GameObject.Find("Enemies");//finds the gameobject
    }

    void Update()
    {
        if (enemies.transform.childCount != 0)//if enemies are still alive
            return;
        
        if (SceneManager.GetActiveScene().name == "Level 1")//when player has killed all the enemies in Level 1
            SceneManager.LoadScene("ArenaSetup");

        if (SceneManager.GetActiveScene().name == "ArenaSetup")//when player has killed all the enemies in ArenaSetup
            SceneManager.LoadScene("Level 1");
    }
}
