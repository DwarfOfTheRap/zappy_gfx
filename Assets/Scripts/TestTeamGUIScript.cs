﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestTeamGUIScript : MonoBehaviour {

	public List<Team> 		teams = new List<Team>() {
		new Team() { name = "Pouet", color = new Color(1.0f, 0.0f, 0.0f, 1.0f) },
		new Team() { name = "DOTR", color = new Color(1.0f, 0.0f, 1.0f, 1.0f) },
		new Team() { name = "Trololo", color = new Color(0.0f, 1.0f, 1.0f, 1.0f) },
		new Team() { name = "Bidule", color = new Color(0.0f, 0.0f, 1.0f, 1.0f) },
		new Team() { name = "Machin", color = new Color(1.0f, 1.0f, 0.0f, 1.0f) },
		new Team() { name = "Chouette", color = new Color(0.0f, 1.0f, 0.0f, 1.0f) },
		new Team() { name = "Xirod", color = new Color(0.5f, 0.0f, 1.0f, 1.0f) },
		new Team() { name = "D", color = new Color(0.0f, 0.5f, 1.0f, 1.0f) },
		new Team() { name = " O", color = new Color(1.0f, 0.0f, 0.5f, 1.0f) },
		new Team() { name = "  T", color = new Color(1.0f, 0.5f, 0.0f, 1.0f) },
		new Team() { name = "   R", color = new Color(0.5f, 1.0f, 0.0f, 1.0f) }
	};

	public TeamsInfo		teamsInfo;

	void OnEnable ()
	{
		teamsInfo.teams = teams;
	}
}