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
	public GameObject GridSquarePrefab2;
	public GameObject[] grid { get ; private set; }
	private int			height;
	private int			width;
	public	int			startHeight;
	public	int			startWidth;

	void Start()
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

	public void Init(int width, int height)
	{
		ClearGrid ();
		this.width = width;
		this.height = height;
		grid = new GameObject[width * height];

		float sizex;
		float sizey;
		float sizez;
		GameObject clone = GameObject.Instantiate (GridSquarePrefab) as GameObject;
		sizex = clone.GetComponent<Renderer>().bounds.size.x;
		sizey = clone.GetComponent<Renderer>().bounds.size.y;
		sizez = clone.GetComponent<Renderer>().bounds.size.z;
		Destroy(clone);

		GameObject[] squareToPlace = {GridSquarePrefab, GridSquarePrefab2};
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				clone = GameObject.Instantiate (squareToPlace[(i + j) % 2 == 0 ? 1 : 0]) as GameObject;
				clone.transform.SetParent (this.transform);
				clone.transform.localPosition = new Vector3(i * sizex, -sizey / 2.0f, j * sizez);
				grid[i * height + j] = clone;
			}
		}
	}
}
