using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManagerScript : MonoBehaviour, IPlayerInstantiationController, IEggInstantiationController, IIncantationInstantiationController, ILevelLoader {
	// Static instance
	public static GameManagerScript 		instance;

	// Prefabs
	private GameObject						_playerPrefab;
	private GameObject						_eggPrefab;
	private GameObject						_incantationPrefab;

	// Managers
	public GridController					gridController { get; private set; }
	public QualityManager					qualityManager { get; private set; }
	public TimeManager						timeManager { get; private set; }
	public TeamManager						teamManager { get; private set; }
	public InputManager 					inputManager { get; private set; }
	public PlayerManager					playerManager { get; private set; }
	public DebugManager						debugManager { get; private set; }	
	public ServerCommands					commandsManager { get; private set; }

	// Event
	public delegate void GameOverEventHandler(GameOverEventArgs ev);
	public delegate void LevelLoadHandler();
	public event GameOverEventHandler OnGameOver;
	public event LevelLoadHandler OnLevelLoad;

	void OnEnable()
	{
		instance = this;
		DontDestroyOnLoad (this);
		_playerPrefab = Resources.Load ("Prefab/Player") as GameObject;
		_eggPrefab = Resources.Load ("Prefab/Egg(Teleporter)") as GameObject;
		_incantationPrefab = Resources.Load ("Prefab/Incantation") as GameObject;

		gridController = GetComponentInChildren<GridScript>().controller;
		qualityManager = new QualityManager();
		timeManager = new TimeManager();
		teamManager = new TeamManager();
		inputManager = new InputManager();
		playerManager = new PlayerManager(gridController, teamManager, this, this);
		debugManager =  new DebugManager();
		commandsManager = new ServerCommands(gridController, teamManager, playerManager, timeManager, debugManager, this);
	}

	// Prefab instantiation
	public EggController InstantiateEgg ()
	{
		var clone = Instantiate (_eggPrefab);
		return clone.GetComponent<EggScript>().controller;
	}
	
	public PlayerController InstantiatePlayer ()
	{
		var clone = Instantiate (_playerPrefab);
		return clone.GetComponent<PlayerScript>().controller;
	}

	public IncantationScript InstantiateIncantation (Vector3 position, Color color)
	{
		var clone = Instantiate (_incantationPrefab).GetComponent<IncantationScript>();
		clone.transform.position = position;
		clone.startColor = color;
		return clone;
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
		gridController.Init (x, y);
		var async = Application.LoadLevelAsync(1);
		if (OnLevelLoad != null)
			OnLevelLoad();
		SocketManager.instance.wait = true;
		async.allowSceneActivation = false;
		while (!async.isDone)
		{
			Debug.Log (gridController.GetInitProgress ());
			yield return null;
			if (async.progress == 0.9f && gridController.GetInitProgress () == 1.0f)
				async.allowSceneActivation = true;
		}
		SocketManager.instance.wait = false;
		SocketManager.instance.StartPingServer ();
	}

	void Update()
	{
		if (Application.loadedLevel == 0 && SocketManager.instance.wait == false && SocketManager.instance.connected == true && inputManager.MenuKey())
			SocketManager.instance.CloseSocket ();
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

public interface IIncantationInstantiationController
{
	IncantationScript InstantiateIncantation(Vector3 position, Color color);
}