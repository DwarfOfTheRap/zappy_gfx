using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public static class ServerCommands {

	private const string cmd	= @"([a-z]{3})";
	private const string X		= @"([0-9]+)";
	private const string Y		= @"([0-9]+)";
	private const string q		= @"([0-9]+)";
	private const string N		= @"([\w ]{1,32})";
	private const string n		= @"(#[0-9]+)";
	private const string O		= @"([1-4])";
	private const string L		= @"([1-8])";

	public static void SendMapSize(string serverMessage)
	{
		Match regexMatch;

		regexMatch = Regex.Match (serverMessage, cmd + " " + X + " " + Y + "\n$");
		GameManagerScript.instance.grid.controller.Init (int.Parse (regexMatch.Groups[2].Value), int.Parse (regexMatch.Groups[3].Value));
	}

	public static void SendSquareContent(string serverMessage)
	{
		Match regexMatch;
		ISquare square;

		regexMatch = Regex.Match (serverMessage, cmd + " " + X + " " + Y + " " + q + " " + q + " " + q + " " + q + " " + q + " " + q + " " + q + "\n$");
		square = GameManagerScript.instance.grid.controller.GetSquare (int.Parse (regexMatch.Groups [2].Value), int.Parse (regexMatch.Groups [3].Value));
		square.SetResources (uint.Parse (regexMatch.Groups [4].Value),
		                    uint.Parse (regexMatch.Groups [5].Value),
		                    uint.Parse (regexMatch.Groups [6].Value),
		                    uint.Parse (regexMatch.Groups [7].Value),
		                    uint.Parse (regexMatch.Groups [8].Value),
		                    uint.Parse (regexMatch.Groups [9].Value),
		                    uint.Parse (regexMatch.Groups [10].Value));
	}

	public static void SendTeamName(string serverMessage)
	{
		Match regexMatch;

		regexMatch = Regex.Match (serverMessage, cmd + " " + N + "\n$");
		GameManagerScript.instance.teamManager.createTeam (regexMatch.Groups [2].Value);
	}

	public static void SendPlayerConnection(string serverMessage)
	{
		Match regexMatch;

		regexMatch = Regex.Match (serverMessage, cmd + " " + n + " " + X + " " + Y + " " + O + " " + L + " " + N + "\n$");
		GameManagerScript.instance.playerManager.SetPlayerConnection (int.Parse (regexMatch.Groups[2].Value),
		                                                             int.Parse (regexMatch.Groups[3].Value),
		          													 int.Parse (regexMatch.Groups[4].Value),
		                                                             (Orientation)int.Parse (regexMatch.Groups[5].Value),
		                                                             int.Parse (regexMatch.Groups[6].Value),
		                                                             regexMatch.Groups[7].Value);
	}

	public static void SendPlayerPosition(string serverMessage)
	{
		Match regexMatch;

		regexMatch = Regex.Match (serverMessage, cmd + " " + n + " " + X + " " + Y + " " + O + "\n$");
		GameManagerScript.instance.playerManager.SetPlayerPosition (int.Parse (regexMatch.Groups [2].Value),
		                                                           int.Parse (regexMatch.Groups [3].Value),
		                                                           int.Parse (regexMatch.Groups [4].Value),
		                                                           (Orientation)int.Parse (regexMatch.Groups [5].Value));
	}

	public static void SendPlayerLevel(string serverMessage)
	{
		Match regexMatch;

		regexMatch = Regex.Match (serverMessage, cmd + " " + n + " " + L + "\n$");
		GameManagerScript.instance.playerManager.SetPlayerLevel (int.Parse (regexMatch.Groups [2].Value),
		                                                         int.Parse (regexMatch.Groups [3].Value));
	}

	public static void SendPlayerInventory(string serverMessage)
	{
		Match regexMatch;

		regexMatch = Regex.Match (serverMessage, cmd + " " + n + " " + X + " " + Y + " " + q + " " + q + " " + q + " " + q + " " + q + " " + q + " " + q + "\n$");
		GameManagerScript.instance.playerManager.SetPlayerInventory (int.Parse (regexMatch.Groups [2].Value),
		                                                             int.Parse (regexMatch.Groups [5].Value),
		                                                             int.Parse (regexMatch.Groups [6].Value),
		                                                             int.Parse (regexMatch.Groups [7].Value),
		                                                             int.Parse (regexMatch.Groups [8].Value),
		                                                             int.Parse (regexMatch.Groups [9].Value),
		                                                             int.Parse (regexMatch.Groups [10].Value),
		                                                             int.Parse (regexMatch.Groups [11].Value));
	}
}
