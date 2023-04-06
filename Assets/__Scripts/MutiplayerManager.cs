using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Threading;
using System.Text;

public class MutiplayerManager : MonoBehaviour
{
    string ipAddress = "127.0.0.1";
    int port = 3000;

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
    String cType;
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
        cType = playerTransform.gameObject.name;
        startHealth = FindObjectOfType<BattleBot>().gameObject.GetComponent<BattleBot>().playerStartingHealth;
        currentHealth = FindObjectOfType<BattleBot>().gameObject.GetComponent<BattleBot>().playerHealth;
        level = FindObjectOfType<BattleBot>().gameObject.GetComponent<BattleBot>().playerLevel;
        rangedDamage = FindObjectOfType<BattleBot>().gameObject.GetComponent<BattleBot>().rangedDamage;
        meleeDamage = FindObjectOfType<BattleBot>().gameObject.GetComponent<BattleBot>().meleeDamage;
        resistance = FindObjectOfType<BattleBot>().gameObject.GetComponent<BattleBot>().resistance;
        magCapacity = FindObjectOfType<BattleBot>().gameObject.GetComponent<BattleBot>().playerMaganizeCapacity;
        ammo = FindObjectOfType<BattleBot>().gameObject.GetComponent<BattleBot>().playerAmmo;
        fireRate = FindObjectOfType<BattleBot>().gameObject.GetComponent<BattleBot>().playerFireRate;
        dashForce = FindObjectOfType<BattleBot>().gameObject.GetComponent<BattleBot>().dashForce;
        jumpStrength = FindObjectOfType<BattleBot>().gameObject.GetComponent<BattleBot>().jumpStrenth;
        moveSpeed = FindObjectOfType<BattleBot>().gameObject.GetComponent<BattleBot>().playerMoveSpeed;
        lookSpeed = FindObjectOfType<BattleBot>().gameObject.GetComponent<BattleBot>().playerLookSpeed;

        //player events
        isReloading = FindObjectOfType<BattleBot>().gameObject.GetComponent<BattleBot>().isReloading;
        isJumping = FindObjectOfType<PlayerMovement>().gameObject.GetComponent<PlayerMovement>().isJumping;
        //isAttacking = FindObjectOfType<PlayerMovement>().gameObject.GetComponent<PlayerMovement>().isJumping;

        // Create a new thread for the socket communication
        socketThread = new Thread(SocketThreadFunc);
        socketThread.Start();
    }

    private void Update()
    {
        message = "Player1,IP,Port" + playerTransform.position.x +
                "," + playerTransform.position.y + "," + playerTransform.position.z + "," + playerTransform.rotation.x + "," + playerTransform.rotation.y + "," + playerTransform.rotation.z + "," //Position Info
               + bigMag + "," + gas + "," + rocketBoots + "," + hiCalBullets + "," + energyDrink + "," + specialSerum + "," + bodyArmor + ","  + aimChip + "," + loCalBullet + "," + fourLeaf + "," //items
               + cType + "," + startHealth + "," + currentHealth + "," + level + "," + rangedDamage + "," + meleeDamage + "," + resistance + "," + magCapacity + "," + ammo + "," + fireRate + "," + dashForce + "," + jumpStrength + "," + moveSpeed + "," + lookSpeed + "," //Player stats
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

            byte[] buffer = new byte[1024];
            int receivedLength = socket.Receive(buffer);

            // Convert the received bytes into a string
            string response = System.Text.Encoding.UTF8.GetString(buffer, 0, receivedLength);

            serverRes = response;

            // Wait for a short period before sending more data
            Thread.Sleep(100);

            if (response.Length == 0) break;
        }
    }
}
