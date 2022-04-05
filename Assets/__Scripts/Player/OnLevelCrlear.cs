using UnityEngine;
using UnityEngine.SceneManagement;

public class OnLevelCrlear : MonoBehaviour
{
    GameObject enemies;//the gameobject that all enemies spawn under

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.Find("Enemies");//finds the gameobject
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies == null) enemies = GameObject.Find("Enemies");//finds the gameobject

        if (enemies.transform.childCount != 0)//if enemies are still alive
            return;

        if (SceneManager.GetActiveScene().name == "Level 1")//when player has killed all the enemies in Level 1 load ArenaSetup
            SceneManager.LoadScene("ArenaSetup");

        if (SceneManager.GetActiveScene().name == "ArenaSetup")//when player has killed all the enemies in ArenaSetup load Level 1
            SceneManager.LoadScene("Level 1");
    }
}
