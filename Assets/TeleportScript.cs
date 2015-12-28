using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class TeleportScript : MonoBehaviour {
	public TeleportScript	destination;
	public Orientation		teleportOrientation;

	void OnTriggerEnter(Collider col)
	{
		switch (teleportOrientation)
		{
			case Orientation.EAST: case Orientation.WEST:
				col.transform.position = new Vector3(destination.transform.position.x, col.transform.position.y, col.transform.position.y);
				break;
			case Orientation.NORTH: case Orientation.SOUTH:
				col.transform.position = new Vector3(col.transform.position.x, col.transform.position.y, destination.transform.position.z);
				break;
		}
	}
}
