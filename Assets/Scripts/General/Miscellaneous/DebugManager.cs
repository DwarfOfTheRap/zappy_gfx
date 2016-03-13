using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DebugManager
{
	public string general_log { get; private set;}

	public Dictionary<int, string> players_log { get; private set;}

	public DebugManager()
	{
		players_log = new Dictionary<int, string>();
	}

	public void AddPlayerLog(int index, string str)
	{
		if (players_log.ContainsKey(index))
			players_log[index] += str;
		else
			players_log.Add (index, str);
		AddLog (str);
	}

	public void AddLog(string str)
	{
		general_log += str;
	}
}








