using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Threading;
using System.Text;
using System.Linq;
using UnityEngine.SceneManagement;

public class MutiplayerManager : MonoBehaviour
{
    string ipAddress = "54.196.231.67";
    int port = 2001;

    string sceneName = "";

    public string serverName = "";

    public Transform playerTransform;

    // Player stats
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

    // Player events
    bool isAttacking;
    bool isJumping;
    bool isReloading;

    // Input manager variables
    
     Vector2 movementInput; // 2D vector tracking where the player is trying to move

     float verticalInput; // Track vertical input
     float horizontalInput; // Track horizontal input
     float cameraInputX; // Track camera input on the x axes
     float cameraInputY; // Track the camera input on the y axes

   
     float moveAmount; // Determine the amount to move
     bool sprintInput; // Check if player is trying to sprint
     bool jumpInput; // Check if player is trying to jump
     bool aimInput; // Check if player is trying to aim
     bool attackInput; // Check sif player is trying to attack
     bool specialMoveInput; // Check if player is trying to use their special moving abilitiy
     bool lockOnInput; // Check if player is trying to lock onto an enemy
     bool lockOnLeftInput; // Check if player is trying to lock onto a different enemy
     bool lockOnRightInput; // Check if player is trying to lock onto a different enemy
     bool specialAbilityInput; // Check if player is trying to use their special ability
     bool reloadInput; // Check if player is trying to reload

    
     bool lockOnFlag; // Check if player should be locked on


    public string userName = "";
    public string preFabName = "";
    public string message = "";
    public string serverRes = "";
    byte[] data;
    byte[] lengthPrefix;
    public byte[] finalData;

    float animatorHorizontal;
    float animatorVertical;

    Thread socketThread;

    private void Awake()
    {
        userName = GameObject.Find("MultiplayerUserInfo").GetComponent<UserSelector>().theName;
        serverName = GameObject.Find("MultiplayerUserInfo").GetComponent<UserSelector>().serveraddy;
        sceneName = SceneManager.GetActiveScene().name;
    }

    void Start()
    {
        playerTransform = FindObjectOfType<MutiplayerManager>().gameObject.transform;

        // Player stats
        startHealth = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().playerStartingHealth;
        currentHealth = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().playerHealth;
        level = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().playerLevel;
        rangedDamage = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().rangedDamage;
        meleeDamage = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().meleeDamage;
        resistance = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().resistance;
        magCapacity = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().playerMaganizeCapacity;
        ammo = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().playerAmmo;
        fireRate = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().playerFireRate;
        jumpStrength = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().jumpStrenth;
        moveSpeed = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().playerMoveSpeed;
        lookSpeed = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().playerLookSpeed;

        // Player events
        isReloading = FindObjectOfType<PlayerGeneral>().gameObject.GetComponent<PlayerGeneral>().isReloading;
        isJumping = FindObjectOfType<PlayerMovement>().gameObject.GetComponent<PlayerMovement>().isJumping;

        // Input manager variables
        movementInput = gameObject.GetComponent<InputManager>().movementInput;

        verticalInput = FindObjectOfType<InputManager>().gameObject.GetComponent<InputManager>().verticalInput;
        horizontalInput = FindObjectOfType<InputManager>().gameObject.GetComponent<InputManager>().horizontalInput;
        cameraInputX = FindObjectOfType<InputManager>().gameObject.GetComponent<InputManager>().cameraInputX;
        cameraInputY = FindObjectOfType<InputManager>().gameObject.GetComponent<InputManager>().cameraInputY;

        // Create a new thread for the socket communication
        socketThread = new Thread(SocketThreadFunc);
        socketThread.Start();
    }

    private void Update()
    {
        // Gather and format data for request to server
        playerTransform.gameObject.name = userName;
        string[] values = {
            userName, serverName, sceneName, ipAddress, port.ToString(),
            Math.Round(playerTransform.position.x).ToString(), Math.Round(playerTransform.position.y).ToString(), Math.Round(playerTransform.position.z).ToString(),
            Math.Round(playerTransform.rotation.eulerAngles.x).ToString(), Math.Round(playerTransform.rotation.eulerAngles.y).ToString(), Math.Round(playerTransform.rotation.eulerAngles.z).ToString(),
            preFabName,
            startHealth.ToString(),
            currentHealth.ToString(),
            gameObject.GetComponent<InputManager>().moveAmount.ToString(),
            gameObject.GetComponent<InputManager>().sprintInput.ToString(),
            gameObject.GetComponent<InputManager>().jumpInput.ToString(),
            gameObject.GetComponent<InputManager>().aimInput.ToString(),
        };

        message = string.Join(",", values);

        data = System.Text.Encoding.ASCII.GetBytes(message);
        
        lengthPrefix = BitConverter.GetBytes(data.Length); // Add length prefix
        finalData = new byte[lengthPrefix.Length + data.Length];
        lengthPrefix.CopyTo(finalData, 0);
        data.CopyTo(finalData, lengthPrefix.Length);
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

            byte[] buffer = new byte[32768];
            int receivedLength = socket.Receive(buffer);

            // Convert the received bytes into a string
            string response = System.Text.Encoding.UTF8.GetString(buffer, 0, receivedLength);

            if (response.Length == 0) break; // Server is down

            // Filter out specific players
            string[] splitResponse = response.Split('|');
            for (int i = 0; i < splitResponse.Length; i++)
            {
                string serverName = splitResponse[i].Split(',')[0];
                string hostName = message.Split(',')[0];

                string serverGameName = splitResponse[i].Split(',')[1];
                string hostGameName = message.Split(',')[1];

                string serverSceneName = splitResponse[i].Split(',')[2];
                string hostSceneName = message.Split(',')[2];

                if (serverName == hostName || serverGameName != hostGameName || serverSceneName != hostSceneName)
                {
                    splitResponse[i] = null;
                }
            }
            
            splitResponse = splitResponse.Where(x => x != null).ToArray();
            response = string.Join("|", splitResponse);

            serverRes = response;
            
            // Wait for a short period before sending more data
            Thread.Sleep(100);
        }
    }
}
