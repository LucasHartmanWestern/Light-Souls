using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour
{
    public Text scoreCount; // Text that shows the player the remaining count of enemies
    public Text expCount; // Text that shows the player their exp
    public Text levelCount; // Text that shows the player their level
    int enemyCount, startingEnemyCount;//the running and starting count of enemies
    GameObject enemies;//the enemies
    PlayerGeneral playerGeneral; // Reference to the PlayerGeneral script

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.Find("Enemies");//finds enemies
        enemyCount = startingEnemyCount = enemies.transform.childCount;//equates the initial enemies count to enemyCount and startingEnemyCount
        playerGeneral = FindObjectOfType<PlayerGeneral>(); // Get reference to PlayerGenral instance
    }

    // Update is called once per frame
    void Update()
    {
        expCount.text = "Exp: " + playerGeneral.playerExperience + "/" + playerGeneral.expToNextLevel; // Display the exp to the player
        levelCount.text = "Level: " + playerGeneral.playerLevel; // Display the level to the player

        scoreCount.text = "Enemies Remaining " + enemyCount + "/" + startingEnemyCount;//displays the running count of enemies against the starting count of enemies
        enemyCount = enemies.transform.childCount;//updates the enemy Count
    }
}
