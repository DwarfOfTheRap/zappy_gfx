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
		v
		// Act

		// Assert
	}

	public IPlayerMovementController GetPlayerMovementControllerMock()
	{
		return Substitute.For<IPlayerMovementController>();
	}

	public IAnimatorController GetAnimatorControllerMock()
	{
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
