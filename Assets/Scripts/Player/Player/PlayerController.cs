using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;

[Serializable]
public class PlayerController {
	// Game Attributes
	public class PlayerInventory
	{
		public int 						linemate = 0;
		public int						deraumere = 0;
		public int						sibur = 0;
		public int						mendiane = 0;
		public int						phiras = 0;
		public int						thystame = 0;
		public int						nourriture = 0;
	}						   	
	public PlayerInventory				inventory = new PlayerInventory();
	public int							level;
	public int							index { get; private set;}
	public Team							team { get; private set;}
	public Orientation					playerOrientation { get; private set; }

	// State
	public bool 						isIncantating { get; private set; }
	public bool							dead { get; private set; }
	public Vector3						destination { get; private set; }
	public bool							expulsed { get; private set; }

	// Speed
	public float						speed = 0.3597f * 2.5f;
	public float						rotSpeed = 6.38f * 2.25f;

	// Player position + orientation
	public	ISquare						oldSquare { get; private set; }
	public	ISquare						currentSquare;
	private Orientation					_oldOrientation;
	public  Quaternion					rotation { get; private set; }
	private int							_subPositionIndex;

	// Vision + Highlight	
	private bool						_highlighted = false;
	private ISquare[]					_squareVision = new ISquare[0];
									
	// Teleport Attributes
	public Vector3						teleportDestination;
	public bool							dontTeleportMe { get; set; }

	// Time Attributes
	private float						_timeSinceLastInventoryUpdate;
	
	// Controller
	public IPlayerMotorController		playerMotorController { get; private set; }
	private IAnimatorController			_animatorController;
	private GridController				_gridController;
	private DebugManager				_debugManager;

	// Managers
	private AInputManager				_inputManager;
	private TimeManager					_timeManager;

	public delegate void				OnRefreshHandler();
	public event OnRefreshHandler		OnRefresh;
		
#if UNITY_EDITOR
	public PlayerController()
	{
	}

	public PlayerController(int index, int level, Team team)
	{
		this.index = index;
		this.level = level;
		this.team = team;
	}
#endif


	// Setters
	public void SetAnimatorController(IAnimatorController animatorController)
	{
		this._animatorController = animatorController;
	}
	
	public void SetPlayerMovementController(IPlayerMotorController playerMovementController)
	{
		this.playerMotorController = playerMovementController;
	}

	public void SetInputManager (AInputManager inputManager)
	{
		this._inputManager = inputManager;
		inputManager.OnLeftClicking += OnLeftClick;
		inputManager.OnRightClicking += OnRightClick;
	}

	public void SetTimeManager (TimeManager timeManager)
	{
		this._timeManager = timeManager;
	}

	public void SetGridController (GridController gridController)
	{
		this._gridController = gridController;
	}

	public void SetDebugManager (DebugManager debugManager)
	{
		this._debugManager = debugManager;
	}

	// Animations
	public void IncantatePrimary()
	{
		_animatorController.SetBool ("Incantate", true);
	}

	public void IncantateSecondary ()
	{
		_animatorController.SetBool ("Incantate", true);
	}
	
	public void StopIncantating()
	{
		_animatorController.SetBool("Incantate", false);
	}

	public void LayEgg()
	{
		CorrectPlayerState();
		_animatorController.SetBool ("LayEgg", true);
	}

	public void StopLayingEgg()
	{
		_animatorController.SetBool ("LayEgg", false);
	}

	public void GrabItem()
	{
		CorrectPlayerState();
		_animatorController.SetTrigger ("Grab");
	}

	public void ThrowItem()
	{
		CorrectPlayerState();
		_animatorController.SetTrigger ("Grab");
	}

	public void Die()
	{
		CorrectPlayerState ();
		dead = true;
		_animatorController.SetTrigger ("Death");
	}

	// Actions
	public void Broadcast (string message)
	{
		CorrectPlayerState();
		playerMotorController.Broadcast (message);
		var colorhex = string.Format("#{0}{1}{2}", ((int)(team.color.r * 255.0f)).ToString("X2"), ((int)(team.color.g * 255)).ToString("X2"), ((int)(team.color.b * 255)).ToString("X2"));
		_debugManager.AddPlayerLog (this, "<color=" + colorhex + "><size=18>[" + message + "]</size></color>");
	}
	
	public void Expulse()
	{
		CorrectPlayerState();
		_animatorController.SetTrigger ("Expulse");
		foreach (PlayerController player in GameManagerScript.instance.playerManager.GetPlayersInSquare(this.oldSquare))
			player.BeExpulsed (OrientationManager.Opposite (playerOrientation));
	}

	public void BeExpulsed(Orientation direction)
	{
		expulsed = true;
		playerMotorController.Expulsed (direction);
	}

	public void StopExpulsion()
	{
		expulsed = false;
		playerMotorController.StopExpulsion();
	}

	public PlayerController Init(int x, int y, Orientation orientation, int level, int index, Team team, GridController gridController)
	{
		try
		{
			_subPositionIndex = UnityEngine.Random.Range (0, 9);
			ISquare square = gridController.GetSquare (x, y);
			SetDestination(square, gridController);
			playerMotorController.SetPosition(square.GetSubPosition(_subPositionIndex));
			this.level = level;
			this.index = index;
			this.team = team;
			this.SetPlayerOrientation (orientation);
			playerMotorController.SetRotation (OrientationManager.GetRotation (orientation));
			playerMotorController.SetTeamColor (team.color);
			return this;
		}
		catch (GridController.GridOutOfBoundsException)
		{
			return null;
		}
	}

