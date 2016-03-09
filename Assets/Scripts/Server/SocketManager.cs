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

	private float previousTime;
	private const float timeout = 5.0f;
	private const float resetGame = 15.0f;

	public delegate void DisconnectedEvent ();
	public event DisconnectedEvent OnDisconnection;
	public event DisconnectedEvent OnReconnection;

    void Awake()
    {
		Debug.Log ("Awake");
        DontDestroyOnLoad(this);
		reader = new ServerReader();
		commands = GameManagerScript.instance.commandsManager;
    }

    void Start()
    {
        instance = this;
        connection = new TCPConnection();
    }
		
	void OnDestroy()
	{
		Debug.Log ("OnDestroy");
		connection.closeSocket();
		StopAllCoroutines ();
	}

    IEnumerator ReadServerMessages()
    {
		string response;

		previousTime = Time.realtimeSinceStartup;
		while (true)
		{
			response = SocketResponse ();
			if (response != "")
			{
				if (Time.realtimeSinceStartup - previousTime > timeout)
				{
					if (OnReconnection != null)
						OnReconnection();
					GetServerInformations ();
				}
				foreach (string serverMessage in reader.SplitMessage (response)) {
					while (wait)
						yield return null;
					if (serverMessage != "" && reader.IsLegitMessage (serverMessage)) {
						try {
							commands.PickMethod (serverMessage);
						}
						catch (Exception e) {
							Debug.Log (e.Message);
						}
					}
				}
				previousTime = Time.realtimeSinceStartup;
			}
			else if (Time.realtimeSinceStartup - previousTime > resetGame)
			{
				previousTime = Time.realtimeSinceStartup;
				Destroy (GameManagerScript.instance);
				Destroy (gameObject);
				Debug.Log ("Restarting");
				Application.LoadLevel(0);
			}
			else if (Time.realtimeSinceStartup - previousTime > timeout)
			{
				if (OnDisconnection != null)
					OnDisconnection();
				Debug.Log ("Disconnected from server.");
			}
			yield return null;
		}
    }

	IEnumerator PingServer ()
	{
		ServerQuery query = new ServerQuery();

		while (true)
		{
			yield return new WaitForSeconds(1.0f);
			query.SendCurrentTimeUnitQuery();
		}
	}

	public void GetServerInformations ()
	{
		ServerQuery query = new ServerQuery();
		List<PlayerController> players;

		query.SendAllSquaresQuery();
		players = GameManagerScript.instance.playerManager.players;
		foreach (PlayerController player in players)
		{
			query.SendPlayerPositionQuery(player.index);
			query.SendPlayerInventoryQuery(player.index);
			query.SendPlayerLevelQuery(player.index);
		}
		query.SendTeamNamesQuery();
	}

    public void SetupConnection(string conHost, int conPort)
    {
        this.conHost = conHost;
        this.conPort = conPort;
        connection.setupSocket(this.conHost, this.conPort);
		StartCoroutine (ReadServerMessages ());
		#if !UNITY_EDITOR
		StartCoroutine (PingServer ());
		#endif
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
