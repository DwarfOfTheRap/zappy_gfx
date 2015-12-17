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
	public bool 		isIncantating { get; private set; }
	private Vector2		positionIndex;
	public Vector3		destination;
	public Orientation	playerOrientation;
	
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

	public void SetPosition(int x, int y)
	{
		if (this.positionIndex.x != x || this.positionIndex.y != y)
			playerMovementController.SetDestination (x, y);
		this.positionIndex.x = x;
		this.positionIndex.y = y;
	}

	Orientation GetDestinationOrientation(Vector3 position, Vector3 destination)
	{
		if (position == destination)
			return Orientation.NONE;
		Vector3 heading = destination - position;
		Vector3 direction = heading / heading.magnitude;
		float absx = Mathf.Abs (direction.x);
		float absy = Mathf.Abs (direction.y);
		Debug.Log (direction);
		
		if (absx > absy && direction.x <= 0)
			return Orientation.WEST;
		if (absx > absy && direction.x > 0)
			return Orientation.EAST;
		if (absx <= absy && direction.y <= 0)
			return Orientation.SOUTH;
		if (absx <= absy && direction.y > 0)
			return Orientation.NORTH;
		return Orientation.NONE;
	}

	Orientation GetAnimationOrientation(Orientation destinationOrientation, Orientation playerOrientation)
	{
		int destinationInt = (int)destinationOrientation;
		int playerOrientationInt = (int)playerOrientation;
		if (4 + Mathf.Abs (destinationInt - playerOrientationInt) % 4 == 0)
			return Orientation.NORTH;
		if (4 + Mathf.Abs (destinationInt - playerOrientationInt) % 4 == 1)
			return Orientation.EAST;
		if (4 + Mathf.Abs (destinationInt - playerOrientationInt) % 4 == 2)
			return Orientation.SOUTH;
		if (4 + Mathf.Abs (destinationInt - playerOrientationInt) % 4 == 3)
			return Orientation.WEST;
		return Orientation.NONE;
	}

	void GoToDestination(Orientation animationOrientation)
	{
		animatorController.SetBool ("Walk", playerMovementController.IsMoving ());
		animatorController.SetInteger ("Orientation", (int)animationOrientation);
		playerMovementController.MoveToDestination (speed);
	}
	
	public void Update(Vector3 position, Vector3 destination)
	{
		Orientation animationOrientation = GetAnimationOrientation (GetDestinationOrientation(position, destination), playerOrientation);
		GoToDestination (animationOrientation);
	}
}
