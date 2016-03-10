using UnityEngine;
using System.Collections;

[System.Serializable]
public class CameraController {
	public AInputManager		inputManager { get; private set;}
	public ICameraMovement		cameraMovement { get; private set;}

	public IClickTarget			target;
	public Vector3				position;

	public float				moveSpeed { get; private set; }
	private const float			_doubleClickSpeed = 1.5f;
	private const float			_startMoveSpeed = 20f;
	public const float			scrollSpeed = 30f;

	private int					_currentHeight;
	private int					_currentWidth;
	
	private const float			_squareSide = 5.0f;
	private const float			_downBoundary = 3.5f;
	private const float			_upBoundary = 103.5f;

	CameraController () {}

	public CameraController (AInputManager inputManager, ICameraMovement cameraMovement, int currentHeight, int currentWidth)
	{
		this._currentHeight = currentHeight;
		this._currentWidth = currentWidth;
		this.inputManager = inputManager;
		this.cameraMovement = cameraMovement;
		this.inputManager.OnDoubleClicking += CheckDoubleClick;
		this.inputManager.OnRightClicking += CheckRightClick;
		this.moveSpeed = _startMoveSpeed;
		InitCameraPosition();
	}

	void InitCameraPosition()
	{
		position = cameraMovement.GoTo(new Vector3 (0.0f + (_squareSide / 2) * (_currentHeight - 1), 25.0f + (1.875f * (_currentWidth - 10)), -31.5f - (1.675f * (_currentWidth - 10))));
		cameraMovement.Rotate(Quaternion.Euler(new Vector3(28.0f, 0.0f, 0.0f)));
	}

	void GoToTarget ()
	{
		if (target != null)
		{
			position = cameraMovement.LerpMove (new Vector3 (target.GetPosition().x, target.GetPosition().y + 10.89f, target.GetPosition().z - 18.48f), _doubleClickSpeed);
		}
	}

	void CheckMouseScrollDown()
	{
		bool mousingOver = inputManager.MousingOverGameObject();

		if (inputManager.ScrollDown() && position.y > _downBoundary && mousingOver == false)
		{
			target = null;
			position = cameraMovement.Move (Vector3.down + Vector3.forward, scrollSpeed);
		}
	}
	
	void CheckMouseScrollUp()
	{
		bool mousingOver = inputManager.MousingOverGameObject();
		
		if (inputManager.ScrollUp() && position.y < _upBoundary && mousingOver == false)
		{
			target = null;
			position = cameraMovement.Move (Vector3.up + Vector3.back, scrollSpeed);
		}
	}

	void CheckForwardInput() {
		if (inputManager.MoveForward() && position.z < (40.0f + (_squareSide * (_currentWidth - 10.0f)) - (position.y - _downBoundary)))
		{
			target = null;
			position = cameraMovement.Move (Vector3.forward * inputManager.VerticalMovementValue (), moveSpeed);
		}
	}

	void CheckBackwardInput() {
		if (inputManager.MoveBackward() && position.z > -10.0f - (position.y - _downBoundary))
		{
			target = null;
			position = cameraMovement.Move (Vector3.forward * inputManager.VerticalMovementValue (), moveSpeed);
		}
	}

	void CheckLeftInput() {
		if (inputManager.MoveLeft() && position.x > -(_squareSide / 2))
		{
			target = null;
			position = cameraMovement.Move (Vector3.left, moveSpeed);
		}
	}

	void CheckRightInput() {
		if (inputManager.MoveRight() && position.x < ((_currentHeight * _squareSide) - (_squareSide / 2)))
		{
			target = null;
			position = cameraMovement.Move (Vector3.right, moveSpeed);
		}
	}

	void CheckUpwardInput() {
		if (inputManager.MoveUp() && position.y < _upBoundary)
		{
			target = null;
			position = cameraMovement.Move (Vector3.up, moveSpeed);
			if (position.z > (40.0f + (_squareSide * (_currentWidth - 10.0f)) - (position.y - _downBoundary)))
				position = cameraMovement.GoTo (new Vector3 (position.x, position.y, 40.0f + (_squareSide * (_currentWidth - 10.0f)) - (position.y - _downBoundary)));
		}
	}

	void CheckDownwardInput() {
		if (inputManager.MoveDown () && position.y > _downBoundary)
		{
			target = null;
			
			position = cameraMovement.Move (Vector3.down, moveSpeed);
			if (position.z < -10.0f - (position.y - _downBoundary))
				position = cameraMovement.GoTo (new Vector3 (position.x, position.y, -10.0f - (position.y - _downBoundary)));
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
			moveSpeed = _startMoveSpeed * 1.5f;
		}
	}

	void CheckSlowDown() {
		if (inputManager.StandardMoveSpeed())
		{
			moveSpeed = _startMoveSpeed;
		}
	}

	void CheckDoubleClick(ClickEventArgs args) {
		target = args.target;
	}

	void CheckRightClick ()
	{
		target = null;
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
		CheckMouseScrollUp();
		CheckMouseScrollDown();
		CheckKeyboardInput();
		GoToTarget();
	}
}
