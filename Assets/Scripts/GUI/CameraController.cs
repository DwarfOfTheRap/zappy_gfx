using UnityEngine;
using System.Collections;

[System.Serializable]
public class CameraController {
	public IInputManager		inputManager;
	public ICameraMovement		cameraMovement;

	public Vector3				position;
	private float				doubleClickSpeed = 1.5f;

	private float				moveSpeed = 0.5f;
	private float				scrollSpeed = 1.0f;

	private int					currentHeight;
	private int					currentWidth;
	private ISquare				target = null;

	public CameraController (IInputManager inputManager, ICameraMovement cameraMovement, int currentHeight, int currentWidth)
	{
		this.currentHeight = currentHeight;
		this.currentWidth = currentWidth;
		this.inputManager = inputManager;
		this.cameraMovement = cameraMovement;
		InitCameraPosition();
	}

	public void InitCameraPosition()
	{
		position = cameraMovement.Move(new Vector3 (0.0f + 2.5f * (currentHeight - 1), 25.0f + (1.875f * (currentWidth - 10)), -31.5f - (1.675f * (currentWidth - 10))));
		cameraMovement.Rotate(Quaternion.Euler(new Vector3(28.0f, 0.0f, 0.0f)));
	}

	void GoToTarget ()
	{
		if (target != null)
		{
			position = cameraMovement.LerpMove (new Vector3 (target.GetPosition().x, target.GetPosition().y + 16.5f, target.GetPosition().z - 28.0f), doubleClickSpeed);
		}
	}

	void CheckMouseScroll()
	{
		//bool mousingOver = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
		bool mousingOver = inputManager.MousingOverGameObject();
		
		if (inputManager.ScrollUp() && position.y > 3.5f && mousingOver == false)
		{
			target = null;
			position = cameraMovement.Move (new Vector3 (position.x, position.y - scrollSpeed, position.z + scrollSpeed));
		}
		if (inputManager.ScrollDown() && position.y < 100.0f && mousingOver == false)
		{
			target = null;
			position = cameraMovement.Move (new Vector3 (position.x, position.y + scrollSpeed, position.z - scrollSpeed));
		}
	}

	void CheckForwardInput() {
		if (inputManager.MoveForward() && position.z < (40.0f + (5.0f * (currentWidth - 10.0f)) - (position.y - 3.5f)))
		{
			target = null;
			position = cameraMovement.Move (new Vector3 (position.x, position.y, position.z + (inputManager.ForwardMovementValue() * moveSpeed)));
		}
	}

	void CheckKeyboardInput()
	{
		CheckForwardInput();
	
	/*	if (Input.GetAxis("Horizontal") < 0 && position.x > -2.5f)
		{
			target = null;
			position = new Vector3 (position.x - moveSpeed, position.y, position.z);
		}
		if (Input.GetAxis("Vertical") < 0 && position.z > -10.0f - (position.y - 3.5f))
		{
			target = null;
			position = new Vector3 (position.x, position.y, position.z + (Input.GetAxis("Vertical") * moveSpeed));
		}
		if (Input.GetAxis("Horizontal") > 0 && position.x < ((currentHeight * 5.0f) - 2.5f))
		{
			target = null;
			position = new Vector3 (position.x + moveSpeed, position.y, position.z);
		}
		if (Input.GetKey(KeyCode.Q) && position.y < 103.5f)
		{
			target = null;
			position = new Vector3 (position.x, position.y + moveSpeed, position.z);
			if (position.z > (40.0f + (5.0f * (currentWidth - 10.0f)) - (position.y - 3.5f)))
				position = new Vector3 (position.x, position.y, 40.0f + (5.0f * (currentWidth - 10.0f)) - (position.y - 3.5f));
		}
		if (Input.GetKey(KeyCode.E) && position.y > 3.5f)
		{
			target = null;
			position = new Vector3 (position.x, position.y - moveSpeed, position.z);
			if (position.z < -10.0f - (position.y - 3.5f))
				position = new Vector3 (position.x, position.y, -10.0f - (position.y - 3.5f));
		}
		if (Input.GetKeyUp(KeyCode.Space))
		{
			target = null;
			InitCameraPosition();
		}
		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			moveSpeed = 1.0f;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			moveSpeed = 0.5f;
		}*/
	}

	public void LateUpdate () {
		CheckMouseScroll();
		CheckKeyboardInput();
		GoToTarget();
	}
}
