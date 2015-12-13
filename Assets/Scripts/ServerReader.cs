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
	private string T = @"[0-9]+";
	private string N = @"\w+";
	private string R = @"[0-1]";
	private string M = "\"[^\"]*\"";
	private string i = @"[0-6]";

	public bool IsPlayerConnectionString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"pnw #" + n + " " + X + " " + Y + " " + O + " " + L + " " + N + "\n$");
	}

	public bool IsGameOverString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"seg " + N + "\n$");
	}

	public bool IsPlayerDeathString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"pdi #" + n + "\n$");
	}

	public bool IsRottenEggString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"edi #" + e + "\n$");
	}

	public bool IsHatchedEggString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"eht #" + e + "\n$");
	}

	public bool IsEndOfForkString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"enw #" + e + " #" + n + " " + X + " " + Y + "\n$");
	}

	public bool IsForkString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"pfk #" + n + "\n$");
	}

	public bool IsBroadcastMessageString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"pbc #" + n + " " + M + "\n$");
	}

	public bool IsIncantationString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"pic " + X + " " + Y + " " + L + "( #" + n + ")+\n$");
	}

	public bool IsPlayerPositionString(string serverMessage)
	{
		return Regex.IsMatch(serverMessage, @"ppo #" + n + " " + X + " " + Y + " " + O + "\n$");
	}
}
