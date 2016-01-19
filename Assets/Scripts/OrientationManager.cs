using UnityEngine;
using System.Collections;

public enum Orientation {
	NORTH = 1,
	EAST = 2,
	SOUTH = 3,
	WEST = 4,
	NONE = 0
}

public static class OrientationManager {
	
	public static Orientation Opposite(Orientation orientation)
	{
		switch (orientation)
		{
		case Orientation.NORTH:
			return Orientation.SOUTH;
		case Orientation.EAST:
			return Orientation.WEST;
		case Orientation.SOUTH:
			return Orientation.NORTH;
		case Orientation.WEST:
			return Orientation.EAST;
		default:
			return Orientation.NONE;
		}
	}
	public static Quaternion GetRotation(Orientation playerOrientation)
	{
		switch (playerOrientation)
		{
		case Orientation.NORTH:
			return Quaternion.Euler (0, 0, 0);
		case Orientation.EAST:
			return Quaternion.Euler (0, 90, 0);
		case Orientation.SOUTH:
			return Quaternion.Euler (0, 180, 0);
		case Orientation.WEST:
			return Quaternion.Euler (0, 270, 0);
		default:
			return Quaternion.identity;
		}
	}
	
	public static Vector2 GetDirectionVector(Orientation orientation)
	{
		switch (orientation)
		{
		case Orientation.NORTH:
			return Vector2.up;
		case Orientation.EAST:
			return Vector2.right;
		case Orientation.SOUTH:
			return Vector2.down;
		case Orientation.WEST:
			return Vector2.left;
		default:
			return Vector2.zero;
		}
	}
	
	
	public static Orientation GetDestinationOrientation(Vector3 position, Vector3 destination)
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
	
	public static Orientation GetAnimationOrientation(Orientation destinationOrientation, Orientation playerOrientation)
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
}