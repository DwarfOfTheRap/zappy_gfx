using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ServerReader {
	private const string X = @"[0-9]+";
	private const string Y = @"[0-9]+";
	private const string q = @"[0-9]+";
	private const string n = @"[0-9]+";
	private const string O = @"[1-4]";
	private const string L = @"[1-8]";
	private const string e = @"[0-9]+";
	private const string T = @"([1-9]|[0-9]{2,})";
	private const string N = @"[\w ]{1,32}";
	private const string R = @"[0-1]";
	private const string M = "[^\n]+";
	private const string i = @"[0-6]";

	private delegate bool methodDelegate(string serverMessage);

	public string[] SplitMessage(string serverMessage)
	{
		return serverMessage.Split ('\n');
	}

	public bool IsLegitMessage(string serverMessage)
	{
		var methodArray = new methodDelegate[] {
			IsMapSizeString,
			IsSquareContentString,
			IsTeamNamesString,
			IsPlayerConnectionString,
			IsPlayerPositionString,
			IsPlayerLevelString,
			IsPlayerInventoryString,
			IsPlayerExpulseString,
			IsPlayerBroadcastString,
			IsPlayerIncantationString,
			IsEndOfIncantationString,
			IsPlayerForkString,
			IsPlayerThrowResourceString,
			IsPlayerTakeResourceString,
			IsPlayerDeathString,
			IsEndOfForkString,
			IsHatchedEggString,
			IsPlayerToEggConnectionString,
			IsRottenEggString,
			IsTimeUnitString,
			IsGameOverString,
			IsServerMessageString,
			IsUnknownCommandString,
			IsWrongParametersString,
			IsWelcomeMessageString
		};

		foreach (methodDelegate md in methodArray)
			if (md(serverMessage))
				return true;
		return false;
	}

	public bool IsMapSizeString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"msz " + X + " " + Y + "$");
	}

	public bool IsSquareContentString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"bct " + X + " " + Y + " " + q + " " + q + " " + q + " " + q + " " + q + " " + q + " " + q + "$");
	}

	public bool IsTeamNamesString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"tna " + N + "$");
	}

	public bool IsPlayerConnectionString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"pnw " + n + " " + X + " " + Y + " " + O + " " + L + " " + N + "$");
	}
	
	public bool IsPlayerPositionString(string serverMessage)
	{
		return Regex.IsMatch(serverMessage, @"ppo " + n + " " + X + " " + Y + " " + O + "$");
	}

	public bool IsPlayerLevelString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"plv " + n + " " + L + "$");
	}

	public bool IsPlayerInventoryString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"pin " + n + " " + X + " " + Y + " " + q + " " + q + " " + q + " " + q + " " + q + " " + q + " " + q + "$");
	}
	
	public bool IsPlayerExpulseString(string serverMessage)
	{
		return Regex.IsMatch(serverMessage, @"pex " + n + "$");
	}
	
	public bool IsPlayerBroadcastString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"pbc " + n + " " + M + "$");
	}

	public bool IsPlayerIncantationString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"pic " + X + " " + Y + " " + L + "( " + n + ")+$");
	}

	public bool IsEndOfIncantationString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"pie " + X + " " + Y + " " + R + "$");
	}

	public bool IsPlayerForkString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"pfk " + n + "$");
	}
	
	public bool IsPlayerThrowResourceString(string serverMessage)
	{
		return Regex.IsMatch(serverMessage, @"pdr " + n + " " + i + "$");
	}

	public bool IsPlayerTakeResourceString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"pgt " + n + " " + i + "$");
	}

	public bool IsPlayerDeathString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"pdi " + n + "$");
	}

	public bool IsEndOfForkString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"enw " + e + " " + n + " " + X + " " + Y + "$");
	}

	public bool IsHatchedEggString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"eht " + e + "$");
	}

	public bool IsPlayerToEggConnectionString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"ebo " + e + "$");
	}

	public bool IsRottenEggString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"edi " + e + "$");
	}

	public bool	IsTimeUnitString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"sgt " + T + "$");
	}

	public bool IsGameOverString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"seg " + N + "$");
	}

	public bool IsServerMessageString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"smg " + M + "$");
	}

	public bool IsWelcomeMessageString(string serverMessage)
	{
		return serverMessage == "BIENVENUE";
	}

	public bool IsUnknownCommandString(string serverMessage)
	{
		return serverMessage == "suc";
	}

	public bool IsWrongParametersString(string serverMessage)
	{
		return serverMessage == "sbp";
	}
}
