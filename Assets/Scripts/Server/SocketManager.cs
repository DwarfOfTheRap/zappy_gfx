using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

public class SocketManager : MonoBehaviour
{
    public static SocketManager instance;

	public ServerCommands commands;
	public ServerReader reader;

    //variables
    private string serverMsg;
    public string msgToServer;

    public string conHost { get; private set; }
    public int conPort { get; private set; }

    private TCPConnection connection;
	private bool activated = false;

    void Awake()
    {
        DontDestroyOnLoad(this);
		reader = new ServerReader();
		commands = GameManagerScript.instance.commandsManager;
    }

    void Start()
    {
        instance = this;
        connection = new TCPConnection();
    }

    void Update()
    {
		string response;

		if (activated)
		{
        	connection.maintainConnection(conHost, conPort);
			response = SocketResponse ();
			if (response != "")
			{
				foreach (string serverMessage in reader.SplitMessage (response))
				{
					Debug.Log (serverMessage);
					if (reader.IsLegitMessage (serverMessage))
						commands.PickMethod (serverMessage);
				}
			}
		}
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
			activated = true;
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
