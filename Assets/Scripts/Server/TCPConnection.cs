using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;

public class TCPConnection
{

    //the name of the connection, not required but better for overview if you have more than 1 connections running
    //public string conName = "Localhost";

    //ip/address of the server, 127.0.0.1 is for your own computer
    //public string conHost = "127.0.0.1";

    //port for the server, make sure to unblock this in your router firewall if you want to allow external connections
    //public int conPort = 27015;

    //a true/false variable for connection status
    private bool _socketReady = false;

    private TcpClient _mySocket;
    private NetworkStream _stream;
    private StreamWriter _writer;
    private StreamReader _reader;

    //try to initiate connection
    public void SetupSocket(string conHost, int conPort)
    {
        _mySocket = new TcpClient(conHost, conPort);
        _stream = _mySocket.GetStream();
        _writer = new StreamWriter(_stream);
        _reader = new StreamReader(_stream);
        _socketReady = true;
    }

    //send message to server
    public void WriteSocket(string message)
    {
        if (!_socketReady)
            return;
        _writer.Write(message);
        _writer.Flush();
    }

    //read message from server
    public string ReadSocket()
    {
        String result = "";
        while (_stream.DataAvailable)
        {
            Byte[] inStream = new Byte[_mySocket.SendBufferSize];
            _stream.Read(inStream, 0, inStream.Length);
            result += System.Text.Encoding.UTF8.GetString(inStream);
        }
        return result;
    }

    //disconnect from the socket
    public void CloseSocket()
    {
        if (!_socketReady)
            return;
        _writer.Close();
        _reader.Close();
        _mySocket.Close();
        _socketReady = false;
    }

    //keep connection alive, reconnect if connection lost
    public bool MaintainConnection(string conHost, int conPort)
    {
        if (!_stream.CanRead)
        {
            SetupSocket(conHost, conPort);
        }
		return (_stream.CanRead);
    }
}