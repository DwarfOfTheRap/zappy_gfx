﻿using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class GridController {

	public class GridOutOfBoundsException : Exception
	{
		public GridOutOfBoundsException(){}
		
		public GridOutOfBoundsException(string message) : base(message) {}
	}

	public class GridIllegalIndexException : Exception
	{
		public GridIllegalIndexException(){}

		public GridIllegalIndexException (string message) : base(message) {}
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
	
	public ISquare[] ClearGrid(ISquare[] grid)
	{
		if (grid == null)
			return null;
		for (int i = 0; i < grid.Length; i++)
			grid[i].Destroy();
		return null;
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
		if (width < 0 || height < 0 || width * height > int.MaxValue)
			throw new GridIllegalIndexException ("Index is out of range of the grid with width = " + width + " and height = " + height + ".");
		grid = ClearGrid (grid);
		this.width = width;
		this.height = height;
		grid = new ISquare[width * height];
		
		float sizex;
		float sizey;
		float sizez;
		ISquare clone = squareInstantiationController.Instantiate (0);
		sizex = clone.GetBoundX();
		sizey = clone.GetBoundY();
		sizez = clone.GetBoundZ();
		clone.DestroyImmediate ();

		int switchInt = 0;
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
	}

}
