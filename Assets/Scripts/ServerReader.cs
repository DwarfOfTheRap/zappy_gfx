using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class ServerReader : MonoBehaviour {
	private string X = @"[0-9]*";
	private string Y = @"[0-9]*";
	private string q = @"[0-9]*";
	private string n = @"[0-9]*";
	private string O = @"[1-4]";
	private string L = @"[1-8]";
	private string e = @"[0-9]*";
	private string T = @"[0-9]*";
	private string N = @"\w+";
	private string R = @"[0-1]";
	private string M = @"[^\n]*";
	private string i = @"[0-6]";

	public bool IsPlayerConnectionString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"pnw #" + n + " " + X + " " + Y + " " + O + " " + L + " " + N + "\n$");
	}

	public bool IsGameOverString(string serverMessage)
	{
		return Regex.IsMatch (serverMessage, @"seg " + N + "\n$");
	}
}
