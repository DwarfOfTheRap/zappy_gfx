using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using System.Collections;

[TestFixture]
public class NavigationTests {

	[Test]
	public void CameraReset()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		IInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);
		Vector3 initPos;

		initPos = camCon.position;
		//Act
		camCon.position = new Vector3 (0.0f, 0.0f, 0.0f);
		camCon.InitCameraPosition();
		//Assert
		Assert.AreEqual(initPos, camCon.position);
	}

	[Test]
	public void CameraScrollUp()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		IInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);
		Vector3 initPos;

		initPos = camCon.position;
		inputM.MousingOverGameObject().Returns(false);
		inputM.ScrollUp().Returns(true);
		//Act
		camCon.LateUpdate();
		//Assert
		camMov.Received().Move (Arg.Is<Vector3>(x => (x.y > initPos.y && x.z < initPos.z)));
	}

	[Test]
	public void CameraScrollUpOnObject()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		IInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);
		Vector3 initPos;
		
		initPos = camCon.position;
		inputM.MousingOverGameObject().Returns(true);
		inputM.ScrollUp().Returns(true);
		//Act
		camCon.LateUpdate();
		//Assert
		camMov.DidNotReceive().Move (Arg.Is<Vector3>(x => (x.y > initPos.y && x.z < initPos.z)));
	}

	[Test]
	public void CameraScrollDown()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		IInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);
		Vector3 initPos;
		
		initPos = camCon.position;
		inputM.MousingOverGameObject().Returns(false);
		inputM.ScrollDown().Returns(true);
		//Act
		camCon.LateUpdate();
		//Assert
		camMov.Received().Move (Arg.Is<Vector3>(x => (x.y < initPos.y && x.z > initPos.z)));
	}

	[Test]
	public void CameraScrollDownOnObject()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		IInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);
		Vector3 initPos;
		
		initPos = camCon.position;
		inputM.MousingOverGameObject().Returns(true);
		inputM.ScrollDown().Returns(true);
		//Act
		camCon.LateUpdate();
		//Assert
		camMov.DidNotReceive().Move (Arg.Is<Vector3>(x => (x.y > initPos.y && x.z < initPos.z)));
	}

	[Test]
	public void CameraMoveLeft()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		IInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);
		Vector3 initPos;
		
		initPos = camCon.position;
		inputM.MoveLeft().Returns(true);
		//Act
		camCon.LateUpdate();
		//Assert
		camMov.Received().Move (Arg.Is<Vector3>(x => (x.x < initPos.x)));
	}

	[Test]
	public void CameraMoveRight()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		IInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);
		Vector3 initPos;
		
		initPos = camCon.position;
		inputM.MoveRight().Returns(true);
		//Act
		camCon.LateUpdate();
		//Assert
		camMov.Received().Move (Arg.Is<Vector3>(x => (x.x > initPos.x)));
	}

	[Test]
	public void CameraMoveForward()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		IInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);
		Vector3 initPos;
		
		initPos = camCon.position;
		inputM.MoveForward().Returns(true);
		inputM.VerticalMovementValue().Returns(1.0f);
		//Act
		camCon.LateUpdate();
		//Assert
		camMov.Received().Move (Arg.Is<Vector3>(x => (x.z > initPos.z)));
	}

	[Test]
	public void CameraMoveBackward()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		IInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);
		Vector3 initPos;
		
		camCon.position = new Vector3(camCon.position.x, camCon.position.y, 0.0f);
		initPos = camCon.position;
		inputM.MoveBackward().Returns(true);
		inputM.VerticalMovementValue().Returns(-1.0f);
		//Act
		camCon.LateUpdate();
		//Assert
		camMov.Received().Move (Arg.Is<Vector3>(x => (x.z < initPos.z)));
	}

	[Test]
	public void CameraMoveUp()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		IInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);
		Vector3 initPos;
		
		initPos = camCon.position;
		inputM.MoveUp().Returns(true);
		//Act
		camCon.LateUpdate();
		//Assert
		camMov.Received().Move (Arg.Is<Vector3>(x => (x.y > initPos.y)));
	}

	[Test]
	public void CameraMoveDown()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		IInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);
		Vector3 initPos;
		
		initPos = camCon.position;
		inputM.MoveDown().Returns(true);
		//Act
		camCon.LateUpdate();
		//Assert
		camMov.Received().Move (Arg.Is<Vector3>(x => (x.y < initPos.y)));
	}

	[Test]
	public void CameraSpeedUp()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		IInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);
		float initSpeed = 1.0f;

		inputM.DoubleMoveSpeed().Returns(true);
		//Act
		camCon.LateUpdate();
		//Assert
		Assert.AreEqual(camCon.moveSpeed, initSpeed);
	}

	[Test]
	public void CameraDoubleClick()
	{
		//Arrange
		GridController gc = new GridController ();
		int height = 10;
		int width = 10;
		ISquareInstantiationController sic = GetMockSquareInstantiationController ();
		ICameraMovement camMov = GetMockCameraMovement ();
		IInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, height, width);
		bool wasCalled = false;
		ISquare square;
		Vector3 targetPos;

		gc.SetSquareInstantiationController (sic);
		gc.Init (width, height);
		square = gc.GetSquare(3, 3);
		targetPos = square.GetPosition();
		inputM.DoubleLeftClick().Returns(true);
		//Act
		GameManagerScript.instance.inputManager.OnDoubleClick += (args) => wasCalled = true;
		GameManagerScript.instance.inputManager.OnDoubleClick += Raise.Event<InputManager.DoubleClickEvent>(this, square);
		camCon.LateUpdate();
		//Assert
		Assert.That (wasCalled && camCon.position == targetPos);
	}

	public IInputManager GetMockInputManager()
	{
		return Substitute.For<IInputManager> ();
	}

	public ICameraMovement GetMockCameraMovement()
	{
		ICameraMovement cam = Substitute.For<ICameraMovement> ();
		cam.Move (Vector3.zero).ReturnsForAnyArgs(x => { return (Vector3)x[0]; });
		return cam;
	}

	private ISquare GetMockSquare ()
	{
		return Substitute.For<ISquare> ();
	}
	
	private ISquareInstantiationController GetMockSquareInstantiationController ()
	{
		return Substitute.For<ISquareInstantiationController> ();
	}
}
