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
	
	public class GridNotInitializedException : Exception
	{
		public GridNotInitializedException(){}
		
		public GridNotInitializedException(string message) : base(message) {}
	}

	public ISquare[] grid { get ; private set; }

	private int			height;
	private int			width;
	public	int			startHeight;
	public	int			startWidth;
	public	int			switchInt = 0;

	private ISquareInstantiationController squareInstantiationController;

	public void SetSquareInstantiationController (ISquareInstantiationController squareInstantiationController)
	{
		this.squareInstantiationController = squareInstantiationController;
	}

	public void Start()
	{
		if (startHeight != 0 && startWidth != 0)
			Init (startHeight, startWidth);
	}
	
	public void ClearGrid()
	{
		if (grid == null)
			return ;
		for (int i = 0; i < grid.Length; i++)
		{
			grid[i].Destroy();
			grid[i] = null;
		}
	}
	
	public ISquare GetSquare(int x, int y)
	{
		if (grid == null)
			throw new GridNotInitializedException("Tried to access the grid while it's not initialized yet.");
		if ((x * height + y) < 0 || (x * height + y) >= grid.Length)
			throw new GridOutOfBoundsException("Out of bounds with X = " + x + " and Y = " + y + ".");
		return grid[x * height + y];
	}
	
	public void Init(int width, int height)
	{
		ClearGrid ();
		this.width = width;
		this.height = height;
		grid = new ISquare[width * height];
		
		float sizex;
		float sizey;
		float sizez;
		ISquare clone = squareInstantiationController.Instantiate (switchInt);
		sizex = clone.GetBoundX();
		sizey = clone.GetBoundY();
		sizez = clone.GetBoundZ();
		clone.Destroy();

		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				clone = squareInstantiationController.Instantiate (switchInt, new Vector3(i * sizex, -sizey / 2.0f, j * sizez));
				switchInt ^= 1;
				grid[i * height + j] = clone;
			}
		}
	}

}
