using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ServerCommands {

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

	private Dictionary<string, methodDelegate> methodDictionary;

	public GridController gridController;
	public TeamManager teamManager;
	public PlayerManagerScript playerManager;
	public TimeManager timeManager;

	ServerCommands () {}

	public ServerCommands(GridController gridController, TeamManager teamManager, PlayerManagerScript playerManager, TimeManager timeManager)
	{
		this.gridController = gridController;
		this.teamManager = teamManager;
		this.playerManager = playerManager;
		this.timeManager = timeManager;

		methodDictionary = new Dictionary<string, methodDelegate> ()
		{
			{"msz", SendMapSize},
			{"bct", SendSquareContent},
			{"tna", SendTeamName},
			{"pnw", SendPlayerConnection},
			{"ppo", SendPlayerPosition},
			{"plv",	SendPlayerLevel},
			{"pin", SendPlayerInventory},
			{"pex", SendPlayerExpulsion},
			{"pbc", SendBroadcast},
			{"pic", SendIncantationStart},
			{"pie", SendIncantationStop},
			{"pfk", SendLayEgg},
			{"pdr", SendThrowResource},
			{"pgt", SendTakeResource},
			{"pdi", SendDeath},
			{"enw", SendEndOfFork},
			{"eht", SendHatchedEgg},
			{"ebo", SendPlayerToEggConnection},
			{"edi", SendRottenEgg},
			{"sgt", SendTimeUnit},
			{"seg", SendGameOver},
			{"smg", SendServerMessage},
			{"suc", SendUnknownCommand},
			{"sbp", SendWrongParameters}
		};
	}

	public void PickMethod(string serverMessage)
	{
		Match regexMatch;

		regexMatch = Regex.Match (serverMessage, cmd);
		methodDictionary [regexMatch.Groups [1].Value] (serverMessage);
	}

	public void SendMapSize(string serverMessage)
	{
		Match regexMatch;

		regexMatch = Regex.Match (serverMessage, cmd + " " + X + " " + Y + "\n$");
		gridController.Init (int.Parse (regexMatch.Groups[2].Value), int.Parse (regexMatch.Groups[3].Value));
	}

	public void SendSquareContent(string serverMessage)
	{
		Match regexMatch;
		ISquare square;

		regexMatch = Regex.Match (serverMessage, cmd + " " + X + " " + Y + " " + q + " " + q + " " + q + " " + q + " " + q + " " + q + " " + q + "\n$");
		square = gridController.GetSquare (int.Parse (regexMatch.Groups [2].Value), int.Parse (regexMatch.Groups [3].Value));
		square.SetResources (uint.Parse (regexMatch.Groups [4].Value),
		                    uint.Parse (regexMatch.Groups [5].Value),
		                    uint.Parse (regexMatch.Groups [6].Value),
		                    uint.Parse (regexMatch.Groups [7].Value),
		                    uint.Parse (regexMatch.Groups [8].Value),
		                    uint.Parse (regexMatch.Groups [9].Value),
		                    uint.Parse (regexMatch.Groups [10].Value));
	}

	public void SendTeamName(string serverMessage)
	{
		Match regexMatch;

		regexMatch = Regex.Match (serverMessage, cmd + " " + N + "\n$");
		teamManager.createTeam (regexMatch.Groups [2].Value);
	}

	public void SendPlayerConnection(string serverMessage)
	{
		Match regexMatch;

		regexMatch = Regex.Match (serverMessage, cmd + " " + n + " " + X + " " + Y + " " + O + " " + L + " " + N + "\n$");
		playerManager.SetPlayerConnection (int.Parse (regexMatch.Groups[2].Value.Substring(1)),
		                                                             int.Parse (regexMatch.Groups[3].Value),
		          													 int.Parse (regexMatch.Groups[4].Value),
		                                                             (Orientation)int.Parse (regexMatch.Groups[5].Value),
		                                                             int.Parse (regexMatch.Groups[6].Value),
		                                                             regexMatch.Groups[7].Value);
	}

	public void SendPlayerPosition(string serverMessage)
	{
		Match regexMatch;

		regexMatch = Regex.Match (serverMessage, cmd + " " + n + " " + X + " " + Y + " " + O + "\n$");
		playerManager.SetPlayerPosition (int.Parse (regexMatch.Groups [2].Value.Substring(1)),
		                                                           int.Parse (regexMatch.Groups [3].Value),
		                                                           int.Parse (regexMatch.Groups [4].Value),
		                                                           (Orientation)int.Parse (regexMatch.Groups [5].Value));
	}

	public void SendPlayerLevel(string serverMessage)
	{
		Match regexMatch;

		regexMatch = Regex.Match (serverMessage, cmd + " " + n + " " + L + "\n$");
		playerManager.SetPlayerLevel (int.Parse (regexMatch.Groups [2].Value.Substring(1)),
		                                                         int.Parse (regexMatch.Groups [3].Value));
	}

	public void SendPlayerInventory(string serverMessage)
	{
		Match regexMatch;

		regexMatch = Regex.Match (serverMessage, cmd + " " + n + " " + X + " " + Y + " " + q + " " + q + " " + q + " " + q + " " + q + " " + q + " " + q + "\n$");
		playerManager.SetPlayerInventory (int.Parse (regexMatch.Groups [2].Value.Substring(1)),
		                                                             int.Parse (regexMatch.Groups [5].Value),
		                                                             int.Parse (regexMatch.Groups [6].Value),
		                                                             int.Parse (regexMatch.Groups [7].Value),
		                                                             int.Parse (regexMatch.Groups [8].Value),
		                                                             int.Parse (regexMatch.Groups [9].Value),
		                                                             int.Parse (regexMatch.Groups [10].Value),
		                                                             int.Parse (regexMatch.Groups [11].Value));
	}

	public void SendPlayerExpulsion(string serverMessage)
	{
		Match regexMatch;

		regexMatch = Regex.Match (serverMessage, cmd + " " + n + "\n$");
		playerManager.SetPlayerExpulse (int.Parse (regexMatch.Groups [2].Value.Substring(1)));
	}

	public void SendBroadcast(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + n + " " + M + "\n$");
		playerManager.SetPlayerBroadcast (int.Parse (regexMatch.Groups [2].Value.Substring(1)), regexMatch.Groups [3].Value);
	}

	public void SendIncantationStart(string serverMessage)
	{
		Match regexMatch;
		string secondaryPlayers = @"(( #[0-9]+)+)";
		string[] split;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + X + " " + Y + " " + L + " " + n + secondaryPlayers + "\n$");
		split = regexMatch.Groups [6].Value.Split (' ');
		playerManager.SetPlayerIncantatePrimary (int.Parse (regexMatch.Groups [5].Value.Substring(1)));
		foreach (string player in split) {
			if (player != "")
				playerManager.SetPlayerIncantateSecondary (int.Parse (player.Substring(1)));
		}
	}

	public void SendIncantationStop(string serverMessage)
	{
		Match regexMatch;
		int incantationResult;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + X + " " + Y + " " + R + "\n$");
		playerManager.SetPlayersStopIncantate (int.Parse (regexMatch.Groups [2].Value), int.Parse (regexMatch.Groups [3].Value));
		incantationResult = int.Parse (regexMatch.Groups [4].Value);
	}

	public void SendLayEgg(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + n + "\n$");
		playerManager.SetPlayerLayEgg (int.Parse (regexMatch.Groups [2].Value.Substring(1)));
	}

	public void SendThrowResource(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + n + " " + i + "\n$");
		playerManager.SetPlayerThrowResource (int.Parse (regexMatch.Groups [2].Value.Substring(1)), int.Parse (regexMatch.Groups [3].Value));
	}

	public void SendTakeResource(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + n + " " + i + "\n$");
		playerManager.SetPlayerTakeResource (int.Parse (regexMatch.Groups [2].Value.Substring(1)), int.Parse(regexMatch.Groups [3].Value));
	}

	public void SendDeath(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + n + "\n$");
		playerManager.SetPlayerDeath (int.Parse (regexMatch.Groups [2].Value.Substring(1)));
	}

	public void SendEndOfFork(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + e + " " + n + " " + X + " " + Y + "\n$");
		playerManager.SetEggCreation (int.Parse (regexMatch.Groups [2].Value.Substring(1)),
		                              int.Parse (regexMatch.Groups [3].Value.Substring(1)),
		                                                        int.Parse (regexMatch.Groups [4].Value),
		                                                        int.Parse (regexMatch.Groups [5].Value));
	}

	public void SendHatchedEgg(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + e + "\n$");
		playerManager.SetEggHatch (int.Parse (regexMatch.Groups[2].Value.Substring(1)));
	}

	public void SendPlayerToEggConnection(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + e + "\n$");
		playerManager.SetPlayerToEggConnection (int.Parse (regexMatch.Groups[2].Value.Substring(1)));
	}

	public void SendRottenEgg(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + e + "\n$");
		playerManager.SetEggDie (int.Parse (regexMatch.Groups[2].Value.Substring(1)));
	}

	public void SendTimeUnit(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + T + "\n$");
		timeManager.ChangeTimeSpeed (float.Parse(regexMatch.Groups[2].Value));
	}

	public void SendGameOver(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + N + "\n$");
		GameManagerScript.instance.GameOver(teamManager.findTeam (regexMatch.Groups [2].Value));
	}

	public void SendServerMessage(string serverMessage)
	{
		Match regexMatch;
		
		regexMatch = Regex.Match (serverMessage, cmd + " " + M + "\n$");
		Debug.Log (regexMatch.Groups [2].Value);
	}

	public void SendUnknownCommand(string serverMessage)
	{
		Debug.Log ("Server sends: Unknown Command");
	}

	public void SendWrongParameters(string serverMessage)
	{
		Debug.Log ("Server sends: Wrong Parameters");
	}
}
