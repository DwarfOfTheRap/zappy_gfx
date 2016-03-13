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
		general_log = "";
	}

	string ColoredString(string str, Color color)
	{
		return "<color=#" + ((int)(color.r * 255.0f)).ToString ("X2") + ((int)(color.g * 255.0f)).ToString ("X2") + ((int)(color.b * 255.0f)).ToString ("X2") + ">" + str + "</color>";
	}

	public void AddPlayerLog(int index, string str)
	{
		var color = GameManagerScript.instance.playerManager.GetPlayer(index).team.color;
		if (players_log.ContainsKey(index))
			players_log[index] += "\n" + ColoredString(str, color);
		else
			players_log.Add (index, ColoredString(str, color));
		AddLog ("<color=magenta>[SERVER]</color> -> " + ColoredString(str, color));
	}

	public void AddLog(string str)
	{
		general_log += str + "\n";
	}
}








