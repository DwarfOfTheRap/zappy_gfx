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

	public string GetSquareContentString(int x, int y)
	{
		if (x < 0 || y < 0)
			throw new NegativeSquareCoordException ("Negative Square Coordinates.");
		return "bct " + x + " " + y + "\n";
	}

	public string GetAllSquaresString()
	{
		return "mct\n";
	}

	public string GetTeamNamesString()
	{
		return "tna\n";
	}

	public string GetPlayerPosition(int n)
	{
		if (n < 0)
			throw new NegativePlayerIndexException("Negative Player Index.");
		return "ppo #" + n + "\n";
	}

	public string GetPlayerLevel(int n)
	{
		if (n < 0)
			throw new NegativePlayerIndexException("Negative Player Index.");
		return "plv #" + n + "\n";
	}

	public string GetPlayerInventory(int n)
	{
		if (n < 0)
			throw new NegativePlayerIndexException("Negative Player Index.");
		return "pin #" + n + "\n";
	}

	public string GetCurrentTimeUnitString()
	{
		return "sgt\n";
	}

	public string GetTimeUnitChangeString(int t)
	{
		if (t < 0)
			throw new NegativeTimeUnitException("Negative Time Unit.");
		return "sst " + t + "\n";
	}
	
	public void GetSquareContent(int x, int y)
	{
		// SendToSocket(GetSquareContentString());
	}

	public void GetMapSize()
	{
		// SendToSocket(GetMapSizeString(());
	}
}
