using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {
	public static GameManagerScript instance;

	public GridScript				grid { get; private set; }
	public InputManager 			inputManager { get; private set; }
	public PlayerManagerScript			playerManager { get; private set; }
	public TeamManager				teamManager { get; private set; }
	
	
	void Awake()
	{
		instance = this;
		grid = GetComponentInChildren<GridScript>();
		inputManager = new InputManager();
		teamManager = new TeamManager();
		playerManager = new PlayerManagerScript(grid.controller, teamManager);
	}

	void Update()
	{
		inputManager.CheckInput ();
	}
}