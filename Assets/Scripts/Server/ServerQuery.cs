﻿using UnityEngine;
using System;
using System.Collections;

public class ServerQuery {
	public class ServerQueryException : Exception
	{
		public ServerQueryException(){}

		public ServerQueryException(string message) : base(message) {}
	}

	public class NegativeSquareCoordException : ServerQueryException
	{
		public NegativeSquareCoordException() {}

		public NegativeSquareCoordException(string message) : base(message) {}
	}

	public class NegativePlayerIndexException : ServerQueryException
	{
		public NegativePlayerIndexException() {}
		
		public NegativePlayerIndexException(string message) : base(message) {}
	}

	public class NegativeTimeUnitException : ServerQueryException
	{
		public NegativeTimeUnitException() {}
		
		public NegativeTimeUnitException(string message) : base(message) {}
	}

	public string GetMapSizeString()
	{
		return "msz\n";
	}

	public void SendMapSizeQuery()
	{
		SocketManager.instance.SendToServer (GetMapSizeString ());
	}

	public string GetSquareContentString(int x, int y)
	{
		if (x < 0 || y < 0)
			throw new NegativeSquareCoordException ("Negative Square Coordinates.");
		return "bct " + x + " " + y + "\n";
	}

	public void SendSquareContentQuery(int x, int y)
	{
		SocketManager.instance.SendToServer (GetSquareContentString(x, y));
	}

	public string GetAllSquaresString()
	{
		return "mct\n";
	}

	public void SendAllSquaresQuery()
	{
		SocketManager.instance.SendToServer (GetAllSquaresString ());
	}

	public string GetTeamNamesString()
	{
		return "tna\n";
	}

	public void SendTeamNamesQuery()
	{
		SocketManager.instance.SendToServer (GetTeamNamesString());
	}

	public string GetPlayerPositionString(int n)
	{
		if (n < 0)
			throw new NegativePlayerIndexException("Negative Player Index.");
		return "ppo #" + n + "\n";
	}

	public void SendPlayerPositionQuery(int n)
	{
		SocketManager.instance.SendToServer (GetPlayerPositionString(n));
	}

	public string GetPlayerLevelString(int n)
	{
		if (n < 0)
			throw new NegativePlayerIndexException("Negative Player Index.");
		return "plv #" + n + "\n";
	}

	public void SendPlayerLevelQuery(int n)
	{
		SocketManager.instance.SendToServer (GetPlayerLevelString(n));
	}

	public string GetPlayerInventoryString(int n)
	{
		if (n < 0)
			throw new NegativePlayerIndexException("Negative Player Index.");
		return "pin #" + n + "\n";
	}

	public void SendPlayerInventoryQuery(int n)
	{
		SocketManager.instance.SendToServer (GetPlayerInventoryString (n));
	}

	public string GetCurrentTimeUnitString()
	{
		return "sgt\n";
	}

	public void SendCurrentTimeUnitQuery()
	{
		SocketManager.instance.SendToServer (GetCurrentTimeUnitString ());
	}

	public string GetTimeUnitChangeString(int t)
	{
		if (t < 0)
			throw new NegativeTimeUnitException("Negative Time Unit.");
		return "sst " + t + "\n";
	}

	public void SendTimeUnitChangeQuery(int t)
	{
		SocketManager.instance.SendToServer (GetTimeUnitChangeString (t));
	}

	public string GetWelcomeMessageString()
	{
		return "GRAPHIC\n";
	}

	public void SendWelcomeMessage()
	{
		SocketManager.instance.SendToServer(GetWelcomeMessageString());
	}
}
