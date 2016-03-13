using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;

public class TCPConnection
{
    private bool _socketReady = false;

    private TcpClient _mySocket;
    private NetworkStream _stream;
    private StreamWriter _writer;
    private StreamReader _reader;

    public void SetupSocket(string conHost, int conPort)
    {
        _mySocket = new TcpClient(conHost, conPort);
        _stream = _mySocket.GetStream();
        _writer = new StreamWriter(_stream);
        _reader = new StreamReader(_stream);
        _socketReady = true;
    }

    public void WriteSocket(string message)
    {
        if (!_socketReady)
            return;
        _writer.Write(message);
        _writer.Flush();
    }

    public string ReadSocket()
    {
        String result = "";
        while (_stream.DataAvailable)
        {
            Byte[] inStream = new Byte[_mySocket.SendBufferSize];
            var bytes = _stream.Read(inStream, 0, inStream.Length);
            result += System.Text.Encoding.UTF8.GetString(inStream, 0, bytes);
        }
        return result;
    }

    public void CloseSocket()
    {
        if (!_socketReady)
            return;
        _writer.Close();
        _reader.Close();
        _mySocket.Close();
        _socketReady = false;
    }

    public bool MaintainConnection(string conHost, int conPort)
    {
        if (!_stream.CanRead)
        {
            SetupSocket(conHost, conPort);
        }
		return (_stream.CanRead);
    }
}