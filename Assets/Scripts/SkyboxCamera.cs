using UnityEngine;
using System.Collections;

public class SkyboxCamera : MonoBehaviour {
	public float	speed = 0.0001f;
	
	// Update is called once per frame
	void Update () {
		this.transform.localRotation = Quaternion.Euler (this.transform.localRotation.eulerAngles.x, this.transform.localRotation.eulerAngles.y + speed, this.transform.localRotation.eulerAngles.z);
	}
}
