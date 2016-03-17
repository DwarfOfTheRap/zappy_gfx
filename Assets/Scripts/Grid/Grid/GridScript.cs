using UnityEngine;
using System;
using System.Collections;

public class GridScript : MonoBehaviour, IGrid {

	public SquareScript[] prefabs = new SquareScript[2];
	public TeleportScript[] teleporters { get; private set; }
	public GridController controller;
	public float	_initProgress = 0.0f;
	
	void OnEnable()
	{
		teleporters = GetComponentsInChildren<TeleportScript>();
		controller.SetSquareInstantiationController (this);
		#if UNITY_EDITOR
		controller.Start();
		#endif
	}

	#region ISquareInstantiationController implementation

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

	public Vector3 GetTeleporterPosition(Orientation orientation)
	{
		foreach (TeleportScript teleporter in teleporters)
		{
			if (teleporter.teleportOrientation == orientation)
				return teleporter.transform.position;
		}
		return Vector3.zero;
	}

	public void Init(int width, int height)
	{
		StartCoroutine (InitCoroutine (width, height));
	}

	public float GetInitProgress()
	{
		return _initProgress;
	}

	IEnumerator InitCoroutine(int width, int height)
	{
		int switchInt = 0;
		var clone = this.Instantiate(0);
		var sizex = clone.GetBoundX();
		var sizey = clone.GetBoundY();
		var sizez = clone.GetBoundZ();
		var grid = new ISquare[width * height];
		clone.DestroyImmediate ();
		_initProgress = 0.0f;
		
		var time = Time.realtimeSinceStartup;
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				clone = this.Instantiate (switchInt, new Vector3(i * sizex, -sizey / 2.0f, j * sizez));
				switchInt ^= 1;
				grid[i * height + j] = clone;
				_initProgress = Mathf.Clamp (_initProgress + (1.0f / (width * height)), 0.0f, 1.0f);
				if (Time.realtimeSinceStartup - time >= Time.smoothDeltaTime)
				{
					yield return null;
					time = Time.realtimeSinceStartup;
				}
			}
			if (height % 2 == 0)
				switchInt ^= 1;
		}
		_initProgress = 1.0f;
		InitTeleporters(sizex, sizey, sizez, width, height);
		controller.SetGrid (grid);
	}

	void InitTeleporters (float sizex, float sizey, float sizez, int width, int height)
	{
		float maxX = width * sizex;
		float minX = -1.0f * sizex;
		float y = sizey / 2.0f;
		float maxZ = height * sizez;
		float minZ = -1.0f * sizez;

		foreach (TeleportScript teleporter in teleporters)
		{
			switch (teleporter.teleportOrientation)
			{
				case Orientation.NORTH:
					teleporter.transform.localPosition = new Vector3((sizex * (width - 1)) / 2.0f, y, maxZ);
					teleporter.GetComponent<BoxCollider>().size = new Vector3((width + 2) * sizex, sizey, sizez);
					break;
				case Orientation.SOUTH:
					teleporter.transform.localPosition = new Vector3((sizex * (width - 1)) / 2.0f, y, minZ);
					teleporter.GetComponent<BoxCollider>().size = new Vector3((width + 2) * sizex, sizey, sizez);
					break;
				case Orientation.WEST:
					teleporter.transform.localPosition = new Vector3(minX, y, (sizey * (height - 1)) / 2.0f);
					teleporter.GetComponent<BoxCollider>().size = new Vector3(sizex, sizey, (height + 2) * sizez);
					break;
				case Orientation.EAST:
					teleporter.transform.localPosition = new Vector3(maxX, y, (sizey * (height - 1)) / 2.0f);
					teleporter.GetComponent<BoxCollider>().size = new Vector3(sizex, sizey, (height + 2) * sizez);
					break;
			}
		}
	}

	#endregion
}
