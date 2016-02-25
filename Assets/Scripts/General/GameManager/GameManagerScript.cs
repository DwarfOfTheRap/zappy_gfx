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
	public IPlayerInstantiationController	pic { get; private set; }
	public float							timeSpeed;

	void OnEnable()
	{
		playerPrefab = Resources.Load ("Prefab/Ciccio_LOD") as GameObject;
		instance = this;
		grid = GetComponentInChildren<GridScript>();
		qualityManager = new QualityManager();
		teamManager = new TeamManager();
		inputManager = new InputManager();
		playerManager = new PlayerManagerScript(grid.controller, teamManager, this, this);
		timeSpeed = 10.0f;
		GameObject.Find ("Slider").GetComponent<Slider> ().value = 10.0f;
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

	public void ChangeTimeSpeed(float value)
	{
		timeSpeed = value;
	}

	void Update()
	{
		qualityManager.Update ();
		inputManager.CheckInput ();
	}
}

public interface IEggInstantiationController
{
	EggController InstantiateEgg ();
}

public interface IPlayerInstantiationController
{
	PlayerController InstantiatePlayer ();
}