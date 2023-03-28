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

    public string message = "player 1, test 1, test 2";
    byte[] data;
    byte[] lengthPrefix;
    byte[] finalData;

    Thread socketThread;

    void Start()
    {
        data = System.Text.Encoding.ASCII.GetBytes(message);
        lengthPrefix = BitConverter.GetBytes(data.Length); // Add length prefix
        finalData = new byte[lengthPrefix.Length + data.Length];
        lengthPrefix.CopyTo(finalData, 0);
        data.CopyTo(finalData, lengthPrefix.Length);

        // Create a new thread for the socket communication
        socketThread = new Thread(SocketThreadFunc);
        socketThread.Start();
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
            // Send data to the server
            socket.Send(finalData);

            byte[] buffer = new byte[1024];
            int receivedLength = socket.Receive(buffer);

            // Convert the received bytes into a string
            string response = System.Text.Encoding.UTF8.GetString(buffer, 0, receivedLength);

            Debug.Log("Response: " + response);

            // Wait for a short period before sending more data
            Thread.Sleep(100);

            if (response.Length == 0) break;
        }
    }
}
