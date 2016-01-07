using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TeamCompletion : MonoBehaviour {

	public Team			team;
	public Text			text;

	string GetTeamCompletion ()
	{
		PlayerController[] players = GameManagerScript.instance.playerManager.GetPlayersInTeam(team);
		float average = 0.0f;
		
		if (players != null) {
			Array.Sort (players, new Comparison<PlayerController> ((x, y) => y.level.CompareTo (x.level)));
			for (int i = 0; i < 6; ++i) {
				average += players [i].level;
			}
			average /= 6;
			average = (average * 100) / 8;
		}
		return (average.ToString () + "%");
	}

	// Use this for initialization
	void Start () {
		text.text = GetTeamCompletion ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = GetTeamCompletion ();
	}
}
