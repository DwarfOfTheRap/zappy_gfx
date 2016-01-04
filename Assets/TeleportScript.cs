using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class TeleportScript : MonoBehaviour {
	public Orientation		teleportOrientation;
	public GameObject		destination;

	void Start()
	{
		switch (teleportOrientation)
		{
			case Orientation.NORTH:
				destination = GameObject.Find("TeleportSouth");
				break;
			case Orientation.EAST:
				destination = GameObject.Find("TeleportWest");
				break;
			case Orientation.SOUTH:
				destination = GameObject.Find ("TeleportNorth");
				break;
			case Orientation.WEST:
				destination = GameObject.Find ("TeleportEast");
				break;

		}
	}
	void OnTriggerEnter(Collider col)
	{
		Vector3 distance = col.transform.position - destination.transform.position;

		switch (teleportOrientation)
		{
			case Orientation.EAST: case Orientation.WEST:
				col.transform.position = distance + new Vector3(destination.transform.position.x, col.transform.position.y, col.transform.position.y);
				break;
			case Orientation.NORTH: case Orientation.SOUTH:
				col.transform.position = distance + new Vector3(col.transform.position.x, col.transform.position.y, destination.transform.position.z);
				break;
		}
	}
}
