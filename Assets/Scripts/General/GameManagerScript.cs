using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour, IPlayerInstantiationController {
	public static GameManagerScript instance;
	public GameObject				prefab;
	public GridScript				grid { get; private set; }
	public PlayerManagerScript			playerManager { get; private set; }
	public TeamManager				teamManager { get; private set; }
	public IPlayerInstantiationController	pic { get; private set; }

	void OnEnable()
	{
		prefab = Resources.Load ("Prefab/PA_Warrior") as GameObject;
		instance = this;
		grid = GetComponentInChildren<GridScript>();
		teamManager = new TeamManager();
		pic = this;
		playerManager = new PlayerManagerScript(grid.controller, teamManager, pic);
	}

	public PlayerController Instantiate ()
	{
		GameObject clone = Instantiate (prefab);
		return clone.GetComponent<PlayerScript>().controller;
	}
}