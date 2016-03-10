using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using System.Collections;
using System;

[TestFixture]
public class NavigationTests {

	[Test]
	public void CameraReset()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		AInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);
		Vector3 initPos;

		initPos = camCon.position;
		//Act
		camCon.position = new Vector3 (0.0f, 0.0f, 0.0f);
		inputM.ResetCamera ().ReturnsForAnyArgs (true);
		camCon.LateUpdate ();
		//Assert
		Assert.AreEqual(initPos, camCon.position);
	}

	[Test]
	public void CameraScrollUp()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		AInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);

		inputM.MousingOverGameObject().Returns(false);
		inputM.ScrollUp().Returns(true);
		//Act
		camCon.LateUpdate();
		//Assert
		camMov.Received().Move (Arg.Is<Vector3>(x => (x.y > 0 && x.z < 0)), CameraController.scrollSpeed);
	}

	[Test]
	public void CameraScrollUpOnObject()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		AInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);

		inputM.MousingOverGameObject().Returns(true);
		inputM.ScrollUp().Returns(true);
		//Act
		camCon.LateUpdate();
		//Assert
		camMov.DidNotReceiveWithAnyArgs().Move (Arg.Any <Vector3>(), Arg.Any<float>());
	}

	[Test]
	public void CameraScrollDown()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		AInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);

		inputM.MousingOverGameObject().Returns(false);
		inputM.ScrollDown().Returns(true);
		//Act
		camCon.LateUpdate();
		//Assert
		camMov.Received().Move (Arg.Is<Vector3>(x => (x.y < 0 && x.z > 0)), CameraController.scrollSpeed);
	}

	[Test]
	public void CameraScrollDownOnObject()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		AInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);

		inputM.MousingOverGameObject().Returns(true);
		inputM.ScrollDown().Returns(true);
		//Act
		camCon.LateUpdate();
		//Assert
		camMov.DidNotReceiveWithAnyArgs().Move (Vector3.zero, 0.0f);
	}

	[Test]
	public void CameraMoveLeft()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		AInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);

		inputM.MoveLeft().Returns(true);
		//Act
		camCon.LateUpdate();
		//Assert
		camMov.Received().Move (Arg.Is<Vector3>(x => (x.x < 0)), camCon.moveSpeed);
	}

	[Test]
	public void CameraMoveRight()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		AInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);

		inputM.MoveRight().Returns(true);
		//Act
		camCon.LateUpdate();
		//Assert
		camMov.Received().Move (Arg.Is<Vector3>(x => (x.x > 0)), camCon.moveSpeed);
	}

	[Test]
	public void CameraMoveForward()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		AInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);

		camCon.position = new Vector3(camCon.position.x, camCon.position.y, 0.0f);
		inputM.MoveForward().Returns(true);
		inputM.VerticalMovementValue().Returns(1.0f);
		//Act
		camCon.LateUpdate();
		//Assert
		camMov.Received().Move (Arg.Is<Vector3>(x => (x.z > 0)), camCon.moveSpeed);
	}

	[Test]
	public void CameraMoveBackward()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		AInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);
		
		camCon.position = new Vector3(camCon.position.x, camCon.position.y, 0.0f);
		inputM.MoveBackward().Returns(true);
		inputM.VerticalMovementValue().Returns(-1.0f);
		//Act
		camCon.LateUpdate();
		//Assert
		camMov.Received().Move (Arg.Is<Vector3>(x => (x.z < 0)), camCon.moveSpeed);
	}

	[Test]
	public void CameraMoveUp()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		AInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);

		inputM.MoveUp().Returns(true);
		//Act
		camCon.LateUpdate();
		//Assert
		camMov.Received().Move (Arg.Is<Vector3>(x => (x.y > 0)), camCon.moveSpeed);
	}

	[Test]
	public void CameraMoveDown()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		AInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);

		inputM.MoveDown().Returns(true);
		//Act
		camCon.LateUpdate();
		//Assert
		camMov.Received().Move (Arg.Is<Vector3>(x => (x.y < 0)), camCon.moveSpeed);
	}

	[Test]
	public void CameraSpeedUp()
	{
		//Arrange
		ICameraMovement camMov = GetMockCameraMovement ();
		AInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, 10, 10);
		float initSpeed = camCon.moveSpeed;

		inputM.DoubleMoveSpeed().Returns(true);
		//Act
		camCon.LateUpdate();
		//Assert
		Assert.Greater(camCon.moveSpeed, initSpeed);
	}

	[Test]
	public void CameraMoveToTarget()
	{
		//Arrange
		int height = 10;
		int width = 10;
		ICameraMovement camMov = GetMockCameraMovement ();
		AInputManager inputM = GetMockInputManager ();
		CameraController camCon = new CameraController(inputM, camMov, height, width);
		var target = GetMockClickTarget ();

		Vector3 targetPos = target.GetPosition();
		//Act
		camCon.target = target;
		camCon.LateUpdate();
		//Assert
		Assert.That (camCon.position == targetPos);
	}
	public AInputManager GetMockInputManager()
	{
		var im = Substitute.For<AInputManager> ();
		im.MousingOverGameObject ().Returns (false);
		return im;
	}

	public ICameraMovement GetMockCameraMovement()
	{
		ICameraMovement cam = Substitute.For<ICameraMovement> ();
		cam.GoTo (Vector3.zero).ReturnsForAnyArgs(x => { return (Vector3)x[0]; });
		return cam;
	}

	private ISquare GetMockSquare ()
	{
		return Substitute.For<ISquare> ();
	}
	
	private IGrid GetMockSquareInstantiationController ()
	{
		return Substitute.For<IGrid> ();
	}

	private IClickTarget GetMockClickTarget()
	{
		return Substitute.For<IClickTarget>();
	}
}
