using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class PlayerController {
	public int			index;
	public float		speed = 1.0f;
	public float		rotSpeed = 1.0f;

	private ISquare		currentSquare;
	private Quaternion	rotation;
	private bool		expulsed;

	public bool 		isIncantating { get; private set; }
	public bool			dead { get; private set; }
	public Vector3		destination { get; private set; }
	public Orientation	playerOrientation { get; private set; }
	
	private IAnimatorController	animatorController;
	private IPlayerMovementController playerMovementController;

	public void SetAnimatorController(IAnimatorController animatorController)
	{
		this.animatorController = animatorController;
	}

	public void SetPlayerMovementController(IPlayerMovementController playerMovementController)
	{
		this.playerMovementController = playerMovementController;
	}

	public void Incantate()
	{
		isIncantating = true;
		animatorController.SetBool ("Incantate", true);
	}

	public void StopIncantating()
	{
		isIncantating = false;
		animatorController.SetBool("Incantate", false);
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

	public void SetDestination(ISquare square)
	{
		if (this.currentSquare != square)
		{
			if (currentSquare != null)
				currentSquare.GetResources ().players.Remove (this);
			square.GetResources ().players.Add(this);
			destination = playerMovementController.SetDestination (square.GetPosition ());
		}
		currentSquare = square;
	}

	public void SetPosition(int x, int y, GridController gridController)
	{
		try
		{
			ISquare square = gridController.GetSquare (x, y);
			SetDestination (square);
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
		playerMovementController.MoveToDestination (destination, speed);
		playerMovementController.MoveToRotation(rotation, rotSpeed);
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
