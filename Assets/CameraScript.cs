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
	private ISquare[] grid;

	void Start() {
		grid = GameManagerScript.instance.grid.controller.grid;
		aspectRatio = Screen.width / Screen.height;
		tanFov = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2.0f);
	}

	// Update is called once per frame
	void Update () {
		if (grid != null && grid.Length != 0)
		{
			// Position the camera in the center.
			Vector3 newCameraPos = Camera.main.transform.position;
			newCameraPos.x = middlePoint.x;
			this.transform.position = newCameraPos;
			
			// Find the middle point between players.
			Vector3 vectorBetweenPlayers = grid[grid.Length - 1].GetPosition() - grid[0].GetPosition();

			middlePoint = grid[0].GetPosition() + 0.5f * vectorBetweenPlayers;
			
			// Calculate the new distance.
			distanceBetweenPlayers = vectorBetweenPlayers.magnitude;
			cameraDistance = (distanceBetweenPlayers / 2.0f / aspectRatio) / tanFov;
			
			// Set camera to new position.
			Vector3 dir = (Camera.main.transform.position - middlePoint).normalized;
			this.transform.position = middlePoint + dir * (cameraDistance + DISTANCE_MARGIN);
		}
	}
}
