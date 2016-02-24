﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Diagnostics;

[Serializable]
public class PlayerController {
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
	public float						speed = 1.0f;
	public float						rotSpeed = 1.0f;
									
	public	ISquare						currentSquare;
	private Quaternion					rotation;
	private bool						expulsed;
	public bool							dead { get; private set; }
	public Vector3						destination { get; private set; }
	public Vector3						teleportDestination;
	public Orientation					playerOrientation { get; private set; }
	
	private IAnimatorController			animatorController;
	private IPlayerMotorController		playerMovementController;
	private AInputManager				inputManager;	
		
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

	public void ChangeAnimationSpeed(float value)
	{
		this.animatorController.SetFloat ("Speed", GameManagerScript.instance.timeSpeed / 10.0f);
	}

	public void SetAnimatorController(IAnimatorController animatorController)
	{
		this.animatorController = animatorController;
	}
	
	public void SetPlayerMovementController(IPlayerMotorController playerMovementController)
	{
		this.playerMovementController = playerMovementController;
	}

	public void SetInputManager (AInputManager inputManager)
	{
		this.inputManager = inputManager;
		inputManager.OnLeftClicking += OnLeftClick;
		inputManager.OnRightClicking += OnRightClick;
	}
	
	public void IncantatePrimary()
	{
		animatorController.SetBool ("Incantate", true);
	}

	public void IncantateSecondary ()
	{
		animatorController.SetBool ("Incantate", true);
	}
	
	public void StopIncantating()
	{
		animatorController.SetBool("Incantate", false);
	}

	public void LayEgg()
	{
		animatorController.SetBool ("LayEgg", true);
	}

	public void StopLayingEgg()
	{
		animatorController.SetBool ("LayEgg", false);
	}

	public void GrabItem()
	{
		animatorController.SetTrigger ("Grab");
	}

	public void ThrowItem()
	{
		animatorController.SetTrigger ("Grab");
	}

	public void Die()
	{
		dead = true;
		animatorController.SetTrigger ("Death");
	}
	
	public void Expulse()
	{
		animatorController.SetTrigger ("Expulse");
		foreach (PlayerController player in currentSquare.GetResources().players)
			player.BeExpulsed (OrientationManager.Opposite (playerOrientation));
	}

	public void BeExpulsed(Orientation direction)
	{
		expulsed = true;
		playerMovementController.Expulsed (direction);
	}

	public void StopExpulsion()
	{
		expulsed = false;
		playerMovementController.StopExpulsion();
	}

	public PlayerController Init(int x, int y, Orientation orientation, int level, int index, Team team, GridController gridController)
	{
		try
		{
			ISquare square = gridController.GetSquare (x, y);
			SetDestination(square, gridController);
			playerMovementController.SetPosition(square.GetPosition());
			this.level = level;
			this.index = index;
			this.team = team;
			this.SetPlayerOrientation (orientation);
			playerMovementController.SetRotation (OrientationManager.GetRotation (orientation));
			playerMovementController.SetTeamColor (team.color);
			return this;
		}
		catch (GridController.GridOutOfBoundsException)
		{
			return null;
		}
	}

	public void SetDestination(ISquare square, GridController gridController)
	{
		if (this.currentSquare != square)
		{
			if (currentSquare != null)
				currentSquare.GetResources ().players.Remove (this);
			square.GetResources ().players.Add(this);
			Vector3 distance = currentSquare != null ? square.GetPosition () - currentSquare.GetPosition () : Vector3.zero;
			if (gridController != null && (Mathf.Abs (distance.x) > (gridController.width * square.GetBoundX ()) / 2.0f || Mathf.Abs (distance.z) > (gridController.height * square.GetBoundZ ()) / 2.0f))
				teleportDestination = gridController.GetNearestTeleport(distance, destination);
			destination = playerMovementController.SetDestination (square.GetPosition ());
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
		this.playerOrientation = playerOrientation;
		this.rotation = OrientationManager.GetRotation(playerOrientation);
	}
	
	public void GoToDestination(Orientation animationOrientation)
	{
		animatorController.SetBool ("Walk", playerMovementController.IsMoving(this.destination) && !expulsed);
		animatorController.SetInteger ("Orientation", (int)animationOrientation);
		if (teleportDestination != Vector3.zero)
			playerMovementController.MoveToDestination (teleportDestination, speed);
		else
			playerMovementController.MoveToDestination (destination, speed);
		playerMovementController.MoveToRotation(rotation, rotSpeed);
	}

	public void EnableHighlight()
	{
		playerMovementController.EnableHighlight (this.team.color);
	}

	public void DisableHighlight()
	{
		playerMovementController.DisableHighlight ();
	}

	void OnLeftClick(ClickEventArgs args)
	{
		if (args.target.IsPlayer () && args.target == (IClickTarget)playerMovementController)
			EnableHighlight ();
		else
			DisableHighlight ();
	}

	void OnRightClick ()
	{
		DisableHighlight ();
	}

	public void Update(Vector3 position)
	{
		if (!dead)
		{
			Orientation animationOrientation = OrientationManager.GetAnimationOrientation(OrientationManager.GetDestinationOrientation(position, destination), playerOrientation);
			GoToDestination (animationOrientation);
			if (!playerMovementController.IsMoving (this.destination))
				StopExpulsion ();
		}
	}
}
