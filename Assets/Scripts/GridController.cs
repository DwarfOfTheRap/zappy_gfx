using UnityEngine;
using System;
using System.Collections;

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

	public ISquare[] grid { get ; private set; }

	public int 			height { get; private set; }
	public int			width { get; private set; }
	public	int			startHeight;
	public	int			startWidth;

	private IGrid squareInstantiationController;

	public void SetSquareInstantiationController (IGrid squareInstantiationController)
	{
		this.squareInstantiationController = squareInstantiationController;
	}

	public void Start()
	{
		if (startHeight != 0 && startWidth != 0)
			Init (startHeight, startWidth);
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
		if ((x * height + y) < 0 || (x * height + y) >= grid.Length)
			throw new GridOutOfBoundsException("Out of bounds with X = " + x + " and Y = " + y + ".");
		return grid[x * height + y];
	}

	public Vector3 GetNearestTeleport (Vector3 distance, Vector3 currentPosition)
	{
		float absx = Mathf.Abs (distance.x);
		float absz = Mathf.Abs (distance.z);
		if (absx > absz && distance.x > 0)
			return new Vector3(squareInstantiationController.GetTeleporterPosition(Orientation.WEST).x, currentPosition.y, currentPosition.z);
		else if (absx > absz && distance.x <= 0)
			return new Vector3(squareInstantiationController.GetTeleporterPosition(Orientation.EAST).x, currentPosition.y, currentPosition.z);
		else if (absx <= absz && distance.z > 0)
			return new Vector3(currentPosition.x, currentPosition.y, squareInstantiationController.GetTeleporterPosition (Orientation.SOUTH).z);
		else if (absx <= absz && distance.z <= 0)
			return new Vector3(currentPosition.x, currentPosition.y, squareInstantiationController.GetTeleporterPosition(Orientation.NORTH).z);
		else
			return Vector3.zero;
	}
	
	public void Init(int width, int height)
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
		
		clone = squareInstantiationController.Instantiate (0);
		sizex = clone.GetBoundX();
		sizey = clone.GetBoundY();
		sizez = clone.GetBoundZ();
		clone.DestroyImmediate ();


		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{	
				clone = squareInstantiationController.Instantiate (switchInt, new Vector3(i * sizex, -sizey / 2.0f, j * sizez));
				switchInt ^= 1;
				grid[i * height + j] = clone;
			}
			if (height % 2 == 0)
				switchInt ^= 1;
		}
		squareInstantiationController.InitTeleporters (sizex, sizey, sizez, width, height);
	}

}
