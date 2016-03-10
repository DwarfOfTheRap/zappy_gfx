using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class GridController {

	public class GridOutOfBoundsException : Exception
	{
		public GridOutOfBoundsException(){}
		
		public GridOutOfBoundsException(string message) : base(message) {}
	}

	public class GridIllegalSizeException : Exception
	{
		public GridIllegalSizeException(){}

		public GridIllegalSizeException (string message) : base(message) {}
	}
	
	public class GridNotInitializedException : Exception
	{
		public GridNotInitializedException(){}
		
		public GridNotInitializedException(string message) : base(message) {}
	}

	public class SquareNotFoundException : Exception
	{
		public SquareNotFoundException(){}
		public SquareNotFoundException(string message) : base(message) {}
	}

	public ISquare[]	grid { get ; private set; }

	public int 			height { get; private set; }
	public int			width { get; private set; }

	#if UNITY_EDITOR
	public int			startHeight;
	public int			startWidth;
	#endif

	private IGrid		_gridMotor;

	#if UNITY_EDITOR
	public void Start ()
	{
		if (startHeight != 0 && startWidth != 0)
			Init (startHeight, startWidth);
	}
	#endif

	public void SetSquareInstantiationController (IGrid gridMotor)
	{
		this._gridMotor = gridMotor;
	}
	
	public ISquare[] ClearGrid(ISquare[] grid)
	{
		if (grid == null)
			return null;
		for (int i = 0; i < grid.Length; i++)
			grid[i].Destroy();
		return null;
	}
	
	public virtual ISquare GetSquare(int x, int y)
	{
		if (grid == null)
			throw new GridNotInitializedException("Tried to access the grid while it's not initialized yet.");
		if ((x * height + y) < 0 || (x * height + y) >= grid.Length || x < 0 || y < 0)
			throw new GridOutOfBoundsException("Out of bounds with X = " + x + " and Y = " + y + ".");
		return grid[x * height + y];
	}

	public bool GetSquarePosition(ISquare square, ref int x, ref int y)
	{
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				if (grid[i * height + j] == square)
				{
					x = i;
					y = j;
					return true;
				}
			}
		}
		return false;
	}

	void GetVisionNorth(int x, int y, int level, ref List<ISquare> squares)
	{
		for (int i = 0; i <= level; i++)
		{
			int count = (i * 2) + 1;
			for (int j = 0; j < count; j++)
				squares.Add (this.GetSquare (((x - count / 2 + j) + width) % width, ((y + i) + height) % height));
		}
	}

	void GetVisionSouth(int x, int y, int level, ref List<ISquare> squares)
	{
		for (int i = 0; i <= level; i++)
		{
			int count = (i * 2) + 1;
			for (int j = 0; j < count; j++)
				squares.Add (this.GetSquare (((x - count / 2 + j) + width) % width, ((y - i) + height) % height));
		}
	}

	void GetVisionEast(int x, int y, int level, ref List<ISquare> squares)
	{
		for (int i = 0; i <= level; i++)
		{
			int count = (i * 2) + 1;
			for (int j = 0; j < count; j++)
				squares.Add (this.GetSquare (((x + i) + width) % width, ((y - count / 2 + j) + height) % height));
		}
	}

	void GetVisionWest(int x, int y, int level, ref List<ISquare> squares)
	{
		for (int i = 0; i <= level; i++)
		{
			int count = (i * 2) + 1;
			for (int j = 0; j < count; j++)
				squares.Add (this.GetSquare (((x - i) + width) % width, ((y - count / 2 + j) + height) % height));
		}
	}

	public virtual ISquare[] GetVision(int x, int y, Orientation orientation, int level)
	{
		List<ISquare> squares = new List<ISquare>();
		switch (orientation)
		{
			case Orientation.NORTH:
				this.GetVisionNorth (x, y, level, ref squares);
				break;
			case Orientation.SOUTH:
				this.GetVisionSouth (x, y, level, ref squares);
				break;
			case Orientation.EAST:
				this.GetVisionEast (x, y, level, ref squares);
				break;
			case Orientation.WEST:
				this.GetVisionWest (x, y, level, ref squares);
				break;
			default:
				this.GetVisionSouth (x, y, level, ref squares);
				break;
		}
		if (squares.Count == 0)
			return null;
		return squares.ToArray ();
	}

	public Vector3 GetNearestTeleport (Vector3 distance, Vector3 currentPosition)
	{
		float absx = Mathf.Abs (distance.x);
		float absz = Mathf.Abs (distance.z);
		if (absx > absz && distance.x > 0)
			return new Vector3(_gridMotor.GetTeleporterPosition(Orientation.WEST).x, currentPosition.y, currentPosition.z);
		else if (absx > absz && distance.x <= 0)
			return new Vector3(_gridMotor.GetTeleporterPosition(Orientation.EAST).x, currentPosition.y, currentPosition.z);
		else if (absx <= absz && distance.z > 0)
			return new Vector3(currentPosition.x, currentPosition.y, _gridMotor.GetTeleporterPosition (Orientation.SOUTH).z);
		else if (absx <= absz && distance.z <= 0)
			return new Vector3(currentPosition.x, currentPosition.y, _gridMotor.GetTeleporterPosition(Orientation.NORTH).z);
		else
			return Vector3.zero;
	}
	
	public virtual void Init(int width, int height)
	{
		float sizex;
		float sizey;
		float sizez;
		ISquare clone;
		int switchInt = 0;

		if (width < 10 || height < 10 || width > 50 || height > 50)
			throw new GridIllegalSizeException ("Incorrect grid size with width = " + width + " and height = " + height + ".");
		grid = ClearGrid (grid);
		this.width = width;
		this.height = height;
		grid = new ISquare[width * height];
		
		clone = _gridMotor.Instantiate (0);
		sizex = clone.GetBoundX();
		sizey = clone.GetBoundY();
		sizez = clone.GetBoundZ();
		clone.DestroyImmediate ();


		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{	
				clone = _gridMotor.Instantiate (switchInt, new Vector3(i * sizex, -sizey / 2.0f, j * sizez));
				switchInt ^= 1;
				grid[i * height + j] = clone;
			}
			if (height % 2 == 0)
				switchInt ^= 1;
		}
		_gridMotor.InitTeleporters (sizex, sizey, sizez, width, height);
	}

}
