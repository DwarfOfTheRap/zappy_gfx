using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using System.Collections;
using System.Collections.Generic;

[TestFixture]
public class TeamManagerTests {
	[Test]
	[ExpectedException(typeof(TeamManager.TeamNotFoundException))]
	public void FindTeam_TeamNotFoundException_Test()
	{
		//Arrange
		TeamManager tm = new TeamManager();
		tm.createTeam ("Test");

		//Act
		tm.findTeam ("Test2");
	}

	[Test]
	[ExpectedException(typeof(TeamManager.NoTeamException))]
	public void FindTeam_NoTeamException_Test()
	{
		//Arrange
		TeamManager tm = new TeamManager();
		
		//Act
		tm.findTeam ("Test2");
	}

	[Test]
	public void FindTeam_TeamFound_Test()
	{
		//Arrange
		TeamManager tm = new TeamManager();
		Team team1 = tm.createTeam ("Test");
		
		//Act
		Team team2 = tm.findTeam ("Test");

		//Assert
		Assert.AreSame (team1, team2);
	}

	[Test]
	public void CreateTeam_DifferentColors_Test()
	{
		//Arrange
		TeamManager tm = new TeamManager();

		//Act
		Team team1 = tm.createTeam ("Test");
		Team team2 = tm.createTeam ("Test2");
		Team team3 = tm.createTeam ("Test3");

		//Assert
		Assert.AreNotEqual (team1.color, team2.color);
		Assert.AreNotEqual (team2.color, team3.color);
		Assert.AreNotEqual (team3.color, team1.color);
	}
}
