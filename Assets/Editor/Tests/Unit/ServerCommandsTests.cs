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
		return Substitute.For<ISquare> ();
	}
}
