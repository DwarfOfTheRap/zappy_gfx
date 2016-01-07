using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {
	public static GameManagerScript instance;
	public GridScript				grid { get; private set; }
	public PlayerManager			playerManager { get; private set; }
	public TeamManager				teamManager { get; private set; }
	public PlayerInstantiationController	pic { get; private set; }
	
	
	void Awake()
	{
		instance = this;
		grid = GetComponentInChildren<GridScript>();
		teamManager = new TeamManager();
		pic = new PlayerInstantiationController();
		playerManager = new PlayerManager(grid.controller, teamManager, pic);
	}
}