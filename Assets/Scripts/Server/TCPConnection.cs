﻿using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;

public static class TCPConnection {
	
	//the name of the connection, not required but better for overview if you have more than 1 connections running
	//public string conName = "Localhost";
	
	//ip/address of the server, 127.0.0.1 is for your own computer
	//public string conHost = "127.0.0.1";
	
	//port for the server, make sure to unblock this in your router firewall if you want to allow external connections
	//public int conPort = 27015;
	
	//a true/false variable for connection status
	private static bool socketReady = false;

    private static TcpClient mySocket;
    private static NetworkStream stream;
    private static StreamWriter writer;
    private static StreamReader reader;
	
	//try to initiate connection
	public static void setupSocket(string conName, string conHost, int conPort) {
		try {
			mySocket = new TcpClient(conHost, conPort);
			stream = mySocket.GetStream();
			writer = new StreamWriter(stream);
			reader = new StreamReader(stream);
			socketReady = true;
		}
		catch (Exception e) {
			Debug.Log("Socket error:" + e);
		}
	}
	
	//send message to server
	public static void writeSocket(string message) {
		if (!socketReady)
			return ;
		writer.Write(message);
		writer.Flush();
	}
	
	//read message from server
	public static string readSocket() {
		String result = "";
		if (stream.DataAvailable) {
			Byte[] inStream = new Byte[mySocket.SendBufferSize];
			stream.Read(inStream, 0, inStream.Length);
			result += System.Text.Encoding.UTF8.GetString(inStream);
		}
		return result;
	}
	
	//disconnect from the socket
	public static void closeSocket() {
		if (!socketReady)
			return ;
		writer.Close();
		reader.Close();
		mySocket.Close();
		socketReady = false;
	}
	
	//keep connection alive, reconnect if connection lost
	public static void maintainConnection(string conName, string conHost, int conPort)
    {
		if(!stream.CanRead) {
			setupSocket(conName, conHost, conPort);
		}
	}
	
	
}