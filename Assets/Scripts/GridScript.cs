using UnityEngine;
using System;
using System.Collections;

<<<<<<< HEAD
public class GridScript : MonoBehaviour, ISquareInstantiationController {
=======
public class GridScript : MonoBehaviour {

	public class GridOutOfBoundsException : Exception
	{
		public GridOutOfBoundsException(){}
>>>>>>> feature/Square_gui

	public SquareScript[] prefabs = new SquareScript[2];
	public GridController controller;

<<<<<<< HEAD
	void OnEnable()
=======
	class GridIncorrectSizeException : Exception
	{
		public GridIncorrectSizeException (){}

		public GridIncorrectSizeException (string message) : base(message) {}
	}

	public class GridNotInitializedException : Exception
>>>>>>> feature/Square_gui
	{
		controller.SetSquareInstantiationController (this);
		controller.Start ();
	}

<<<<<<< HEAD
	#region ISquareInstantiationController implementation
=======
	public GameObject GridSquarePrefab;
	public GameObject[] grid { get ; private set; }
	public	int			startHeight;
	public	int			startWidth;

	private int			height;
	private int			width;

	void Start()
	{
		if (startHeight != 0 && startWidth != 0)
			init (startHeight, startWidth);
	}
>>>>>>> feature/Square_gui

	public ISquare Instantiate (int index)
	{
		return GameObject.Instantiate (prefabs[index]) as SquareScript;
	}

	public ISquare Instantiate (int index, Vector3 position)
	{
		SquareScript clone = GameObject.Instantiate (prefabs[index]) as SquareScript;
		clone.transform.SetParent (this.transform);
		clone.transform.localPosition = position;
		return clone;
	}

<<<<<<< HEAD
	#endregion
=======
	public void init(int width, int height)
	{
		float sizex;
		float sizey;
		float sizez;
		GameObject clone;

		if (width < 10 || width > 50 || height < 10 || height > 50)
			throw new GridIncorrectSizeException ("The grid will have an improper size with width = " + width + " and height = " + height + ".");
		this.width = width;
		this.height = height;

		clearGrid ();
		grid = new GameObject[width * height];

		clone = GameObject.Instantiate (GridSquarePrefab) as GameObject;
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
>>>>>>> feature/Square_gui
}
