using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

public class ClientScript : MonoBehaviour {
	public static ClientScript	instance;

	//variables
	private TCPConnection myTCP;

	private string serverMsg;	
	public string msgToServer;

	void Awake() {	
		//add a copy of TCPConnection to this game object	
		myTCP = gameObject.AddComponent<TCPConnection>();	
	}
	

	void Start () {	
		instance = this;
	}

	void Update () {
	}
	
	//socket reading script
	public string SocketResponse() {
		string serverSays = myTCP.readSocket();
		if (serverSays != "") {
			Debug.Log("[SERVER]" + serverSays);
		}
		return serverSays;
	}

	//send message to the server
	public void SendToServer(string str) {	
		myTCP.writeSocket(str);	
		Debug.Log ("[CLIENT] -> " + str);	
	}		
}
