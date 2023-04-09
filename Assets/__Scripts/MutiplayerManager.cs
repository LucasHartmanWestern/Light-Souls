using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Threading;
using System.Text;

public class MutiplayerManager : MonoBehaviour
{
    string ipAddress = "54.196.231.67";
    int port = 2001;

    public Transform playerTransform;
    public string message = "";
    public string serverRes = "";
    byte[] data;
    byte[] lengthPrefix;
    public byte[] finalData;

    Thread socketThread;

    void Start()
    {
        playerTransform = FindObjectOfType<MutiplayerManager>().gameObject.transform;

        // Create a new thread for the socket communication
        socketThread = new Thread(SocketThreadFunc);
        socketThread.Start();
    }

    private void Update()
    {
        message = "Player1," + playerTransform.position.x +
                "," + playerTransform.position.y + "," + playerTransform.position.z;

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