	public void SetDestination(ISquare square, GridController gridController)
	{
		CorrectPlayerState ();
		this.oldSquare = (this.oldSquare != null) ? this.currentSquare : null;
		if (this.currentSquare != square)
		{
			Vector3 distance = currentSquare != null ? square.GetPosition () - currentSquare.GetPosition () : Vector3.zero;
			if (gridController != null && (Mathf.Abs (distance.x) > (gridController.width * square.GetBoundX ()) / 2.0f || Mathf.Abs (distance.z) > (gridController.height * square.GetBoundZ ()) / 2.0f))
				teleportDestination = gridController.GetNearestTeleport(distance, destination);
			destination = playerMotorController.SetDestination (square.GetSubPosition (_subPositionIndex));
		}
		currentSquare = square;
	}

	public void SetPosition(int x, int y, GridController gridController)
	{
		try
		{
			ISquare square = gridController.GetSquare (x, y);
			SetDestination (square, gridController);
		}
		catch (GridController.GridOutOfBoundsException)
		{
			return ;
		}
	}

	public void SetPlayerOrientation(Orientation playerOrientation)
	{
		CorrectPlayerState ();
		this._oldOrientation = this.playerOrientation;
		this.playerOrientation = playerOrientation;
		this.rotation = OrientationManager.GetRotation(playerOrientation);
	}

	public void SetInventory (int nourriture, int linemate, int deraumere, int sibur, int mendiane, int phiras, int thystame)
	{
		_timeSinceLastInventoryUpdate = Time.realtimeSinceStartup;
		this.inventory.nourriture = nourriture;
		this.inventory.linemate = linemate;
		this.inventory.deraumere = deraumere;
		this.inventory.sibur = sibur;
		this.inventory.mendiane = mendiane;
		this.inventory.phiras = phiras;
		this.inventory.thystame = thystame;
	}

	void RefreshInventory()
	{
		new ServerQuery().SendPlayerInventoryQuery(this.index);
		if (OnRefresh != null)
			OnRefresh();
	}
	
	public void GoToDestination(Orientation animationOrientation)
	{
		_animatorController.SetBool ("Walk", playerMotorController.IsMoving(this.destination, this.rotation) && !expulsed);
		_animatorController.SetInteger ("Orientation", (int)animationOrientation);
		if (teleportDestination != Vector3.zero)
			playerMotorController.MoveToDestination (teleportDestination, speed);
		else
			playerMotorController.MoveToDestination (destination, speed);
		playerMotorController.MoveToRotation(rotation, rotSpeed);
	}

	public void EnableHighlight()
	{
		_highlighted = true;
		playerMotorController.EnableHighlight (this.team.color);
	}

	public void DisableHighlight()
	{
		if (_highlighted)
			foreach (ISquare square in _squareVision)
				square.Standard ();
		_highlighted = false;
		playerMotorController.DisableHighlight ();

	}

	void OnLeftClick(ClickEventArgs args)
	{
		if (args.target.IsPlayer () && args.target == (IClickTarget)playerMotorController)
			EnableHighlight ();
		else
			DisableHighlight ();
	}

	void OnRightClick ()
	{
		DisableHighlight ();
	}
	
	void UpdateSquareVision()
	{
		int x = 0;
		int y = 0;
		foreach (ISquare square in _squareVision)
			square.Standard ();
		_gridController.GetSquarePosition (oldSquare, ref x, ref y);
		this._squareVision = _gridController.GetVision (x, y, _oldOrientation, level);
		foreach (ISquare square in _squareVision)
		{
			square.Highlighted (team.color / 4.0f);
		}
	}

	void OnDestinationHit()
	{
		if (playerMotorController.HasReachedDestination (this.destination) && this.oldSquare != this.currentSquare)
		{
			this.oldSquare = currentSquare;
			dontTeleportMe = false;
		}
		if (playerMotorController.HasReachedRotation (this.rotation))
		{
			this._oldOrientation = this.playerOrientation;
		}

	}

	void ChangeAnimationSpeed()
	{
		this._animatorController.SetFloat ("Speed", _timeManager.timeSpeed / 5.0f);
	}

	void CorrectPlayerState()
	{
		this.StopIncantating ();
		this.StopLayingEgg ();
		if (dontTeleportMe)
		{
			playerMotorController.SetPosition (destination);
			dontTeleportMe = false;
			OnDestinationHit();
		}
	}

	public void Destroy ()
	{
		this.playerMotorController.Destroy();
	}

	public void OnDisable ()
	{
		_inputManager.OnLeftClicking -= OnLeftClick;
		_inputManager.OnRightClicking -= OnRightClick;
	}

	public void Update(Vector3 position)
	{
		if (!dead)
		{
			ChangeAnimationSpeed();
			Orientation animationOrientation = OrientationManager.GetAnimationOrientation(OrientationManager.GetDestinationOrientation(position, destination), playerOrientation);
			GoToDestination (animationOrientation);
			if (!playerMotorController.IsMoving (this.destination, this.rotation))
				StopExpulsion () ;
			if (_highlighted)
				UpdateSquareVision ();
			OnDestinationHit ();
			if (Time.realtimeSinceStartup - this._timeSinceLastInventoryUpdate > 1.0f / (_timeManager.timeSpeed / 100.0f))
				RefreshInventory ();
		}
	}

	public void LateUpdate()
	{
	}
}
