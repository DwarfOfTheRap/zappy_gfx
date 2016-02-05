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

	private const float			squareSide = 5.0f;
	private const float			downBoundary = 3.5f;
	private const float			upBoundary = 103.5f;

	public CameraController (IInputManager inputManager, ICameraMovement cameraMovement, int currentHeight, int currentWidth)
	{
		this.currentHeight = currentHeight;
		this.currentWidth = currentWidth;
		this.inputManager = inputManager;
		this.cameraMovement = cameraMovement;
		InputManager.OnDoubleClick += CheckDoubleClick;
		InitCameraPosition();
	}

	public void InitCameraPosition()
	{
		position = cameraMovement.Move(new Vector3 (0.0f + (squareSide / 2) * (currentHeight - 1), 25.0f + (1.875f * (currentWidth - 10)), -31.5f - (1.675f * (currentWidth - 10))));
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
		bool mousingOver = inputManager.MousingOverGameObject();
		
		if (inputManager.ScrollUp() && position.y > downBoundary && mousingOver == false)
		{
			target = null;
			position = cameraMovement.Move (new Vector3 (position.x, position.y - scrollSpeed, position.z + scrollSpeed));
		}
		if (inputManager.ScrollDown() && position.y < upBoundary && mousingOver == false)
		{
			target = null;
			position = cameraMovement.Move (new Vector3 (position.x, position.y + scrollSpeed, position.z - scrollSpeed));
		}
	}

	void CheckForwardInput() {
		if (inputManager.MoveForward() && position.z < (40.0f + (squareSide * (currentWidth - 10.0f)) - (position.y - downBoundary)))
		{
			target = null;
			position = cameraMovement.Move (new Vector3 (position.x, position.y, position.z + (inputManager.VerticalMovementValue() * moveSpeed)));
		}
	}

	void CheckBackwardInput() {
		if (inputManager.MoveBackward() && position.z > -10.0f - (position.y - downBoundary))
		{
			target = null;
			position = cameraMovement.Move (new Vector3 (position.x, position.y, position.z + (inputManager.VerticalMovementValue() * moveSpeed)));
		}
	}

	void CheckLeftInput() {
		if (inputManager.MoveLeft() && position.x > -(squareSide / 2))
		{
			target = null;
			position = cameraMovement.Move (new Vector3 (position.x - moveSpeed, position.y, position.z));
		}
	}

	void CheckRightInput() {
		if (inputManager.MoveRight() && position.x < ((currentHeight * squareSide) - (squareSide / 2)))
		{
			target = null;
			position = cameraMovement.Move (new Vector3 (position.x + moveSpeed, position.y, position.z));
		}
	}

	void CheckUpwardInput() {
		if (inputManager.MoveUp() && position.y < upBoundary)
		{
			target = null;
			position = cameraMovement.Move (new Vector3 (position.x, position.y + moveSpeed, position.z));
			if (position.z > (40.0f + (squareSide * (currentWidth - 10.0f)) - (position.y - downBoundary)))
				position = cameraMovement.Move (new Vector3 (position.x, position.y, 40.0f + (squareSide * (currentWidth - 10.0f)) - (position.y - downBoundary)));
		}
	}

	void CheckDownwardInput() {
		if (inputManager.MoveDown () && position.y > downBoundary)
		{
			target = null;
			position = cameraMovement.Move (new Vector3 (position.x, position.y - moveSpeed, position.z));
			if (position.z < -10.0f - (position.y - downBoundary))
				position = cameraMovement.Move (new Vector3 (position.x, position.y, -10.0f - (position.y - downBoundary)));
		}
	}

	void CheckCameraReset() {
		if (inputManager.ResetCamera())
		{
			target = null;
			InitCameraPosition();
		}
	}

	void CheckSpeedUp() {
		if (inputManager.DoubleMoveSpeed())
		{
			moveSpeed = 1.0f;
		}
	}

	void CheckSlowDown() {
		if (inputManager.StandardMoveSpeed())
		{
			moveSpeed = 0.5f;
		}
	}

	void CheckDoubleClick(ISquare square) {
		target = square;
	}

	void CheckKeyboardInput()
	{
		CheckForwardInput();
		CheckBackwardInput();
		CheckLeftInput();
		CheckRightInput();
		CheckUpwardInput();
		CheckDownwardInput();
		CheckCameraReset();
		CheckSpeedUp();
		CheckSlowDown();
	}

	public void LateUpdate () {
		CheckMouseScroll();
		CheckKeyboardInput();
		GoToTarget();
	}
}
