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

	public IPlayerMovementController GetPlayerMovementControllerMock()
	{
		return Substitute.For<IPlayerMovementController>();
	}

	public IAnimatorController GetAnimatorControllerMock(){
		return Substitute.For<IAnimatorController>();
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
		return controller;
	}
}
