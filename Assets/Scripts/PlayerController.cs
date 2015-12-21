using UnityEngine;
using System.Collections;
using System;

public enum Orientation {
	NORTH = 1,
	EAST = 2,
	SOUTH = 3,
	WEST = 4,
	NONE = 0
}

[Serializable]
public class PlayerController {
	public int			index;
	public float		speed = 1.0f;
	public float		rotSpeed = 1.0f;
	public bool 		isIncantating { get; private set; }
	public bool			dead { get; private set; }
	private ISquare		currentSquare;
	private Quaternion	rotation;
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
	}

	public void SetPosition(int x, int y, GridController gridController)
	{
		ISquare square = gridController.GetSquare (x, y);
		if (this.currentSquare != square)
		{
			square.GetResources ().players.Add(this);
			destination = playerMovementController.SetDestination (square.GetPosition ());
		}
		currentSquare = square;
	}

	public void SetPlayerOrientation(Orientation playerOrientation)
	{
		this.playerOrientation = playerOrientation;
		switch (playerOrientation)
		{
			case Orientation.NORTH:
				rotation = Quaternion.Euler (0, 0, 0);
				break;
			case Orientation.EAST:
				rotation = Quaternion.Euler (0, 90, 0);
				break;
			case Orientation.SOUTH:
				rotation = Quaternion.Euler (0, 180, 0);
				break;
			case Orientation.WEST:
				rotation = Quaternion.Euler (0, 270, 0);
				break;
		}
	}
	

	public Orientation GetDestinationOrientation(Vector3 position, Vector3 destination)
	{
		if (position == destination)
			return Orientation.NONE;
		Vector3 heading = destination - position;
		Vector3 direction = heading / heading.magnitude;
		float absx = Mathf.Abs (direction.x);
		float absz = Mathf.Abs (direction.z);
		
		if (absx > absz && direction.x <= 0)
			return Orientation.WEST;
		if (absx > absz && direction.x > 0)
			return Orientation.EAST;
		if (absx <= absz && direction.z <= 0)
			return Orientation.SOUTH;
		if (absx <= absz && direction.z > 0)
			return Orientation.NORTH;
		return Orientation.NONE;
	}

	public Orientation GetAnimationOrientation(Orientation destinationOrientation, Orientation playerOrientation)
	{
		int destinationInt = (int)destinationOrientation;
		int playerOrientationInt = (int)playerOrientation;
		if ((4 + (destinationInt - playerOrientationInt)) % 4 == 0)
			return Orientation.NORTH;
		if ((4 + (destinationInt - playerOrientationInt)) % 4 == 1)
			return Orientation.EAST;
		if ((4 + (destinationInt - playerOrientationInt)) % 4 == 2)
			return Orientation.SOUTH;
		if ((4 + (destinationInt - playerOrientationInt)) % 4 == 3)
			return Orientation.WEST;
		return Orientation.NONE;
	}

	public void GoToDestination(Orientation animationOrientation)
	{
		animatorController.SetBool ("Walk", playerMovementController.IsMoving ());
		animatorController.SetInteger ("Orientation", (int)animationOrientation);
		playerMovementController.MoveToDestination (destination, speed);
		playerMovementController.MoveToRotation(rotation, rotSpeed);
	}

	private void Move(Vector3 position, Vector3 destination)
	{
		Orientation animationOrientation = GetAnimationOrientation (GetDestinationOrientation(position, destination), playerOrientation);
		GoToDestination (animationOrientation);
	}
	
	public void Update(Vector3 position, Vector3 destination)
	{
		if (!dead)
			Move (position, destination);
	}
}
