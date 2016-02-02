using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
public class TestTeamGUIScript : MonoBehaviour {

	public List<Team> 		teams = new List<Team>() {
		new Team("Pouet", new Color(1.0f, 0.0f, 0.0f, 1.0f)),
		new Team("DOTR", new Color(1.0f, 0.0f, 1.0f, 1.0f)),
		new Team("Trololo", new Color(0.0f, 1.0f, 1.0f, 1.0f)),
		new Team("Bidule", new Color(0.0f, 0.0f, 1.0f, 1.0f)),
		new Team("Machin", new Color(1.0f, 1.0f, 0.0f, 1.0f)),
		new Team("Chouette", new Color(0.0f, 1.0f, 0.0f, 1.0f)),
		new Team("Xirod", new Color(0.5f, 0.0f, 1.0f, 1.0f)),
		new Team("D", new Color(0.0f, 0.5f, 1.0f, 1.0f)),
		new Team(" O", new Color(1.0f, 0.0f, 0.5f, 1.0f)),
		new Team("  T", new Color(1.0f, 0.5f, 0.0f, 1.0f)),
		new Team("   R", new Color(0.5f, 1.0f, 0.0f, 1.0f))
	};

	public TeamListUI		teamList;

	void OnEnable ()
	{
		GameManagerScript.instance.teamManager.teams = teams;
	}

	void Update ()
	{
		Team pouet = teams.Find(x => x.name == "Pouet");
		Team dotr = teams.Find(x => x.name == "DOTR");
		
		List<PlayerController> players = new List<PlayerController>() {
			new PlayerController(1, 1, pouet),
			new PlayerController(2, 1, pouet),
			new PlayerController(3, 1, pouet),
			new PlayerController(4, 1, pouet),
			new PlayerController(5, 1, pouet),
			new PlayerController(6, 1, pouet),
			new PlayerController(7, 1, pouet),
			new PlayerController(8, 1, pouet),
			new PlayerController(9, 1, pouet),
			new PlayerController(10, 1, pouet),
			new PlayerController(11, 1, pouet),
			new PlayerController(12, 1, pouet),
			new PlayerController(1, 1, dotr),
			new PlayerController(2, 1, dotr),
			new PlayerController(3, 1, dotr)
		};

		GameManagerScript.instance.playerManager.players = players;
	}
}
#endif
