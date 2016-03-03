using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManagerScript : MonoBehaviour, IPlayerInstantiationController, IEggInstantiationController {
	public static GameManagerScript 		instance;
	public GameObject						playerPrefab;
	public GameObject						eggPrefab;
	public GridScript						grid { get; private set; }
	public PlayerManagerScript				playerManager { get; private set; }
	public QualityManager					qualityManager { get; private set; }
	public InputManager 					inputManager { get; private set; }
	public TeamManager						teamManager { get; private set; }
	public TimeManager						timeManager { get; private set; }
	public IPlayerInstantiationController	pic { get; private set; }

	public delegate void GameOverEventHandler(GameOverEventArgs ev);
	public event GameOverEventHandler OnGameOver;

	void OnEnable()
	{
		playerPrefab = Resources.Load ("Prefab/Player") as GameObject;
		eggPrefab = Resources.Load ("Prefab/Egg(Teleporter)") as GameObject;
		instance = this;
		grid = GetComponentInChildren<GridScript>();
		qualityManager = new QualityManager();
		timeManager = new TimeManager();
		teamManager = new TeamManager();
		inputManager = new InputManager();
		playerManager = new PlayerManagerScript(grid.controller, teamManager, this, this);
		GameObject.Find ("Slider").GetComponent<Slider> ().value = 10.0f;
	}

	public void GameOver(Team team)
	{
		if (OnGameOver != null)
			OnGameOver ( new GameOverEventArgs { team = team });
	}

	public void ChangeTimeSpeed(float timeSpeed)
	{
		timeManager.ChangeTimeSpeed (timeSpeed);
	}

	public EggController InstantiateEgg ()
	{
		GameObject clone = Instantiate (eggPrefab);
		return clone.GetComponent<EggScript>().controller;
	}

	public PlayerController InstantiatePlayer ()
	{
		GameObject clone = Instantiate (playerPrefab);
		return clone.GetComponent<PlayerScript>().controller;
	}

	void Update()
	{
		qualityManager.Update ();
		inputManager.CheckInput ();
	}
}

public class GameOverEventArgs : System.EventArgs
{
	public Team team;
}

public interface IEggInstantiationController
{
	EggController InstantiateEgg ();
}

public interface IPlayerInstantiationController
{
	PlayerController InstantiatePlayer ();
}