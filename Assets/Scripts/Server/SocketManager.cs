﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System;

public class SocketManager : MonoBehaviour
{
    public static SocketManager instance;

    public string conHost { get; private set; }
    public int conPort { get; private set; }

	public bool wait = false;
    public bool connected = false;

#if !UNITY_EDITOR
	private float _previousTime;
#endif

	private const float _timeout = 5.0f;
	private const float _resetGame = 15.0f;

	private TCPConnection	_connection;
	private ServerCommands	_commands;
	private ServerReader	_reader;

	public delegate void DisconnectedEvent ();
	public event DisconnectedEvent OnDisconnection;
	public event DisconnectedEvent OnReconnection;
	public event DisconnectedEvent OnConnectionCanceled;

    void Awake()
    {
        DontDestroyOnLoad(this);
		_reader = new ServerReader();
		_commands = GameManagerScript.instance.commandsManager;
		GameManagerScript.instance.inputManager.OnRefreshKeyUp += Refresh;
    }

    void Start()
    {
        instance = this;
        _connection = new TCPConnection();
    }

	void OnDisable()
	{
		GameManagerScript.instance.inputManager.OnRefreshKeyUp -= Refresh;
	}
		
	void OnDestroy()
	{
		CloseSocket ();
	}
	
	public void CloseSocket()
	{
		_connection.CloseSocket ();
		StopAllCoroutines ();
		connected = false;
		if (OnConnectionCanceled != null)
			OnConnectionCanceled();
	}

	public void ResetLevel()
	{
		Destroy (GameManagerScript.instance);
		Destroy (gameObject);
		Application.LoadLevel(0);
	}

    IEnumerator ReadServerMessages()
    {
		string response;
#if !UNITY_EDITOR
		_previousTime = Time.realtimeSinceStartup;
#endif
		while (true)
		{
			_connection.MaintainConnection (conHost, conPort);
			response = SocketResponse ();
			if (response != "")
			{
#if !UNITY_EDITOR
				if (Time.realtimeSinceStartup - _previousTime > _timeout && Application.loadedLevel == 1)
					Refresh ();
#endif
				foreach (string serverMessage in _reader.SplitMessage (response)) {
					while (wait)
						yield return null;
					if (serverMessage != "" && _reader.IsLegitMessage (serverMessage)) {
						try {
							_commands.PickMethod (serverMessage);
						}
						catch (Exception e) {
								GameManagerScript.instance.debugManager.AddLog(e.Message);
							Debug.LogError (e.Message);
						}
					}
					else if (serverMessage != "")
						GameManagerScript.instance.debugManager.AddLog("<color=magenta>[SERVER] -></color>" + serverMessage);
				}
#if !UNITY_EDITOR
				_previousTime = Time.realtimeSinceStartup;
#endif
			}
#if !UNITY_EDITOR
			else if (Time.realtimeSinceStartup - _previousTime > _resetGame && Application.loadedLevel == 1)
				ResetLevel ();
			else if (Time.realtimeSinceStartup - _previousTime > _timeout && Application.loadedLevel == 1)
			{
				if (OnDisconnection != null)
					OnDisconnection();
				Debug.Log ("Disconnected from server.");
			}
#endif
			yield return null;
		}
    }

	IEnumerator PingServer ()
	{
		ServerQuery query = new ServerQuery();

		while (true)
		{
			yield return new WaitForSeconds(2.5f);
			query.SendCurrentTimeUnitQuery();
		}
	}

	void Refresh ()
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
		if (OnReconnection != null)
			OnReconnection();
	}

	public void StartPingServer()
	{
#if !UNITY_EDITOR
		StartCoroutine (PingServer ());
#endif
	}

    public void SetupConnection(string conHost, int conPort)
    {
		GameManagerScript.instance.debugManager.AddLog("Connecting to IP: " + conHost + " at Port: " + conPort);
		Debug.Log ("Connecting to IP: " + conHost + " at Port: " + conPort);
        this.conHost = conHost;
        this.conPort = conPort;
        _connection.SetupSocket(this.conHost, this.conPort);
		connected = true;
		StartCoroutine (ReadServerMessages ());
    }

    //socket reading script
   	string SocketResponse()
    {
        string serverSays = _connection.ReadSocket();
        return serverSays;
    }

    //send message to the server
   	public void SendToServer(string str)
    {
        _connection.WriteSocket(str);
    }
}
