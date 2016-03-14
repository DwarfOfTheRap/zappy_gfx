using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DebugManager
{
	public string general_log { get; private set;}

	public Dictionary<int, string> players_log { get; private set;}

	private const int maxNumberOfLines = 200;

	public DebugManager()
	{
		players_log = new Dictionary<int, string>();
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

	public void AddPlayerLog(int index, string str)
	{
		var color = GameManagerScript.instance.playerManager.GetPlayer(index).team.color;
		if (players_log.ContainsKey(index))
			players_log[index] = ConcatLineToLog(players_log[index], ColoredString(str, color));
		else
			players_log.Add (index, ConcatLineToLog("", ColoredString(str, color)));
		AddLog ("<color=magenta>[SERVER]</color> -> " + ColoredString(str, color));
	}

	public void AddLog(string str)
	{
		general_log = ConcatLineToLog (general_log, str);
	}
}








