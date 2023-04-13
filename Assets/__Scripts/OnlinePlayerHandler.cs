using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnlinePlayerHandler : MonoBehaviour
{
    public MutiplayerManager mainPlayer;

    //prefabs for instatiating the players
    public GameObject DroidPlayer;
    public GameObject JetPackPlayer;
    public GameObject SwordPlayer;
    public GameObject MultiplayerObject;
    
    public List<GameObject> players = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //this finds main player for purpose of getting backend info
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
        //Debug.Log("Server Response:" + mainPlayer.serverRes);
       
        // here we need to parse the string to and seperate by the players
        string stringData = mainPlayer.serverRes;
        //seperate players by |
        string[] dataByPlayer = stringData.Split('|');
        //Debug.Log("Player Count: " + dataByPlayer.Length);

        List<string> userNames = new List<string>();

        //gets list of usernames in server response
        for (int i = 0; i < dataByPlayer.Length; i++)
        {
            userNames.Add(dataByPlayer[i].Split(',')[0]);
        }

        // checks if multiplayer user is included in the response, deletes if not
        for (int i = 0; i < MultiplayerObject.transform.childCount; i++)
        {
            string playerName = MultiplayerObject.transform.GetChild(i).name;
            bool userFound = userNames.Contains(playerName);
            if (!userFound)
            {
                Destroy(MultiplayerObject.transform.GetChild(i).gameObject);
            }
        }
        // add line for seperating at certain length for each player? 

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
            string ip = data[parseCounter++];
            string port = data[parseCounter++];

            // Position Info
            Vector3 position = new Vector3(float.Parse(data[parseCounter++]), float.Parse(data[parseCounter++]), float.Parse(data[parseCounter++]));
            Quaternion rotation = Quaternion.Euler(float.Parse(data[parseCounter++]), float.Parse(data[parseCounter++]), float.Parse(data[parseCounter++]));


            // Items
            bool bigMag = bool.Parse(data[parseCounter++]);
            bool gas = bool.Parse(data[parseCounter++]);
            bool rocketBoots = bool.Parse(data[parseCounter++]);
            bool hiCalBullets = bool.Parse(data[parseCounter++]);
            bool energyDrink = bool.Parse(data[parseCounter++]);
            bool specialSerum = bool.Parse(data[parseCounter++]);
            bool bodyArmor = bool.Parse(data[parseCounter++]);
            bool aimChip = bool.Parse(data[parseCounter++]);
            bool loCalBullet = bool.Parse(data[parseCounter++]);
            bool fourLeaf = bool.Parse(data[parseCounter++]);
            
            // Player Stats
            string cType = data[parseCounter++];
            float startHealth = float.Parse(data[parseCounter++]);
            float currentHealth = float.Parse(data[parseCounter++]);
            int level = int.Parse(data[parseCounter++]);
            float rangedDamage = float.Parse(data[parseCounter++]);
            float meleeDamage = float.Parse(data[parseCounter++]);
            float resistance = float.Parse(data[parseCounter++]);
            int magCapacity = int.Parse(data[parseCounter++]);
            int ammo = int.Parse(data[parseCounter++]);
            float fireRate = float.Parse(data[parseCounter++]);
            float dashForce = float.Parse(data[parseCounter++]);
            float jumpStrength = float.Parse(data[parseCounter++]);
            float moveSpeed = float.Parse(data[parseCounter++]);
            
            
            // Player Actions
            bool isAttacking = bool.Parse(data[parseCounter++]);
            bool isJumping = bool.Parse(data[parseCounter++]);
            bool isReloading = bool.Parse(data[parseCounter++]);

            //input manager
            float moveAmount = float.Parse(data[parseCounter++]);

            bool sprintInput = bool.Parse(data[parseCounter++]);
            bool jumpInput = bool.Parse(data[parseCounter++]);
            bool aimInput = bool.Parse(data[parseCounter++]);
            bool attackInput = bool.Parse(data[parseCounter++]);
            bool specialMoveInput = bool.Parse(data[parseCounter++]);
            bool specialAbilityInput = bool.Parse(data[parseCounter++]);
            //bool reloadInput = bool.Parse(data[parseCounter++]);

            //  Debug.Log("Player Name: " + playerName);
            //  Debug.Log("IP: " + ip);
            //  Debug.Log("Port: " + port);
            //  Debug.Log("Position: " + position);
            //  Debug.Log("Rotation: " + rotation.eulerAngles);

            GameObject playerToUpdate = null;

            for (int j = 0; j < players.Count; j++)
            {
                //if player exists already
                if (players[j].name == playerName)
                {
                    playerToUpdate = players[j];
                    break;
                }
            }

            //if player does not exist already, instantiate new player
            if (playerToUpdate == null)
            {
                //for now new player is just a droidplayer
                if (cType == "DroidPlayer")
                {
                    GameObject newPlayer = Instantiate(DroidPlayer, MultiplayerObject.transform);
                    newPlayer.name = playerName;

                    players.Add(newPlayer);
                    newPlayer.SetActive(true);
                    Destroy(newPlayer.GetComponent<MutiplayerManager>());

                    playerToUpdate = newPlayer;
                }
                else if (cType == "JetPackPlayer")
                {
                    GameObject newPlayer = Instantiate(JetPackPlayer, MultiplayerObject.transform);
                    newPlayer.name = playerName;

                    players.Add(newPlayer);
                    newPlayer.SetActive(true);
                    Destroy(newPlayer.GetComponent<MutiplayerManager>());

                    playerToUpdate = newPlayer;
                }
                else if (cType == "SwordPlayer")
                {                
                    GameObject newPlayer = Instantiate(SwordPlayer, MultiplayerObject.transform);
                    newPlayer.name = playerName;

                    players.Add(newPlayer);
                    newPlayer.SetActive(true);
                    Destroy(newPlayer.GetComponent<MutiplayerManager>());

                    playerToUpdate = newPlayer;
                }

            }

            //logic for updating player info

            //Position
            playerToUpdate.transform.position = Vector3.Lerp(playerToUpdate.transform.position, position, Time.deltaTime * 5f);
            playerToUpdate.transform.localRotation= Quaternion.Lerp(playerToUpdate.transform.localRotation, rotation, Time.deltaTime * 5f);

            //Items
            playerToUpdate.GetComponent<EquipableItems>().bigMagazine = bigMag;
            playerToUpdate.GetComponent<EquipableItems>().gasoline = gas;
            playerToUpdate.GetComponent<EquipableItems>().rocketBoots = rocketBoots;
            playerToUpdate.GetComponent<EquipableItems>().highCalBullets = hiCalBullets;
            playerToUpdate.GetComponent<EquipableItems>().energyDrink = energyDrink;
            playerToUpdate.GetComponent<EquipableItems>().specialSerum = specialSerum;
            playerToUpdate.GetComponent<EquipableItems>().bodyArmor = bodyArmor;
            playerToUpdate.GetComponent<EquipableItems>().aimbotChip = aimChip;
            playerToUpdate.GetComponent<EquipableItems>().lowCalBullet = loCalBullet;
            playerToUpdate.GetComponent<EquipableItems>().bigMagazine = fourLeaf;

            //Stats
            //cType = playerTransform.gameObject.name;
            playerToUpdate.GetComponent<PlayerGeneral>().playerStartingHealth = startHealth;
            playerToUpdate.GetComponent<PlayerGeneral>().playerHealth = currentHealth;
            playerToUpdate.GetComponent<PlayerGeneral>().playerLevel = level;
            playerToUpdate.GetComponent<PlayerGeneral>().rangedDamage = rangedDamage;
            playerToUpdate.GetComponent<PlayerGeneral>().meleeDamage = meleeDamage;
            playerToUpdate.GetComponent<PlayerGeneral>().resistance = resistance;
            playerToUpdate.GetComponent<PlayerGeneral>().playerMaganizeCapacity = magCapacity;
            playerToUpdate.GetComponent<PlayerGeneral>().playerAmmo = ammo;
            playerToUpdate.GetComponent<PlayerGeneral>().playerFireRate = fireRate;

            //Actions
            playerToUpdate.GetComponent<PlayerGeneral>().isReloading = isReloading;
            playerToUpdate.GetComponent<PlayerMovement>().isJumping = isJumping;
            //isAttacking = FindObjectOfType<PlayerMovement>().gameObject.GetComponent<PlayerMovement>().isJumping;

            playerToUpdate.GetComponent<InputManager>().sprintInput = sprintInput;
            playerToUpdate.GetComponent<InputManager>().jumpInput = jumpInput;
            playerToUpdate.GetComponent<InputManager>().aimInput = aimInput;
            playerToUpdate.GetComponent<InputManager>().attackInput = attackInput;
            playerToUpdate.GetComponent<InputManager>().specialMoveInput = specialMoveInput;

            playerToUpdate.GetComponent<InputManager>().specialAbilityInput = specialAbilityInput;
            //playerToUpdate.GetComponent<InputManager>().reloadInput = reloadInput;

            playerToUpdate.GetComponent<PlayerAnimationManager>().UpdateAnimatorValues(0, moveAmount, sprintInput); // Update the player's movement animation
        }
    }
}
