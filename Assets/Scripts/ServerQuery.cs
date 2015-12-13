using UnityEngine;
using System;
using System.Collections;

public class ServerQuery : MonoBehaviour {
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
	
	public static ServerQuery	instance;

	void Start()
	{
		instance = this;
	}

	public string GetMapSizeString()
	{
		return "msz\n";
	}

	public void SendMapSizeQuery()
	{
		ClientScript.instance.SendToServer (GetMapSizeString ());
	}

	public string GetSquareContentString(int x, int y)
	{
		if (x < 0 || y < 0)
			throw new NegativeSquareCoordException ("Negative Square Coordinates.");
		return "bct " + x + " " + y + "\n";
	}

	public void SendSquareContentQuery(int x, int y)
	{
		ClientScript.instance.SendToServer (GetSquareContentString(x, y));
	}

	public string GetAllSquaresString()
	{
		return "mct\n";
	}

	public void SendAllSquaresQuery()
	{
		ClientScript.instance.SendToServer (GetAllSquaresString ());
	}

	public string GetTeamNamesString()
	{
		return "tna\n";
	}

	public void SendTeamNamesQuery()
	{
		ClientScript.instance.SendToServer (GetTeamNamesString());
	}

	public string GetPlayerPositionString(int n)
	{
		if (n < 0)
			throw new NegativePlayerIndexException("Negative Player Index.");
		return "ppo #" + n + "\n";
	}

	public void SendPlayerPositionQuery(int n)
	{
		ClientScript.instance.SendToServer (GetPlayerPositionString(n));
	}

	public string GetPlayerLevelString(int n)
	{
		if (n < 0)
			throw new NegativePlayerIndexException("Negative Player Index.");
		return "plv #" + n + "\n";
	}

	public void SendPlayerLevelQuery(int n)
	{
		ClientScript.instance.SendToServer (GetPlayerLevelString(n));
	}

	public string GetPlayerInventoryString(int n)
	{
		if (n < 0)
			throw new NegativePlayerIndexException("Negative Player Index.");
		return "pin #" + n + "\n";
	}

	public void SendPlayerInventoryQuery(int n)
	{
		ClientScript.instance.SendToServer (GetPlayerInventoryString (n));
	}

	public string GetCurrentTimeUnitString()
	{
		return "sgt\n";
	}

	public void SendCurrentTimeUnitQuery()
	{
		ClientScript.instance.SendToServer (GetCurrentTimeUnitString ());
	}

	public string GetTimeUnitChangeString(int t)
	{
		if (t < 0)
			throw new NegativeTimeUnitException("Negative Time Unit.");
		return "sst " + t + "\n";
	}

	public void SendTimeUnitChangeQuery(int t)
	{
		ClientScript.instance.SendToServer (GetTimeUnitChangeString (t));
	}

	public string GetWelcomeMessageString()
	{
		return "GRAPHIC\n";
	}

	public void SendWelcomeMessage()
	{
		ClientScript.instance.SendToServer(GetWelcomeMessageString());
	}
}
