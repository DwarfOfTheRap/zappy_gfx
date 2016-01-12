using UnityEngine;
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

	void Update ()
	{
		Team pouet = teams.Find(x => x.name == "Pouet");
		Team dotr = teams.Find(x => x.name == "DOTR");
		
		List<PlayerController> players = new List<PlayerController>() {
			new PlayerController() { index = 1, level = 1, team = pouet },
			new PlayerController() { index = 2, level = 1, team = pouet },
			new PlayerController() { index = 3, level = 1, team = pouet },
			new PlayerController() { index = 4, level = 1, team = pouet },
			new PlayerController() { index = 5, level = 1, team = pouet },
			new PlayerController() { index = 6, level = 1, team = pouet },
			new PlayerController() { index = 7, level = 1, team = pouet },
			new PlayerController() { index = 8, level = 1, team = pouet },
			new PlayerController() { index = 9, level = 1, team = pouet },
			new PlayerController() { index = 10, level = 1, team = pouet },
			new PlayerController() { index = 11, level = 1, team = pouet },
			new PlayerController() { index = 12, level = 1, team = pouet },
			new PlayerController() { index = 1, level = 1, team = dotr },
			new PlayerController() { index = 2, level = 1, team = dotr },
			new PlayerController() { index = 3, level = 1, team = dotr }
		};

		GameManagerScript.instance.playerManager.players = players;
	}
}
