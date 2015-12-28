using UnityEngine;
using System;
using System.Collections;

public class GridScript : MonoBehaviour, IGrid {

	public SquareScript[] prefabs = new SquareScript[2];
	public TeleportScript[] teleporters;
	public GridController controller;
	
	void OnEnable()
	{
		teleporters = GetComponentsInChildren<TeleportScript>();
		controller.SetSquareInstantiationController (this);
		controller.Start ();
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

	public void InitTeleporters (float sizex, float sizey, float sizez, int width, int height)
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
