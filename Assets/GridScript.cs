using UnityEngine;
using System.Collections;

public class GridScript : MonoBehaviour {
	public GameObject GridSquarePrefab;
	public GameObject[] Grid;

	void Start()
	{
		init (3, 3);
	}

	public void clearGrid()
	{
		if (Grid == null)
			return ;
		for (int i = 0; i < Grid.Length; i++)
		{
			Destroy (Grid[i]);
			Grid[i] = null;
		}
	}

	public void init(int width, int height)
	{
		float sizex;
		float sizey;
		clearGrid ();
		Grid = new GameObject[width * height];
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
				Grid[i * height + j] = clone;
			}
		}
		Camera.main.transform.localPosition = new Vector3(sizex * width / 2.0f - sizex / 2.0f, sizey * height / 2.0f - sizey / 2.0f, -10);
		Camera.main.orthographicSize = (width + 1) * sizex * Screen.height / Screen.width * 0.5f + 1;
	}
}
