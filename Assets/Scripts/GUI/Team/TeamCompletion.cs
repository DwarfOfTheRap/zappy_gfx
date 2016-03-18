using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TeamCompletion : MonoBehaviour {

	public Team			team;
	public Text			text;
	public Text			CountText;

	string GetTeamCompletion ()
	{
		PlayerController[] players = GameManagerScript.instance.playerManager.GetPlayersInTeam(team);
		float average = 0.0f;
		
		if (players != null) {
			CountText.text = players.Length.ToString ();
			Array.Sort (players, new Comparison<PlayerController> ((x, y) => y.level.CompareTo (x.level)));
			for (int i = 0; i < players.Length && i < 6; ++i) {
				average += players [i].level;
			}
			average /= 6;
			average = (average * 100) / 8;
		}
		else
			CountText.text = "";
		return (average.ToString ("0.0") + "%");
	}

	void Update () {
		text.text = GetTeamCompletion ();
	}
}
