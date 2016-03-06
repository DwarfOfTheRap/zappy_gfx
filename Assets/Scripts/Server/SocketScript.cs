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

	void Start () {	
		instance = this;
	}

	void Update () {
        TCPConnection.maintainConnection(conName, conHost, conPort);
	}

    public void SetupConnection(string conName, string conHost, int conPort)
    {
        this.conName = conName;
        this.conHost = conHost;
        this.conPort = conPort;
        TCPConnection.setupSocket(this.conName, this.conHost, this.conPort);
    }

	//socket reading script
	public string SocketResponse() {
		string serverSays = TCPConnection.readSocket();
		if (serverSays != "") {
			Debug.Log("[SERVER]" + serverSays);
		}
		return serverSays;
	}

	//send message to the server
	public void SendToServer(string str) {	
		TCPConnection.writeSocket(str);	
		Debug.Log ("[CLIENT] -> " + str);	
	}		
}
