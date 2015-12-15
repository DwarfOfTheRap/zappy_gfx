using UnityEngine;
using System.Collections;



public class CameraScript : MonoBehaviour {

	private const float DISTANCE_MARGIN = 1.0f;
	
	private Vector3 middlePoint;
	private float distanceFromMiddlePoint;
	private float distanceBetweenPlayers;
	private float cameraDistance;
	private float aspectRatio;
	private float fov;
	private float tanFov;

	void Start() {
		aspectRatio = Screen.width / Screen.height;
		tanFov = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2.0f);
	}

	// Update is called once per frame
	void Update () {
		if (GameManagerScript.instance.grid.grid.Length != 0)
		{
			// Position the camera in the center.
			Vector3 newCameraPos = Camera.main.transform.position;
			newCameraPos.x = middlePoint.x;
			this.transform.position = newCameraPos;
			
			// Find the middle point between players.
			Vector3 vectorBetweenPlayers = GameManagerScript.instance.grid.grid[GameManagerScript.instance.grid.grid.Length - 1].transform.position -
				GameManagerScript.instance.grid.grid[0].transform.position;

			middlePoint = GameManagerScript.instance.grid.grid[0].transform.position + 0.5f * vectorBetweenPlayers;
			
			// Calculate the new distance.
			distanceBetweenPlayers = vectorBetweenPlayers.magnitude;
			cameraDistance = (distanceBetweenPlayers / 2.0f / aspectRatio) / tanFov;
			
			// Set camera to new position.
			Vector3 dir = (Camera.main.transform.position - middlePoint).normalized;
			this.transform.position = middlePoint + dir * (cameraDistance + DISTANCE_MARGIN);
		}
	}
}
