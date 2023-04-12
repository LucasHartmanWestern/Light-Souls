using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Threading;
using System.Text;
using System.Linq;

public class MutiplayerManager : MonoBehaviour
{
    string ipAddress = "54.196.231.67";
    int port = 2001;

   // string ipAddress = "127.0.0.1";
    //int port = 3000;

    public Transform playerTransform;

    //player Items
    bool bigMag;
    bool gas;
    bool rocketBoots;
    bool hiCalBullets;
    bool energyDrink;
    bool specialSerum;
    bool bodyArmor;
    bool aimChip;
    bool loCalBullet;
    bool fourLeaf;

    //player stats
    float startHealth;
    float currentHealth;
    float level;
    float rangedDamage;
    float meleeDamage;
    float resistance;
    float magCapacity;
    float ammo;
    float fireRate;
    float dashForce;
    float jumpStrength;
    float moveSpeed;
    float lookSpeed;

    //player events
    bool isAttacking;
    bool isJumping;
    bool isReloading;


    public string userName = "";
    public string preFabName = "";
    public string message = "";
    public string serverRes = "";
    byte[] data;
    byte[] lengthPrefix;
    public byte[] finalData;

    Thread socketThread;

    void Start()
    {
        playerTransform = FindObjectOfType<MutiplayerManager>().gameObject.transform;

        //player items
         bigMag = FindObjectOfType<EquipableItems>().gameObject.GetComponent<EquipableItems>().bigMagazine;
         gas = FindObjectOfType<EquipableItems>().gameObject.GetComponent<EquipableItems>().gasoline;
         rocketBoots = FindObjectOfType<EquipableItems>().gameObject.GetComponent<EquipableItems>().rocketBoots;
         hiCalBullets = FindObjectOfType<EquipableItems>().gameObject.GetComponent<EquipableItems>().highCalBullets;
         energyDrink = FindObjectOfType<EquipableItems>().gameObject.GetComponent<EquipableItems>().energyDrink;
         specialSerum = FindObjectOfType<EquipableItems>().gameObject.GetComponent<EquipableItems>().specialSerum;
         bodyArmor = FindObjectOfType<EquipableItems>().gameObject.GetComponent<EquipableItems>().bodyArmor;
         aimChip = FindObjectOfType<EquipableItems>().gameObject.GetComponent<EquipableItems>().aimbotChip;
         loCalBullet = FindObjectOfType<EquipableItems>().gameObject.GetComponent<EquipableItems>().lowCalBullet;
         fourLeaf = FindObjectOfType<EquipableItems>().gameObject.GetComponent<EquipableItems>().fourLeafClover;

        //player stats

        startHealth = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().playerStartingHealth;
        currentHealth = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().playerHealth;
        level = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().playerLevel;
        rangedDamage = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().rangedDamage;
        meleeDamage = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().meleeDamage;
        resistance = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().resistance;
        magCapacity = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().playerMaganizeCapacity;
        ammo = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().playerAmmo;
        fireRate = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().playerFireRate;
        //dashForce = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().dashForce;
        jumpStrength = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().jumpStrenth;
        moveSpeed = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().playerMoveSpeed;
        lookSpeed = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().playerLookSpeed;

        //player events
        isReloading = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().isReloading;
        isJumping = FindObjectOfType<PlayerMovement>().gameObject.GetComponent<PlayerMovement>().isJumping;
        //isAttacking = FindObjectOfType<PlayerMovement>().gameObject.GetComponent<PlayerMovement>().isJumping;

        // Create a new thread for the socket communication
        socketThread = new Thread(SocketThreadFunc);
        socketThread.Start();
    }

    private void Update()
    {

        message = userName + "," + ipAddress + "," + port + "," + playerTransform.position.x +
                "," + playerTransform.position.y + "," + playerTransform.position.z + "," + playerTransform.rotation.x + "," + playerTransform.rotation.y + "," + playerTransform.rotation.z + "," //Position Info
               + bigMag + "," + gas + "," + rocketBoots + "," + hiCalBullets + "," + energyDrink + "," + specialSerum + "," + bodyArmor + "," + aimChip + "," + loCalBullet + "," + fourLeaf + "," //items
               + preFabName + "," + startHealth + "," + currentHealth + "," + level + "," + rangedDamage + "," + meleeDamage + "," + resistance + "," + magCapacity + "," + ammo + "," + fireRate + "," + dashForce + "," + jumpStrength + "," + moveSpeed + "," + lookSpeed + "," //Player stats
               + isAttacking + "," + isJumping + "," + isReloading;
       

        data = System.Text.Encoding.ASCII.GetBytes(message);

        
        lengthPrefix = BitConverter.GetBytes(data.Length); // Add length prefix
        finalData = new byte[lengthPrefix.Length + data.Length];
        lengthPrefix.CopyTo(finalData, 0);
        data.CopyTo(finalData, lengthPrefix.Length);

/*        // Remove first element of array
        byte[] newArray = new byte[finalData.Length - 1];
        Array.Copy(finalData, 1, newArray, 0, newArray.Length);
        finalData = newArray;*/
    }

    void OnDestroy()
    {
        // Stop the socket thread when the script is destroyed
        if (socketThread != null && socketThread.IsAlive)
        {
            socketThread.Abort();
        }
    }

    void SocketThreadFunc()
    {
        // Create the socket and connect to the server
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(ipAddress, port);

        while (true)
        {
            if (finalData?.Length < 1) continue;

            // Send data to the server
            socket.Send(finalData);

            byte[] buffer = new byte[16384];
            int receivedLength = socket.Receive(buffer);

            // Convert the received bytes into a string
            string response = System.Text.Encoding.UTF8.GetString(buffer, 0, receivedLength);

            string[] splitResponse = response.Split('|');
            for(int i = 0; i < splitResponse.Length; i++)
            {
                string serverName = splitResponse[i].Split(',')[0];
                string hostName = message.Split(',')[0];
                
                if (serverName == hostName)
                {
                    Debug.Log("Found");
                    splitResponse[i] = null;
                }
            }
            
            splitResponse = splitResponse.Where(x => x != null).ToArray();
            response = string.Join("|", splitResponse);

            serverRes = response;
            Debug.Log("Server Response:" + serverRes);

            // Wait for a short period before sending more data
            Thread.Sleep(100);

            if (response.Length == 0) break;
        }
    }
}
