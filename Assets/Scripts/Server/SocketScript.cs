using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

public class ClientScript : MonoBehaviour {
	public static ClientScript	instance;

	//variables
	private string serverMsg;	
	public string msgToServer;

    public string conName;
    public string conHost;
    public int conPort;

    private TCPConnection connection;

	void Start () {	
		instance = this;
        connection = new TCPConnection();
	}

	void Update () {
        connection.maintainConnection(conName, conHost, conPort);
	}

    void OnApplicationQuit ()
    {
        connection.closeSocket();
    }

    public void SetupConnection(string conName, string conHost, int conPort)
    {
        this.conName = conName;
        this.conHost = conHost;
        this.conPort = conPort;
        connection.setupSocket(this.conName, this.conHost, this.conPort);
    }

	//socket reading script
	public string SocketResponse() {
		string serverSays = connection.readSocket();
		if (serverSays != "") {
			Debug.Log("[SERVER]" + serverSays);
		}
		return serverSays;
	}

	//send message to the server
	public void SendToServer(string str) {	
		connection.writeSocket(str);	
		Debug.Log ("[CLIENT] -> " + str);	
	}		
}
