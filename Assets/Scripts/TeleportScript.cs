using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
public class TeleportScript : MonoBehaviour {
	public Orientation		teleportOrientation;
	public GameObject		destination;
	public List<GameObject>	disabled;

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
		if (col.tag == "Player" && disabled.Find (x => x == col.gameObject) == null)
		{
			Vector3 distance = col.transform.position - transform.position;
			switch (teleportOrientation)
			{
				case Orientation.EAST: case Orientation.WEST:
					col.GetComponent<PlayerScript>().controller.teleportDestination = Vector3.zero;
					col.transform.position = new Vector3(destination.transform.position.x - distance.x, col.transform.position.y, col.transform.position.z);
					destination.GetComponent<TeleportScript>().disabled.Add(col.gameObject);
					break;
				case Orientation.NORTH: case Orientation.SOUTH:
					col.GetComponent<PlayerScript>().controller.teleportDestination = Vector3.zero;
					col.transform.position = new Vector3(col.transform.position.x, col.transform.position.y, destination.transform.position.z - distance.z);
					destination.GetComponent<TeleportScript>().disabled.Add(col.gameObject);
					break;
			}
		}
	}

	void OnTriggerExit(Collider col)
	{
		disabled.Remove (col.gameObject);
	}
}