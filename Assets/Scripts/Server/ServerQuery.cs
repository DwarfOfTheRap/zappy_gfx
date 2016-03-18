using UnityEngine;
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

	void SendToServer(string query)
	{
		SendToServer (query, true);
	}

	void SendToServer(string query, bool log)
	{
		SocketManager.instance.SendToServer (query);
		if (log)
			GameManagerScript.instance.debugManager.AddLog("<color=cyan>[CLIENT]</color> -> " + query);
	}

	string GetMapSizeString()
	{
		return "msz\n";
	}

	public void SendMapSizeQuery()
	{
		SendToServer (GetMapSizeString ());
	}

	string GetSquareContentString(int x, int y)
	{
		if (x < 0 || y < 0)
			throw new NegativeSquareCoordException ("Negative Square Coordinates.");
		return "bct " + x + " " + y + "\n";
	}

	public void SendSquareContentQuery(int x, int y)
	{
		SendToServer (GetSquareContentString(x, y));
	}

	string GetAllSquaresString()
	{
		return "mct\n";
	}

	public void SendAllSquaresQuery()
	{
		SendToServer (GetAllSquaresString ());
	}

	string GetTeamNamesString()
	{
		return "tna\n";
	}

	public void SendTeamNamesQuery()
	{
		SendToServer (GetTeamNamesString ());
	}

	string GetPlayerPositionString(int n)
	{
		if (n < 0)
			throw new NegativePlayerIndexException("Negative Player Index.");
		return "ppo " + n + "\n";
	}

	public void SendPlayerPositionQuery(int n)
	{
		SendToServer (GetPlayerPositionString (n));
	}

	string GetPlayerLevelString(int n)
	{
		if (n < 0)
			throw new NegativePlayerIndexException("Negative Player Index.");
		return "plv " + n + "\n";
	}

	public void SendPlayerLevelQuery(int n)
	{
		SendToServer (GetPlayerLevelString (n));
	}

	string GetPlayerInventoryString(int n)
	{
		if (n < 0)
			throw new NegativePlayerIndexException("Negative Player Index.");
		return "pin " + n + "\n";
	}

	public void SendPlayerInventoryQuery(int n)
	{
		SendToServer (GetPlayerInventoryString (n));
	}

	string GetCurrentTimeUnitString()
	{
		return "sgt\n";
	}

	public void SendCurrentTimeUnitQuery()
	{
		SendToServer (GetCurrentTimeUnitString (), false);
	}

	string GetTimeUnitChangeString(int t)
	{
		if (t < 0)
			throw new NegativeTimeUnitException("Negative Time Unit.");
		return "sst " + t + "\n";
	}

	public void SendTimeUnitChangeQuery(int t)
	{
		SendToServer (GetTimeUnitChangeString (t));
	}

	string GetWelcomeMessageString()
	{
		return "GRAPHIC\n";
	}

	public void SendWelcomeMessage()
	{
		SendToServer (GetWelcomeMessageString ());
	}
}
