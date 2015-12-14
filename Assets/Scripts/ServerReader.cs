using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class ServerReader : MonoBehaviour {
	private string X = @"[0-9]+";
	private string Y = @"[0-9]+";
	private string q = @"[0-9]+";
	private string n = @"[0-9]+";
	private string O = @"[1-4]";
	private string L = @"[1-8]";
	private string e = @"[0-9]+";
	private string T = @"([1-9]|[0-9]{2,})";
	private string N = @"\w+";
	private string R = @"[0-1]";
	private string M = "\"[^\"]*\"";
	private string i = @"[0-6]";

	public bool IsMapSizeString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"msz " + X + " " + Y + "\n$");
	}

	public bool IsSquareContentString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"bct " + X + " " + Y + " " + q + " " + q + " " + q + " " + q + " " + q + " " + q + " " + q + "\n$");
	}

	public bool IsTeamNamesString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"tna " + N + "\n$");
	}

	public bool IsPlayerConnectionString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"pnw #" + n + " " + X + " " + Y + " " + O + " " + L + " " + N + "\n$");
	}
	
	public bool IsPlayerPositionString(string serverMessage)
	{
		return Regex.IsMatch(serverMessage, @"ppo #" + n + " " + X + " " + Y + " " + O + "\n$");
	}

	public bool IsPlayerLevelString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"plv #" + n + " " + L + "\n$");
	}

	public bool IsPlayerInventoryString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"pin #" + n + " " + X + " " + Y + " " + q + " " + q + " " + q + " " + q + " " + q + " " + q + " " + q + "\n$");
	}
	
	public bool IsPlayerExpulseString(string serverMessage)
	{
		return Regex.IsMatch(serverMessage, @"pex #" + n + "\n$");
	}
	
	public bool IsPlayerBroadcastString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"pbc #" + n + " " + M + "\n$");
	}

	public bool IsPlayerIncantationString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"pic " + X + " " + Y + " " + L + "( #" + n + ")+\n$");
	}

	public bool IsEndOfIncantationString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"pie " + X + " " + Y + " " + R + "\n$");
	}

	public bool IsPlayerForkString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"pfk #" + n + "\n$");
	}
	
	public bool IsPlayerThrowResourceString(string serverMessage)
	{
		return Regex.IsMatch(serverMessage, @"pdr #" + n + " " + i + "\n$");
	}

	public bool IsPlayerTakeResourceString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"pgt #" + n + " " + i + "\n$");
	}

	public bool IsPlayerDeathString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"pdi #" + n + "\n$");
	}

	public bool IsEndOfForkString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"enw #" + e + " #" + n + " " + X + " " + Y + "\n$");
	}

	public bool IsHatchedEggString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"eht #" + e + "\n$");
	}

	public bool IsPlayerToEggConnectionString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"ebo #" + e + "\n$");
	}

	public bool IsRottenEggString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"edi #" + e + "\n$");
	}

	public bool	IsTimeUnitString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"sgt " + T + "\n$");
	}

	public bool IsGameOverString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"seg " + N + "\n$");
	}

	public bool IsServerMessageString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"smg " + M + "\n$");
	}

	public bool IsUnknownCommandString(string serverMessage)
	{
		return serverMessage == "suc\n";
	}

	public bool IsWrongParametersString(string serverMessage)
	{
		return serverMessage == "sbp\n";
	}
}
