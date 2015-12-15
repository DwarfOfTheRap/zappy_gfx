using UnityEngine;
using System;
using System.Collections;

public class GridScript : MonoBehaviour {
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

	public GameObject GridSquarePrefab;
	public GameObject[] grid { get ; private set; }
	private int			height;
	private int			width;
	public	int			startHeight;
	public	int			startWidth;

	void Start()
	{
		if (startHeight != 0 && startWidth != 0)
			init (startHeight, startWidth);
	}

	public void clearGrid()
	{
		if (grid == null)
			return ;
		for (int i = 0; i < grid.Length; i++)
		{
			Destroy (grid[i]);
			grid[i] = null;
		}
	}

	public GameObject GetSquare(int x, int y)
	{
		if (grid == null)
			throw new GridNotInitializedException("Tried to access the grid while it's not initialized yet.");
		if ((x * height + y) < 0 || (x * height + y) >= grid.Length)
			throw new GridOutOfBoundsException("Out of bounds with X = " + x + " and Y = " + y + ".");
		return grid[x * height + y];
	}

	public void init(int width, int height)
	{
		float sizex;
		float sizey;
		float sizez;
		clearGrid ();
		this.width = width;
		this.height = height;
		grid = new GameObject[width * height];
		GameObject clone = GameObject.Instantiate (GridSquarePrefab) as GameObject;
		sizex = clone.GetComponent<Renderer>().bounds.size.x;
		sizey = clone.GetComponent<Renderer>().bounds.size.y;
		sizez = clone.GetComponent<Renderer>().bounds.size.z;
		Destroy(clone);
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				clone = GameObject.Instantiate (GridSquarePrefab) as GameObject;
				clone.transform.SetParent (this.transform);
				clone.transform.localPosition = new Vector3(i * sizex, -sizey / 2.0f, j * sizez);
				grid[i * height + j] = clone;
			}
		}
	}
}
