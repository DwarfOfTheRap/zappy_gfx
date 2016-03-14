using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DebugManager
{
	public string general_log { get; private set;}

	public Dictionary<PlayerController, string> players_log { get; private set;}

	private const int maxNumberOfLines = 200;

	public DebugManager()
	{
		players_log = new Dictionary<PlayerController, string>();
		general_log = "";
	}

	string ConcatLineToLog(string log, string line)
	{
		log += line + System.Environment.NewLine;
		while (log.Split ('\n').Length > maxNumberOfLines)
		{
			var index = log.IndexOf (System.Environment.NewLine);
			log = log.Substring (index + System.Environment.NewLine.Length);
		}
		return log;
	}

	string ColoredString(string str, Color color)
	{
		return "<color=#" + ((int)(color.r * 255.0f)).ToString ("X2") + ((int)(color.g * 255.0f)).ToString ("X2") + ((int)(color.b * 255.0f)).ToString ("X2") + ">" + str + "</color>";
	}

	public void AddPlayerLog(PlayerController player, string str)
	{
		var color = player.team.color;
		if (players_log.ContainsKey(player))
			players_log[player] = ConcatLineToLog(players_log[player], str);
		else
			players_log.Add (player, ConcatLineToLog("", str));
		AddLog ("<color=magenta>[SERVER]</color> -> " + ColoredString(str, color));
	}

	public void AddLog(string str)
	{
		general_log = ConcatLineToLog (general_log, str);
	}
}