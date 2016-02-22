using UnityEngine;
using System.Collections;

[System.Serializable]
public class CameraController {
	public AInputManager		inputManager;
	public ICameraMovement		cameraMovement;
	
	public Vector3				position;
	private const float			doubleClickSpeed = 1.5f;
	
	private const float			startMoveSpeed = 5f;		
	public float				moveSpeed { get; private set; }
	public const float			scrollSpeed = 10f;
	
	private int					currentHeight;
	private int					currentWidth;
	public IClickTarget			target;
	
	private const float			squareSide = 5.0f;
	private const float			downBoundary = 3.5f;
	private const float			upBoundary = 103.5f;
	
	private CameraController () {}
	
	public CameraController (AInputManager inputManager, ICameraMovement cameraMovement, int currentHeight, int currentWidth)
	{
		this.currentHeight = currentHeight;
		this.currentWidth = currentWidth;
		this.inputManager = inputManager;
		this.cameraMovement = cameraMovement;
		this.inputManager.OnDoubleClicking += CheckDoubleClick;
		this.moveSpeed = startMoveSpeed;
		InitCameraPosition();
	}
	
	public void InitCameraPosition()
	{
		position = cameraMovement.GoTo(new Vector3 (0.0f + (squareSide / 2) * (currentHeight - 1), 25.0f + (1.875f * (currentWidth - 10)), -31.5f - (1.675f * (currentWidth - 10))));
		cameraMovement.Rotate(Quaternion.Euler(new Vector3(28.0f, 0.0f, 0.0f)));
	}
	
	void GoToTarget ()
	{
		if (target != null)
		{
			position = cameraMovement.LerpMove (new Vector3 (target.GetPosition().x, target.GetPosition().y + 16.5f, target.GetPosition().z - 28.0f), doubleClickSpeed);
		}
	}
	
	void CheckMouseScrollDown()
	{
		bool mousingOver = inputManager.MousingOverGameObject();
		
		if (inputManager.ScrollDown() && position.y > downBoundary && mousingOver == false)
		{
			target = null;
			position = cameraMovement.Move (Vector3.down + Vector3.forward, scrollSpeed);
		}
	}
	
	void CheckMouseScrollUp()
	{
		bool mousingOver = inputManager.MousingOverGameObject();
		
		if (inputManager.ScrollUp() && position.y < upBoundary && mousingOver == false)
		{
			target = null;
			position = cameraMovement.Move (Vector3.up + Vector3.back, scrollSpeed);
		}
	}
	
	void CheckForwardInput() {
		if (inputManager.MoveForward() && position.z < (40.0f + (squareSide * (currentWidth - 10.0f)) - (position.y - downBoundary)))
		{
			target = null;
			position = cameraMovement.Move (Vector3.forward * inputManager.VerticalMovementValue (), moveSpeed);
		}
	}
	
	void CheckBackwardInput() {
		if (inputManager.MoveBackward() && position.z > -10.0f - (position.y - downBoundary))
		{
			target = null;
			position = cameraMovement.Move (Vector3.forward * inputManager.VerticalMovementValue (), moveSpeed);
		}
	}
	
	void CheckLeftInput() {
		if (inputManager.MoveLeft() && position.x > -(squareSide / 2))
		{
			target = null;
			position = cameraMovement.Move (Vector3.left, moveSpeed);
		}
	}
	
	void CheckRightInput() {
		if (inputManager.MoveRight() && position.x < ((currentHeight * squareSide) - (squareSide / 2)))
		{
			target = null;
			position = cameraMovement.Move (Vector3.right, moveSpeed);
		}
	}
	
	void CheckUpwardInput() {
		if (inputManager.MoveUp() && position.y < upBoundary)
		{
			target = null;
			position = cameraMovement.Move (Vector3.up, moveSpeed);
			if (position.z > (40.0f + (squareSide * (currentWidth - 10.0f)) - (position.y - downBoundary)))
				position = cameraMovement.GoTo (new Vector3 (position.x, position.y, 40.0f + (squareSide * (currentWidth - 10.0f)) - (position.y - downBoundary)));
		}
	}
	
	void CheckDownwardInput() {
		if (inputManager.MoveDown () && position.y > downBoundary)
		{
			target = null;
			
			position = cameraMovement.Move (Vector3.down, moveSpeed);
			if (position.z < -10.0f - (position.y - downBoundary))
				position = cameraMovement.GoTo (new Vector3 (position.x, position.y, -10.0f - (position.y - downBoundary)));
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
			moveSpeed = startMoveSpeed * 2;
		}
	}
	
	void CheckSlowDown() {
		if (inputManager.StandardMoveSpeed())
		{
			moveSpeed = startMoveSpeed;
		}
	}
	
	void CheckDoubleClick(ClickEventArgs args) {
		target = args.target;
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
