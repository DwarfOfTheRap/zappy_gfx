using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour, ICameraMovement {

	public CameraController		cameraController;

	#region ICameraMovement implementation

	public Vector3 GoTo (Vector3 position)
	{
		this.transform.position = position;
		return (this.transform.position);
	}

	public Vector3 Move (Vector3 direction, float speed)
	{
		this.transform.position += direction * speed * Time.deltaTime;
		return (this.transform.position);
	}


	public Quaternion Rotate (Quaternion rotation)
	{
		this.transform.rotation = rotation;
		return (this.transform.rotation);
	}


	public Vector3 LerpMove (Vector3 destination, float speed)
	{
		this.transform.position = Vector3.Lerp (transform.position, destination, speed * Time.deltaTime);
		return (transform.position);
	}


	#endregion

	void Start () {
		cameraController = new CameraController(GameManagerScript.instance.inputManager, this, GameManagerScript.instance.gridController.height, GameManagerScript.instance.gridController.width);
	}

	void LateUpdate () {
		cameraController.LateUpdate();
	}
}
