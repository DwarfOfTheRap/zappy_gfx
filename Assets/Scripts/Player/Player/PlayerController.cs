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
	public float						speed = 0.3597f;
	public float						rotSpeed = 6.38f;

	// Player position + orientation
	private ISquare						_oldSquare;
	public	ISquare						currentSquare;
	private Orientation					_oldOrientation;
	public  Quaternion					rotation { get; private set; }

	// Vision + Highlight	
	private bool						_highlighted = false;
	private ISquare[]					_squareVision = new ISquare[0];
									
	// Teleport Attributes
	public Vector3						teleportDestination;
	public bool							dontTeleportMe { get; set; }


	// Controller
	private IAnimatorController			_animatorController;
	private IPlayerMotorController		_playerMotorController;
	private GridController				_gridController;

	// Managers
	private AInputManager				_inputManager;
	private TimeManager					_timeManager;
		
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
		this._playerMotorController = playerMovementController;
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
		_animatorController.SetBool ("LayEgg", true);
	}

	public void StopLayingEgg()
	{
		_animatorController.SetBool ("LayEgg", false);
	}

	public void GrabItem()
	{
		_animatorController.SetTrigger ("Grab");
	}

	public void ThrowItem()
	{
		_animatorController.SetTrigger ("Grab");
	}

	public void Die()
	{
		dead = true;
		_animatorController.SetTrigger ("Death");
	}

	// Actions
	public void Broadcast (string message)
	{
		_playerMotorController.Broadcast (message);
	}
	
	public void Expulse()
	{
		_animatorController.SetTrigger ("Expulse");
		foreach (PlayerController player in currentSquare.GetResources().players)
			player.BeExpulsed (OrientationManager.Opposite (playerOrientation));
	}

	public void BeExpulsed(Orientation direction)
	{
		expulsed = true;
		_playerMotorController.Expulsed (direction);
	}

	public void StopExpulsion()
	{
		expulsed = false;
		_playerMotorController.StopExpulsion();
	}

	public PlayerController Init(int x, int y, Orientation orientation, int level, int index, Team team, GridController gridController)
	{
		try
		{
			ISquare square = gridController.GetSquare (x, y);
			SetDestination(square, gridController);
			_playerMotorController.SetPosition(square.GetPosition());
			this.level = level;
			this.index = index;
			this.team = team;
			this.SetPlayerOrientation (orientation);
			_playerMotorController.SetRotation (OrientationManager.GetRotation (orientation));
			_playerMotorController.SetTeamColor (team.color);
			return this;
		}
		catch (GridController.GridOutOfBoundsException)
		{
			return null;
		}
	}

	public void SetDestination(ISquare square, GridController gridController)
	{
		this._oldSquare = this.currentSquare;
		if (this.currentSquare != square)
		{
			Vector3 distance = currentSquare != null ? square.GetPosition () - currentSquare.GetPosition () : Vector3.zero;
			if (gridController != null && (Mathf.Abs (distance.x) > (gridController.width * square.GetBoundX ()) / 2.0f || Mathf.Abs (distance.z) > (gridController.height * square.GetBoundZ ()) / 2.0f))
				teleportDestination = gridController.GetNearestTeleport(distance, destination);
			destination = _playerMotorController.SetDestination (square.GetPosition ());
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
		this._oldOrientation = this.playerOrientation;
		this.playerOrientation = playerOrientation;
		this.rotation = OrientationManager.GetRotation(playerOrientation);
	}
	
	public void GoToDestination(Orientation animationOrientation)
	{
		_animatorController.SetBool ("Walk", _playerMotorController.IsMoving(this.destination) && !expulsed);
		_animatorController.SetInteger ("Orientation", (int)animationOrientation);
		if (teleportDestination != Vector3.zero)
			_playerMotorController.MoveToDestination (teleportDestination, speed);
		else
			_playerMotorController.MoveToDestination (destination, speed);
		_playerMotorController.MoveToRotation(rotation, rotSpeed);
	}

	void EnableHighlight()
	{
		_highlighted = true;
		_playerMotorController.EnableHighlight (this.team.color);
	}

	public void DisableHighlight()
	{
		_highlighted = false;
		_playerMotorController.DisableHighlight ();
	}

	void OnLeftClick(ClickEventArgs args)
	{
		if (args.target.IsPlayer () && args.target == (IClickTarget)_playerMotorController)
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
		_gridController.GetSquarePosition (_oldSquare, ref x, ref y);
		this._squareVision = _gridController.GetVision (x, y, _oldOrientation, level);
		foreach (ISquare square in _squareVision)
		{
			square.Highlighted (team.color / 4.0f);
		}
	}

	void OnDestinationHit()
	{
		if (_playerMotorController.HasReachedDestination (this.destination))
		{
			if (this._oldSquare != null)
				this._oldSquare.GetResources().players.Remove (this);
			this.currentSquare.GetResources ().players.Add (this);
			this._oldSquare = currentSquare;
			dontTeleportMe = false;
		}
		if (_playerMotorController.HasReachedRotation (this.rotation))
		{
			this._oldOrientation = this.playerOrientation;
		}

	}

	void ChangeAnimationSpeed()
	{
		this._animatorController.SetFloat ("Speed", _timeManager.timeSpeed / 10.0f);
	}

	public void Destroy ()
	{
		this._playerMotorController.Destroy();
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
			if (!_playerMotorController.IsMoving (this.destination))
				StopExpulsion () ;
			if (_highlighted)
				UpdateSquareVision ();
			OnDestinationHit ();
		}
	}
}
