using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using System.Collections;

[TestFixture]
public class ServerCommandsTests {
	[Test]
	public void SendMapSizeTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager ());
		//Act
		sc.SendMapSize ("msz 12 12\n");
		//Assert
		sc.gridController.Received ().Init (12, 12);
	}

	[Test]
	public void SendSquareContentTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager ());
		sc.gridController.GetSquare (3, 3).ReturnsForAnyArgs (GetSquareMock ());
		//Act
		sc.SendSquareContent ("bct 3 3 1 1 1 1 1 1 1\n");
		//Assert
		sc.gridController.GetSquare (3, 3).Received().SetResources (1, 1, 1, 1, 1, 1, 1);
	}

	[Test]
	public void SendTeamNameTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager ());
		//Act
		sc.SendTeamName ("tna trololo\n");
		//Assert
		sc.teamManager.Received ().createTeam ("trololo");
	}

	[Test]
	public void SendPlayerConnectionTest ()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager ());
		//Act
		sc.SendPlayerConnection("pnw #3 5 5 1 1 trololo\n");
		//Assert
		sc.playerManager.Received ().SetPlayerConnection (3, 5, 5, (Orientation)1, 1, "trololo");
	}

	[Test]
	public void SendPlayerPositionTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager ());
		//Act
		sc.SendPlayerPosition ("ppo #3 5 5 1\n");
		//Assert
		sc.playerManager.Received ().SetPlayerPosition (3, 5, 5, (Orientation)1);
	}

	[Test]
	public void SendPlayerLevelTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager ());
		//Act
		sc.SendPlayerLevel ("plv #3 1\n");
		//Assert
		sc.playerManager.Received ().SetPlayerLevel (3, 1);
	}

	[Test]
	public void SendPlayerInventoryTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager ());
		//Act
		sc.SendPlayerInventory ("pin #3 5 5 1 1 1 1 1 1 1\n");
		//Assert
		sc.playerManager.Received ().SetPlayerInventory (3, 1, 1, 1, 1, 1, 1, 1);
	}

	[Test]
	public void SendPlayerExpulsionTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager ());
		//Act
		sc.SendPlayerExpulsion ("pex #3\n");
		//Assert
		sc.playerManager.Received ().SetPlayerExpulse (3);
	}

	[Test]
	public void SendBroadcastTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager ());
		//Act
		sc.SendBroadcast ("pbc #3 trololo\n");
		//Assert
		sc.playerManager.Received ().SetPlayerBroadcast (3, "trololo");
	}

	[Test]
	public void SendIncantationStartTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager ());
		//Act
		sc.SendIncantationStart("pic 5 5 2 #3 #4 #5\n");
		//Assert
		sc.playerManager.Received ().SetPlayerIncantatePrimary (3);
		sc.playerManager.Received ().SetPlayerIncantateSecondary (4);
		sc.playerManager.Received ().SetPlayerIncantateSecondary (5);
	}

	[Test]
	public void SendIncantationStopTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager ());
		SquareContent squareC = new SquareContent ();
		squareC.players.Add (GetMockPlayerController (GetAnimatorControllerMock()));
		sc.gridController.GetSquare (5, 5).ReturnsForAnyArgs (Substitute.For<ISquare> ());
		sc.gridController.GetSquare (5, 5).GetResources().ReturnsForAnyArgs(squareC);
		//Act
		sc.SendIncantationStop ("pie 5 5 1\n");
		//Assert
		sc.playerManager.Received ().SetPlayersStopIncantate (5, 5);
	}

	public GridController GetMockGridController()
	{
		GridController gridCon = Substitute.For<GridController> ();
		gridCon.SetSquareInstantiationController (Substitute.For<IGrid> ());
		return gridCon;
	}

	public TeamManager GetMockTeamManager()
	{
		return (Substitute.For<TeamManager> ());
	}

	public PlayerManagerScript GetMockPlayerManager()
	{
		return (Substitute.For<PlayerManagerScript> (GetMockGridController(), GetMockTeamManager(), GetMockPlayerInstantiation(), GetMockEggInstantiation()));
	}

	public TimeManager GetMockTimeManager()
	{
		return (Substitute.For<TimeManager> ());
	}

	public IPlayerInstantiationController GetMockPlayerInstantiation()
	{
		return (Substitute.For<IPlayerInstantiationController> ());
	}

	public IEggInstantiationController GetMockEggInstantiation()
	{
		return (Substitute.For<IEggInstantiationController> ());
	}

	public ISquare GetSquareMock()
	{
		return (Substitute.For<ISquare> ());
	}

	public PlayerController GetMockPlayerController(IAnimatorController animatorController)
	{
		var controller = Substitute.For<PlayerController>();
		controller.SetAnimatorController(animatorController);
		return controller;
	}

	public IAnimatorController GetAnimatorControllerMock()
	{
		return Substitute.For<IAnimatorController>();
	}
}
