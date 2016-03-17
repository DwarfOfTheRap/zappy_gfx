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
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager (), GetMockDebugManager(), GetMockLevelLoader());
		//Act
		sc.SendMapSize ("msz 12 12");
		//Assert
		sc.levelLoader.Received ().LoadLevel (12, 12);
	}

	[Test]
	public void SendSquareContentTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager (), GetMockDebugManager(), null);
		sc.gridController.GetSquare (3, 3).ReturnsForAnyArgs (GetMockSquare ());
		//Act
		sc.SendSquareContent ("bct 3 3 1 1 1 1 1 1 1");
		//Assert
		sc.gridController.GetSquare (3, 3).Received().SetResources (1, 1, 1, 1, 1, 1, 1);
	}

	[Test]
	public void SendTeamNameTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager (), GetMockDebugManager(), null);
		//Act
		sc.SendTeamName ("tna trololo");
		//Assert
		sc.teamManager.Received ().CreateTeam ("trololo");
	}

	[Test]
	public void SendPlayerConnectionTest ()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager (), GetMockDebugManager(), null);
		//Act
		sc.SendPlayerConnection("pnw 3 5 5 1 1 trololo");
		//Assert
		sc.playerManager.Received ().SetPlayerConnection (3, 5, 5, (Orientation)1, 1, "trololo");
	}

	[Test]
	public void SendPlayerPositionTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager (), GetMockDebugManager(), null);
		//Act
		sc.SendPlayerPosition ("ppo 3 5 5 1");
		//Assert
		sc.playerManager.Received ().SetPlayerPosition (3, 5, 5, (Orientation)1);
	}

	[Test]
	public void SendPlayerLevelTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager (), GetMockDebugManager(), null);
		//Act
		sc.SendPlayerLevel ("plv 3 1");
		//Assert
		sc.playerManager.Received ().SetPlayerLevel (3, 1);
	}

	[Test]
	public void SendPlayerInventoryTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager (), GetMockDebugManager(), null);
		//Act
		sc.SendPlayerInventory ("pin 3 5 5 1 1 1 1 1 1 1");
		//Assert
		sc.playerManager.Received ().SetPlayerInventory (3, 1, 1, 1, 1, 1, 1, 1);
	}

	[Test]
	public void SendPlayerExpulsionTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager (), GetMockDebugManager(), null);
		//Act
		sc.SendPlayerExpulsion ("pex 3");
		//Assert
		sc.playerManager.Received ().SetPlayerExpulse (3);
	}

	[Test]
	public void SendBroadcastTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager (), GetMockDebugManager(), null);
		//Act
		sc.SendBroadcast ("pbc 3 trololo");
		//Assert
		sc.playerManager.Received ().SetPlayerBroadcast (3, "trololo");
	}

	[Test]
	public void SendIncantationStartTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager (), GetMockDebugManager(), null);
		//Act
		sc.SendIncantationStart("pic 5 5 2 3 4 5");
		//Assert
		sc.playerManager.Received ().SetPlayerIncantatePrimary (3);
		sc.playerManager.Received ().SetPlayerIncantateSecondary (4);
		sc.playerManager.Received ().SetPlayerIncantateSecondary (5);
	}

	[Test]
	public void SendIncantationStopTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager (), GetMockDebugManager(), null);
		SquareContent squareC = new SquareContent ();
		squareC.players.Add (GetMockPlayerController (GetMockAnimatorController ()));
		sc.gridController.GetSquare (5, 5).ReturnsForAnyArgs (Substitute.For<ISquare> ());
		sc.gridController.GetSquare (5, 5).GetResources().ReturnsForAnyArgs(squareC);
		//Act
		sc.SendIncantationStop ("pie 5 5 1");
		//Assert
		sc.playerManager.Received ().SetPlayersStopIncantate (5, 5, 1);
	}

	[Test]
	public void SendLayEggTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager (), GetMockDebugManager(), null);
		//Act
		sc.SendLayEgg("pfk 3");
		//Assert
		sc.playerManager.Received().SetPlayerLayEgg(3);
	}

	[Test]
	public void SendThrowResourceTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager (), GetMockDebugManager(), null);
		//Act
		sc.SendThrowResource("pdr 3 3");
		//Assert
		sc.playerManager.Received().SetPlayerThrowResource(3, 3);
	}

	[Test]
	public void SendTakeResourceTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager (), GetMockDebugManager(), null);
		//Act
		sc.SendTakeResource("pgt 3 3");
		//Assert
		sc.playerManager.Received().SetPlayerTakeResource(3, 3);
	}

	[Test]
	public void SendDeathTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager (), GetMockDebugManager(), null);
		//Act
		sc.SendDeath("pdi 3");
		//Assert
		sc.playerManager.Received().SetPlayerDeath(3);
	}

	[Test]
	public void SendEndOfForkTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager (), GetMockDebugManager(), null);
		//Act
		sc.SendEndOfFork("enw 4 3 5 5");
		//Assert
		sc.playerManager.Received().SetEggCreation(4, 3, 5, 5);
	}

	[Test]
	public void SendHatchedEggTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager (), GetMockDebugManager(), null);
		//Act
		sc.SendHatchedEgg("eht 4");
		//Assert
		sc.playerManager.Received().SetEggHatch(4);
	}

	[Test]
	public void SendPlayerToEggConnection()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager (), GetMockDebugManager(), null);
		//Act
		sc.SendPlayerToEggConnection("ebo 4");
		//Assert
		sc.playerManager.Received().SetPlayerToEggConnection(4);
	}

	[Test]
	public void SendRottenEggTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager (), GetMockDebugManager(), null);
		//Act
		sc.SendRottenEgg("edi 4");
		//Assert
		sc.playerManager.Received().SetEggDie(4);
	}

	[Test]
	public void SendTimeUnitTest()
	{
		//Arrange
		ServerCommands sc = new ServerCommands (GetMockGridController (), GetMockTeamManager (), GetMockPlayerManager (), GetMockTimeManager (), GetMockDebugManager(), null);
		//Act
		sc.SendTimeUnit("sgt 150");
		//Assert
		sc.timeManager.Received().ChangeTimeSpeedServer(150.0f);
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

	public PlayerManager GetMockPlayerManager()
	{
		return (Substitute.For<PlayerManager> (GetMockGridController(), GetMockTeamManager(), GetMockPlayerInstantiation(), GetMockEggInstantiation()));
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

	public ISquare GetMockSquare()
	{
		return (Substitute.For<ISquare> ());
	}

	public PlayerController GetMockPlayerController(IAnimatorController animatorController)
	{
		var controller = Substitute.For<PlayerController>();
		controller.SetAnimatorController(animatorController);
		return controller;
	}

	public IAnimatorController GetMockAnimatorController()
	{
		return (Substitute.For<IAnimatorController>());
	}

	public ILevelLoader GetMockLevelLoader()
	{
		return (Substitute.For<ILevelLoader>());
	}

	public Team GetMockTeam ()
	{
		return (Substitute.For<Team> ("TestTeam", Color.red));
	}

	public GameManagerScript GetMockGameManager ()
	{
		return (Substitute.For<GameManagerScript>());
	}

	public DebugManager GetMockDebugManager()
	{
		return (Substitute.For<DebugManager>());
	}
}
