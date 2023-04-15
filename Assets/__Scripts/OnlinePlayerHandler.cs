using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnlinePlayerHandler : MonoBehaviour
{
    public MutiplayerManager mainPlayer;

    // Prefabs for instatiating the players
    public GameObject DroidPlayer;
    public GameObject JetPackPlayer;
    public GameObject SwordPlayer;
    public GameObject MultiplayerObject;
    
    public List<GameObject> players = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // This finds main player for purpose of getting backend info
        mainPlayer = FindObjectOfType<MutiplayerManager>();
        players.Add(mainPlayer.gameObject);
    }

    // Update is called once per frame
    void Update()
    {


        // Loop through each child GameObject using a foreach loop
        for(int i = 0; i < players.Count; i++)
        {
            
            if (players[i] == null)
            {
                players.RemoveAt(i);
            }
        }
       
        // Here we need to parse the string to and seperate by the players
        string stringData = mainPlayer.serverRes;
        // Seperate players by |
        string[] dataByPlayer = stringData.Split('|');

        List<string> userNames = new List<string>();

        // Gets list of usernames in server response
        for (int i = 0; i < dataByPlayer.Length; i++)
        {
            userNames.Add(dataByPlayer[i].Split(',')[0]);
        }

        // Checks if multiplayer user is included in the response, deletes if not
        for (int i = 0; i < MultiplayerObject.transform.childCount; i++)
        {
            string playerName = MultiplayerObject.transform.GetChild(i).name;
            bool userFound = userNames.Contains(playerName);
            if (!userFound)
            {
                Destroy(MultiplayerObject.transform.GetChild(i).gameObject.GetComponent<InputManager>());
                Destroy(MultiplayerObject.transform.GetChild(i).gameObject);
            }
        }

        getData(dataByPlayer);
    }

    void getData(string[] dataByPlayer)
    {
        if (dataByPlayer.Length == 1 && dataByPlayer[0].Length == 0) return;
        //Debug.Log(dataByPlayer.Length);
        for (int i = 0; i < dataByPlayer.Length; i++)
        {
            int parseCounter = 0;

            string[] data = dataByPlayer[i].Split(',');

            string playerName = data[parseCounter++];
            string serverName = data[parseCounter++];
            string sceneName = data[parseCounter++];
            string ip = data[parseCounter++];
            string port = data[parseCounter++];

            // Position Info
            Vector3 position = new Vector3(float.Parse(data[parseCounter++]), float.Parse(data[parseCounter++]), float.Parse(data[parseCounter++]));
            Quaternion rotation = Quaternion.Euler(float.Parse(data[parseCounter++]), float.Parse(data[parseCounter++]), float.Parse(data[parseCounter++]));
            
            // Player Stats
            string cType = data[parseCounter++];
            float startHealth = float.Parse(data[parseCounter++]);
            float currentHealth = float.Parse(data[parseCounter++]);

            // Input manager
            float moveAmount = float.Parse(data[parseCounter++]);

            bool sprintInput = bool.Parse(data[parseCounter++]);
            bool jumpInput = bool.Parse(data[parseCounter++]);
            bool aimInput = bool.Parse(data[parseCounter++]);

            GameObject playerToUpdate = null;

            for (int j = 0; j < players.Count; j++)
            {
                // If player exists already
                if (players[j].name == playerName)
                {
                    playerToUpdate = players[j];
                    break;
                }
            }

            // If player does not exist already, instantiate new player
            if (playerToUpdate == null)
            {              
                // Spawn in specific character type
                if (cType == "DroidPlayer")
                {
                    Debug.Log("Player Created");
                    Debug.Log(MultiplayerObject == null);

                    GameObject newPlayer = Instantiate(DroidPlayer, MultiplayerObject.transform);
                    newPlayer.name = playerName;

                    players.Add(newPlayer);
                    newPlayer.SetActive(true);

                    newPlayer.transform.Find("Username").Find("Name").gameObject.SetActive(true);

                    Destroy(newPlayer.GetComponent<MutiplayerManager>());

                    playerToUpdate = newPlayer;
                }
                else if (cType == "JetPackPlayer")
                {
                    Debug.Log("Player Created");
                    Debug.Log(MultiplayerObject == null);

                    GameObject newPlayer = Instantiate(JetPackPlayer, MultiplayerObject.transform);
                    newPlayer.name = playerName;

                    players.Add(newPlayer);
                    newPlayer.SetActive(true);

                    newPlayer.transform.Find("Username").Find("Name").gameObject.SetActive(true);

                    Destroy(newPlayer.GetComponent<MutiplayerManager>());

                    playerToUpdate = newPlayer;
                }
                else if (cType == "SwordPlayer")
                {
                    Debug.Log("Player Created");
                    Debug.Log(MultiplayerObject == null);

                    GameObject newPlayer = Instantiate(SwordPlayer, MultiplayerObject.transform);
                    newPlayer.name = playerName;

                    players.Add(newPlayer);
                    newPlayer.SetActive(true);

                    newPlayer.transform.Find("Username").Find("Name").gameObject.SetActive(true);

                    Destroy(newPlayer.GetComponent<MutiplayerManager>());

                    playerToUpdate = newPlayer;
                }

            }

            // Logic for updating player info

            // Position
            playerToUpdate.transform.position = Vector3.Lerp(playerToUpdate.transform.position, position, Time.deltaTime * 5f);
            playerToUpdate.transform.localRotation= Quaternion.Lerp(playerToUpdate.transform.localRotation, rotation, Time.deltaTime * 5f);

            // Stats
            playerToUpdate.GetComponent<PlayerGeneral>().playerStartingHealth = startHealth;
            playerToUpdate.GetComponent<PlayerGeneral>().playerHealth = currentHealth;


            // Actions
            playerToUpdate.GetComponent<InputManager>().sprintInput = sprintInput;
            playerToUpdate.GetComponent<InputManager>().jumpInput = jumpInput;
            playerToUpdate.GetComponent<InputManager>().aimInput = aimInput;

            // Update the player's movement animation
            playerToUpdate.GetComponent<PlayerAnimationManager>().UpdateAnimatorValues(0, moveAmount, sprintInput);
        }
    }
}
