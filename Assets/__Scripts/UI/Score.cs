using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text count;//text that shows the player the remaining count
    int enemyCount, startingEnemyCount;//the running and starting count of enemies
    GameObject enemies;//the enemies

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.Find("Enemies");//finds enemies
        enemyCount = startingEnemyCount = enemies.transform.childCount;//equates the initial enemies count to enemyCount and startingEnemyCount
        count = GetComponent<Text>();//gets the text area
    }

    // Update is called once per frame
    void Update()
    {
        count.text = "Enemies Remaining " + enemyCount + "/" + startingEnemyCount;//displays the running count of enemies against the starting count of enemies
        enemyCount = enemies.transform.childCount;//updates the enemy Count
    }
}
