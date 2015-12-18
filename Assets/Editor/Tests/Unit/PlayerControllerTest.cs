using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using System.Collections;

[TestFixture]
public class PlayerControllerTest : MonoBehaviour {
	[Test]
	public void Incantate_Test()
	{
		// Arrange
		var animatorController = GetAnimatorControllerMock();
		var controller = GetPlayerControllerMock (animatorController);
		// Act
		controller.Incantate();
		// Assert
		animatorController.Received().SetBool ("Incantate", true);
	}

	[Test]
	public void StopIncantating_Test()
	{
		// Arrange
		var animatorController = GetAnimatorControllerMock ();
		var controller = GetPlayerControllerMock (animatorController);
		// Act
		controller.StopIncantating();
		// Assert
		animatorController.Received ().SetBool ("Incantate", false);
	}

	[Test]
	public void SetPosition_Test_SamePosition()
	{
		// Arrange
		var movementController = GetPlayerMovementControllerMock();
		var controller = GetPlayerControllerMock (movementController);
		// Act
		controller.SetPosition (0, 0);
		// Assert
		movementController.DidNotReceive().SetDestination (Arg.Any<int>(), Arg.Any<int>());
	}

	[Test]
	public void SetPosition_Test_DifferentPosition()
	{
		// Arrange
		var movementController = GetPlayerMovementControllerMock();
		var controller = GetPlayerControllerMock (movementController);
		// Act
		controller.SetPosition (3, 3);
		// Assert
		movementController.Received ().SetDestination (3, 3);
	}

	[Test]
	public void GetDestinationOrientation_Test_Orientation()
	{
		// Arrange
		var controller = GetPlayerControllerMock ();
		Orientation expected_result0 = Orientation.NONE;
		Orientation expected_result1 = Orientation.NORTH;
		Orientation expected_result2 = Orientation.EAST;
		Orientation expected_result3 = Orientation.SOUTH;
		Orientation expected_result4 = Orientation.WEST;
		// Act
		Orientation result0 = controller.GetDestinationOrientation (Vector3.zero, Vector3.zero);
		Orientation result1 = controller.GetDestinationOrientation (Vector3.zero, new Vector3(0, 1, 0));
		Orientation result2 = controller.GetDestinationOrientation (Vector3.zero, new Vector3(1, 0, 0));
		Orientation result3 = controller.GetDestinationOrientation (Vector3.zero, new Vector3(0, -1, 0));
		Orientation result4 = controller.GetDestinationOrientation (Vector3.zero, new Vector3(-1, 0, 0));

		// Assert
		Assert.AreEqual (expected_result0, result0);
		Assert.AreEqual (expected_result1, result1);
		Assert.AreEqual (expected_result2, result2);
		Assert.AreEqual (expected_result3, result3);
		Assert.AreEqual (expected_result4, result4);
	}

	[Test]
	public void GetAnimationOrientation_Test_Orientation()
	{
		// Arrange
		var controller = GetPlayerControllerMock ();
		Orientation expected_result0 = Orientation.NORTH;
		Orientation expected_result1 = Orientation.WEST;
		Orientation expected_result2 = Orientation.SOUTH;
		Orientation expected_result3 = Orientation.EAST;
		Orientation expected_result4 = Orientation.EAST;
		Orientation expected_result5 = Orientation.NORTH;
		Orientation expected_result6 = Orientation.WEST;
		Orientation expected_result7 = Orientation.SOUTH;
		Orientation expected_result8 = Orientation.SOUTH;
		Orientation expected_result9 = Orientation.EAST;
		Orientation expected_result10 = Orientation.NORTH;
		Orientation expected_result11 = Orientation.WEST;
		Orientation expected_result12 = Orientation.WEST;
		Orientation expected_result13 = Orientation.SOUTH;
		Orientation expected_result14 = Orientation.EAST;
		Orientation expected_result15 = Orientation.NORTH;
		// Act
		Orientation result0 = controller.GetAnimationOrientation(Orientation.NORTH, Orientation.NORTH);
		Orientation result1 = controller.GetAnimationOrientation(Orientation.NORTH, Orientation.EAST);
		Orientation result2 = controller.GetAnimationOrientation(Orientation.NORTH, Orientation.SOUTH);
		Orientation result3 = controller.GetAnimationOrientation(Orientation.NORTH, Orientation.WEST);
		Orientation result4 = controller.GetAnimationOrientation(Orientation.EAST, Orientation.NORTH);
		Orientation result5 = controller.GetAnimationOrientation(Orientation.EAST, Orientation.EAST);
		Orientation result6 = controller.GetAnimationOrientation(Orientation.EAST, Orientation.SOUTH);
		Orientation result7 = controller.GetAnimationOrientation(Orientation.EAST, Orientation.WEST);
		Orientation result8 = controller.GetAnimationOrientation(Orientation.SOUTH, Orientation.NORTH);
		Orientation result9 = controller.GetAnimationOrientation(Orientation.SOUTH, Orientation.EAST);
		Orientation result10 = controller.GetAnimationOrientation(Orientation.SOUTH, Orientation.SOUTH);
		Orientation result11 = controller.GetAnimationOrientation(Orientation.SOUTH, Orientation.WEST);
		Orientation result12 = controller.GetAnimationOrientation(Orientation.WEST, Orientation.NORTH);
		Orientation result13 = controller.GetAnimationOrientation(Orientation.WEST, Orientation.EAST);
		Orientation result14 = controller.GetAnimationOrientation(Orientation.WEST, Orientation.SOUTH);
		Orientation result15 = controller.GetAnimationOrientation(Orientation.WEST, Orientation.WEST);
		// Assert
		Assert.AreEqual (expected_result0, result0);
		Assert.AreEqual (expected_result1, result1);
		Assert.AreEqual (expected_result2, result2);
		Assert.AreEqual (expected_result3, result3);
		Assert.AreEqual (expected_result4, result4);
		Assert.AreEqual (expected_result5, result5);
		Assert.AreEqual (expected_result6, result6);
		Assert.AreEqual (expected_result7, result7);
		Assert.AreEqual (expected_result8, result8);
		Assert.AreEqual (expected_result9, result9);
		Assert.AreEqual (expected_result10, result10);
		Assert.AreEqual (expected_result11, result11);
		Assert.AreEqual (expected_result12, result12);
		Assert.AreEqual (expected_result13, result13);
		Assert.AreEqual (expected_result14, result14);
		Assert.AreEqual (expected_result15, result15);
	}

