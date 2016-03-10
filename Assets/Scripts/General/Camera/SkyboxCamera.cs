using UnityEngine;
using System.Collections;

public class SkyboxCamera : MonoBehaviour {
	public float	rightspeed;
	public float	upspeed;
	public float	forwardspeed;

	void Rotate ()
	{
		this.transform.Rotate (Vector3.right * rightspeed * Time.deltaTime);
		this.transform.Rotate (Vector3.up * upspeed * Time.deltaTime);
		this.transform.Rotate (Vector3.forward * forwardspeed * Time.deltaTime);
	}

	void Update () {
		Rotate ();
	}
}
