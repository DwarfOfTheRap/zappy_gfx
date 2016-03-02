using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
	private const string M		= @"([^\n]+)";
	private const string R		= @"([0-1])";
	private const string i 		= @"([0-6])";
	private const string e		= @"(#[0-9]+)";
	private const string T		= @"([1-9]|[0-9]{2,})";
	
	delegate void methodDelegate(string serverMessage);

	private static Dictionary<string, methodDelegate> methodDictionary = new Dictionary<string, methodDelegate> ()
	{
		{"msz", new methodDelegate (SendMapSize)},
		{"bct", new methodDelegate (SendSquareContent)},
		{"tna", new methodDelegate (SendTeamName)},
		{"pnw", new methodDelegate (SendPlayerConnection)},
		{"ppo", new methodDelegate (SendPlayerPosition)},
		{"plv",	new methodDelegate (SendPlayerLevel)},
		{"pin", new methodDelegate (SendPlayerInventory)},
		{"pex", new methodDelegate (SendPlayerExpulsion)},
		{"pbc", new methodDelegate (SendBroadcast)},
		{"pic", new methodDelegate (SendIncantationStart)},
		{"pie", new methodDelegate (SendIncantationStop)},
		{"pfk", new methodDelegate (SendLayEgg)},
		{"pdr", new methodDelegate (SendThrowResource)},
		{"pgt", new methodDelegate (SendTakeResource)},
		{"pdi", new methodDelegate (SendDeath)},
		{"enw", new methodDelegate (SendEndOfFork)},
		{"eht", new methodDelegate (SendHatchedEgg)},
		{"ebo", new methodDelegate (SendPlayerToEggConnection)},
		{"edi", new methodDelegate (SendRottenEgg)},
		{"sgt", new methodDelegate (SendTimeUnit)},
		{"seg", new methodDelegate (SendGameOver)},
		{"smg", new methodDelegate (SendServerMessage)},
		{"suc", new methodDelegate (SendUnknownCommand)},
		{"sbp", new methodDelegate (SendWrongParameters)}
	};

	public static void PickMethod(string serverMessage)
	{
		Match regexMatch;

		regexMatch = Regex.Match (serverMessage, cmd);
		methodDictionary [regexMatch.Groups [1].Value] (serverMessage);
	}

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

	public static void SendPlayerExpulsion(string serverMessage)
	{
		Match regexMatch;

		regexMatch = Regex.Match (serverMessage, cmd + " " + n + "\n$");
		GameManagerScript.instance.playerManager.SetPlayerExpulse (int.Parse (regexMatch.Groups [2].Value));
	}

	public static void SendBroadcast(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + n + " " + M + "\n$");
		GameManagerScript.instance.playerManager.SetPlayerBroadcast (int.Parse (regexMatch.Groups [2].Value), regexMatch.Groups [3].Value);
	}

	public static void SendIncantationStart(string serverMessage)
	{
		Match regexMatch;
		string secondaryPlayers = @"(( #[0-9]+)+)";
		string[] split;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + X + " " + Y + " " + L + " " + n + secondaryPlayers + "\n$");
		split = regexMatch.Groups [6].Value.Split (' ');
		GameManagerScript.instance.playerManager.SetPlayerIncantatePrimary (int.Parse (regexMatch.Groups [5].Value));
		foreach (string player in split) {
			GameManagerScript.instance.playerManager.SetPlayerIncantateSecondary (int.Parse (player.Substring(1)));
		}
	}

	public static void SendIncantationStop(string serverMessage)
	{
		Match regexMatch;
		int incantationResult;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + X + " " + Y + " " + R + "\n$");
		GameManagerScript.instance.playerManager.SetPlayersStopIncantate (int.Parse (regexMatch.Groups [2].Value), int.Parse (regexMatch.Groups [3].Value));
		incantationResult = int.Parse (regexMatch.Groups [4].Value);
	}

	public static void SendLayEgg(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + n + "\n$");
		GameManagerScript.instance.playerManager.SetPlayerLayEgg (int.Parse (regexMatch.Groups [2].Value));
	}

	public static void SendThrowResource(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + n + " " + i + "\n$");
		GameManagerScript.instance.playerManager.SetPlayerThrowResource (int.Parse (regexMatch.Groups [2].Value), int.Parse (regexMatch.Groups [3].Value));
	}

	public static void SendTakeResource(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + n + " " + i + "\n$");
		GameManagerScript.instance.playerManager.SetPlayerTakeResource (int.Parse (regexMatch.Groups [2].Value), int.Parse(regexMatch.Groups [3].Value));
	}

	public static void SendDeath(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + n + "\n$");
		GameManagerScript.instance.playerManager.SetPlayerDeath (int.Parse (regexMatch.Groups [2].Value));
	}

	public static void SendEndOfFork(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + e + " " + n + " " + X + " " + Y + "\n$");
		GameManagerScript.instance.playerManager.SetEggCreation (int.Parse (regexMatch.Groups [2].Value),
		                                                        int.Parse (regexMatch.Groups [3].Value),
		                                                        int.Parse (regexMatch.Groups [4].Value),
		                                                        int.Parse (regexMatch.Groups [5].Value));
	}

	public static void SendHatchedEgg(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + e + "\n$");
		GameManagerScript.instance.playerManager.SetEggHatch (int.Parse (regexMatch.Groups[2].Value));
	}

	public static void SendPlayerToEggConnection(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + e + "\n$");
		GameManagerScript.instance.playerManager.SetPlayerToEggConnection (int.Parse (regexMatch.Groups[2].Value));
	}

	public static void SendRottenEgg(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + e + "\n$");
		GameManagerScript.instance.playerManager.SetEggDie (int.Parse (regexMatch.Groups[2].Value));
	}

	public static void SendTimeUnit(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + T + "\n$");
		GameManagerScript.instance.timeManager.ChangeTimeSpeed (float.Parse(regexMatch.Groups[2].Value));
	}

	public static void SendGameOver(string serverMessage)
	{
		Match regexMatch;
		Team team;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + N + "\n$");
		team = GameManagerScript.instance.teamManager.findTeam (regexMatch.Groups [2].Value);
		GameManagerScript.instance.GameOver(team);
	}

	public static void SendServerMessage(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + M + "\n$");
		Debug.Log (regexMatch.Groups [2].Value);
	}

	public static void SendUnknownCommand(string serverMessage)
	{
		Debug.Log ("Server sends: Unknown Command");
	}

	public static void SendWrongParameters(string serverMessage)
	{
		Debug.Log ("Server sends: Wrong Parameters");
	}
}
