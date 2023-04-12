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

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Server Response:" + mainPlayer.serverRes);
       
        // here we need to parse the string to and seperate by the players
        string stringData = mainPlayer.serverRes;
        //seperate players by |
        string[] dataByPlayer = stringData.Split('|');
        Debug.Log("Player Count: " + dataByPlayer.Length);
        // add line for seperating at certain length for each player? 

        getData(dataByPlayer);
    }

    void getData(string[] dataByPlayer)
    {
        for (int i = 0; i < dataByPlayer.Length; i++)
        {
            string[] data = dataByPlayer[i].Split(',');
            string playerName = data[0];
            string ip = data[1];
            string port = data[2];

            // Position Info
            Vector3 position = new Vector3(float.Parse(data[3]), float.Parse(data[4]), float.Parse(data[5]));
            Quaternion rotation = Quaternion.Euler(int.Parse(data[6]), int.Parse(data[7]), int.Parse(data[8]));

            // Items
            bool bigMag = bool.Parse(data[9]);
            bool gas = bool.Parse(data[10]);
            bool rocketBoots = bool.Parse(data[11]);
            bool hiCalBullets = bool.Parse(data[12]);
            bool energyDrink = bool.Parse(data[13]);
            bool specialSerum = bool.Parse(data[14]);
            bool bodyArmor = bool.Parse(data[15]);
            bool aimChip = bool.Parse(data[16]);
            bool loCalBullet = bool.Parse(data[17]);
            bool fourLeaf = bool.Parse(data[18]);

            // Player Stats
            string cType = data[19];
            float startHealth = float.Parse(data[20]);
            float currentHealth = float.Parse(data[21]);
            int level = int.Parse(data[22]);
            float rangedDamage = float.Parse(data[23]);
            float meleeDamage = float.Parse(data[24]);
            float resistance = float.Parse(data[25]);
            int magCapacity = int.Parse(data[26]);
            int ammo = int.Parse(data[27]);
            float fireRate = float.Parse(data[28]);
            float dashForce = float.Parse(data[29]);
            float jumpStrength = float.Parse(data[30]);
            float moveSpeed = float.Parse(data[31]);
            float lookSpeed = float.Parse(data[32]);

            // Player Actions
            bool isAttacking = bool.Parse(data[33]);
            bool isJumping = bool.Parse(data[34]);
            bool isReloading = bool.Parse(data[35]);

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

                    playerToUpdate = newPlayer;
                }
                else if (cType == "JetPackPlayer")
                {
                    GameObject newPlayer = Instantiate(JetPackPlayer, MultiplayerObject.transform);
                    newPlayer.name = playerName;

                    players.Add(newPlayer);

                    playerToUpdate = newPlayer;
                }
                else if (cType == "SwordPlayer")
                {                
                    GameObject newPlayer = Instantiate(SwordPlayer, MultiplayerObject.transform);
                    newPlayer.name = playerName;

                    players.Add(newPlayer);

                    playerToUpdate = newPlayer;
                }


            }

            //logic for updating player info

            //Position
            playerToUpdate.transform.position = position;
            playerToUpdate.transform.rotation = rotation;

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
            playerToUpdate.GetComponent<PlayerGeneral>().isReloading = isReloading;
            //isAttacking = FindObjectOfType<PlayerMovement>().gameObject.GetComponent<PlayerMovement>().isJumping;
        }
       }












}
