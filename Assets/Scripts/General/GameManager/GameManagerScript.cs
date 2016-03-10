using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManagerScript : MonoBehaviour, IPlayerInstantiationController, IEggInstantiationController, ILevelLoader {
	// Static instance
	public static GameManagerScript 		instance;

	// Prefabs
	private GameObject						_playerPrefab;
	private GameObject						_eggPrefab;

	// Managers
	public GridController					gridController { get; private set; }
	public QualityManager					qualityManager { get; private set; }
	public TimeManager						timeManager { get; private set; }
	public TeamManager						teamManager { get; private set; }
	public InputManager 					inputManager { get; private set; }
	public PlayerManagerScript				playerManager { get; private set; }		
	public ServerCommands					commandsManager { get; private set; }

	// Event
	public delegate void GameOverEventHandler(GameOverEventArgs ev);
	public event GameOverEventHandler OnGameOver;

	void OnEnable()
	{
		instance = this;
		DontDestroyOnLoad (this);
		_playerPrefab = Resources.Load ("Prefab/Player") as GameObject;
		_eggPrefab = Resources.Load ("Prefab/Egg(Teleporter)") as GameObject;

		gridController = GetComponentInChildren<GridScript>().controller;
		qualityManager = new QualityManager();
		timeManager = new TimeManager();
		teamManager = new TeamManager();
		inputManager = new InputManager();
		playerManager = new PlayerManagerScript(gridController, teamManager, this, this);
		commandsManager = new ServerCommands(gridController, teamManager, playerManager, timeManager, this);
	}

	// Prefab instantiation
	public EggController InstantiateEgg ()
	{
		GameObject clone = Instantiate (_eggPrefab);
		return clone.GetComponent<EggScript>().controller;
	}
	
	public PlayerController InstantiatePlayer ()
	{
		GameObject clone = Instantiate (_playerPrefab);
		return clone.GetComponent<PlayerScript>().controller;
	}

	// GameOver event
	public virtual void GameOver(Team team)
	{
		if (OnGameOver != null)
			OnGameOver ( new GameOverEventArgs { team = team });
	}

	// Level Loading
	public void LoadLevel(int x, int y)
	{
		StartCoroutine (AsyncLoadLevel (x, y));
	}
	
	IEnumerator AsyncLoadLevel(int x, int y)
	{
		Debug.Log("Level load start");
		var async = Application.LoadLevelAsync(1);
		SocketManager.instance.wait = true;
		yield return async;
		SocketManager.instance.wait = false;
		SocketManager.instance.StartPingServer ();
		Debug.Log ("Level load end");
		gridController.Init (x, y);
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

public interface ILevelLoader
{
	void LoadLevel(int x, int y);
}

public interface IEggInstantiationController
{
	EggController InstantiateEgg ();
}

public interface IPlayerInstantiationController
{
	PlayerController InstantiatePlayer ();
}