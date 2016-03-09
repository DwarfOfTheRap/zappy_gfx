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

	public bool wait = false;
    public bool connected = false;

    private TCPConnection connection;

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
        connected = connection.socketAvailable();
    }

    IEnumerator ReadServerMessages()
    {
		string response;

		while (true)
		{
        	connection.maintainConnection(conHost, conPort);
			response = SocketResponse ();
			if (response != "")
			{
				foreach (string serverMessage in reader.SplitMessage (response))
				{
					while (wait)
						yield return null;
					if (serverMessage != "" && reader.IsLegitMessage (serverMessage))
					{
						try
						{
							commands.PickMethod (serverMessage);
						}
						catch (Exception e)
						{
							Debug.Log (e.Message);
						}
					}
				}
			}
			yield return new WaitForEndOfFrame();
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
		StartCoroutine (ReadServerMessages ());
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
