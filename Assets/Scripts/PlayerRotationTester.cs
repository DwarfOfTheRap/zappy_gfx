using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerScript))]
public class PlayerRotationTester : MonoBehaviour {
	public Orientation		orientation;

	void Start()
	{
		GetComponent<PlayerScript>().controller.SetPlayerOrientation (orientation);
	}
}
