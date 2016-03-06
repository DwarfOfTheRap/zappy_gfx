using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

public class SocketManager : MonoBehaviour
{
    public static SocketManager instance;

    //variables
    private string serverMsg;
    public string msgToServer;

    public string conHost { get; private set; }
    public int conPort { get; private set; }

    private TCPConnection connection;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        instance = this;
        connection = new TCPConnection();
    }

    void Update()
    {
        connection.maintainConnection(conHost, conPort);
    }

    void OnApplicationQuit()
    {
        connection.closeSocket();
    }

    public void SetupConnection(string conHost, int conPort)
    {
        this.conHost = conHost;
        this.conPort = conPort;
        connection.setupSocket(this.conHost, this.conPort);
    }

    //socket reading script
    public string SocketResponse()
    {
        string serverSays = connection.readSocket();
        if (serverSays != "")
        {
            Debug.Log("[SERVER]" + serverSays);
        }
        return serverSays;
    }

    //send message to the server
    public void SendToServer(string str)
    {
        connection.writeSocket(str);
        Debug.Log("[CLIENT] -> " + str);
    }
}
