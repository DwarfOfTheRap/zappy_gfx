using UnityEngine;
using System.Collections;

public class PlayerCanvasScript : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		Vector3 camPos = Camera.main.transform.position;
		this.transform.LookAt (new Vector3(this.transform.position.x, this.transform.position.y, -camPos.z));
	}
}
