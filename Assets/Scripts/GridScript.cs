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

	void Start()
	{
		init (3, 3);
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
		clearGrid ();
		this.width = width;
		this.height = height;
		grid = new GameObject[width * height];
		GameObject clone = GameObject.Instantiate (GridSquarePrefab) as GameObject;
		sizex = clone.GetComponent<SpriteRenderer>().bounds.size.x;
		sizey = clone.GetComponent<SpriteRenderer>().bounds.size.y / 1.6f;
		Destroy(clone);
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				clone = GameObject.Instantiate (GridSquarePrefab) as GameObject;
				clone.transform.SetParent (this.transform);
				clone.transform.localPosition = new Vector3(i * sizex, j * sizey, 0);
				grid[i * height + j] = clone;
			}
		}
		Camera.main.transform.localPosition = new Vector3(sizex * width / 2.0f - sizex / 2.0f, sizey * height / 2.0f - sizey / 2.0f, -10);
		Camera.main.orthographicSize = (width + 1) * sizex * Screen.height / Screen.width * 0.5f + 1;
	}
}