	[Test]
	public void GoToDestination_Test_North()
	{
		// Arrange
		var movementController = GetPlayerMovementControllerMock();
		var animatorController = GetAnimatorControllerMock ();
		var controller = GetPlayerControllerMock (animatorController, movementController);
		// Act
		controller.GoToDestination (Orientation.NORTH);
		// Assert
		animatorController.Received ().SetBool ("Walk", true);
		animatorController.Received ().SetInteger ("Orientation", 1);
		movementController.Received ().MoveToDestination (Arg.Any<float>());
	}

	[Test]
	public void GoToDestination_Test_East()
	{
		// Arrange
		var movementController = GetPlayerMovementControllerMock();
		var animatorController = GetAnimatorControllerMock ();
		var controller = GetPlayerControllerMock (animatorController, movementController);
		// Act
		controller.GoToDestination (Orientation.EAST);
		// Assert
		animatorController.Received ().SetBool ("Walk", true);
		animatorController.Received ().SetInteger ("Orientation", 2);
		movementController.Received ().MoveToDestination (Arg.Any<float>());
	}

	[Test]
	public void GoToDestination_Test_South()
	{
		// Arrange
		var movementController = GetPlayerMovementControllerMock();
		var animatorController = GetAnimatorControllerMock ();
		var controller = GetPlayerControllerMock (animatorController, movementController);
		// Act
		controller.GoToDestination (Orientation.SOUTH);
		// Assert
		animatorController.Received ().SetBool ("Walk", true);
		animatorController.Received ().SetInteger ("Orientation", 3);
		movementController.Received ().MoveToDestination (Arg.Any<float>());
	}

	[Test]
	public void GoToDestination_Test_West()
	{
		// Arrange
		var movementController = GetPlayerMovementControllerMock();
		var animatorController = GetAnimatorControllerMock ();
		var controller = GetPlayerControllerMock (animatorController, movementController);
		// Act
		controller.GoToDestination (Orientation.WEST);
		// Assert
		animatorController.Received ().SetBool ("Walk", true);
		animatorController.Received ().SetInteger ("Orientation", 4);
		movementController.Received ().MoveToDestination (Arg.Any<float>());
	}

	public IPlayerMovementController GetPlayerMovementControllerMock()
	{
		return Substitute.For<IPlayerMovementController>();
	}

	public IAnimatorController GetAnimatorControllerMock(){
		return Substitute.For<IAnimatorController>();
	}

	public PlayerController GetPlayerControllerMock()
	{
		return Substitute.For<PlayerController>();
	}

	public PlayerController GetPlayerControllerMock(IPlayerMovementController movementController)
	{
		var controller = Substitute.For<PlayerController>();
		controller.SetPlayerMovementController(movementController);
		return controller;
	}

	public PlayerController GetPlayerControllerMock(IAnimatorController animatorController)
	{
		var controller = Substitute.For<PlayerController>();
		controller.SetAnimatorController(animatorController);
		return controller;
	}

	public PlayerController GetPlayerControllerMock(IAnimatorController animatorController, IPlayerMovementController movementController)
	{
		var controller = Substitute.For<PlayerController>();
		controller.SetAnimatorController(animatorController);
		controller.SetPlayerMovementController(movementController);
		movementController.IsMoving ().Returns(true);
		return controller;
	}
}
