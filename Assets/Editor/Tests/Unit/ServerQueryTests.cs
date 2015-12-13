using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using System.Collections;

[TestFixture]
public class ServerQueryTests{
	[Test]
	public void GetMapSizeString_Correct_String()
	{
		// Arrange
		string expected_result = "msz\n";
		ServerQuery sq = Substitute.For<ServerQuery> ();
		string result;
		// Act
		result = sq.GetMapSizeString ();
		// Assert
		Assert.AreEqual (expected_result, result);
	}

	[Test]
	public void GetSquareContentString_Null_Map_Size()
	{
		// Arrange
		string expected_result = "bct 0 0\n";
		ServerQuery sq = Substitute.For<ServerQuery> ();
		string result;
		// Act
		result = sq.GetSquareContentString(0, 0);
		// Assert
		Assert.AreEqual (expected_result, result);
	}

	[Test]
	public void GetSquareContentString_Positive_Map_Size()
	{
		// Arrange
		string expected_result = "bct 10 10\n";
		ServerQuery sq = Substitute.For<ServerQuery> ();
		string result;
		// Act
		result = sq.GetSquareContentString(10, 10);
		// Assert
		Assert.AreEqual (expected_result, result);
	}

	[Test]
	public void GetSquareContentString_Different_Map_Size()
	{
		// Arrange
		string expected_result = "bct 5 8\n";
		ServerQuery sq = Substitute.For<ServerQuery> ();
		string result;
		// Act
		result = sq.GetSquareContentString(5, 8);
		// Assert
		Assert.AreEqual (expected_result, result);
	}

	[Test]
	[ExpectedException(typeof(ServerQuery.NegativeSquareCoordException))]
	public void GetSquareContentString_Negative_X()
	{
		// Arrange
		ServerQuery sq = Substitute.For<ServerQuery> ();
		// Act
		sq.GetSquareContentString(-1, 0);
	}

	[Test]
	[ExpectedException(typeof(ServerQuery.NegativeSquareCoordException))]
	public void GetSquareContentString_Negative_Y()
	{
		// Arrange 
		ServerQuery sq = Substitute.For<ServerQuery> ();
		// Act
		sq.GetSquareContentString(0, -1);
	}

	[Test]
	public void GetAllSquareString_Correct_String()
	{
		// Arrange
		string expected_result = "mct\n";
		ServerQuery sq = Substitute.For<ServerQuery> ();
		string result;
		// Act
		result = sq.GetAllSquaresString();
		// Assert
		Assert.AreEqual (expected_result, result);
	}

	[Test]
	public void GetTeamNamesString_Correct_String()
	{
		// Arrange
		string expected_result = "tna\n";
		ServerQuery sq = Substitute.For<ServerQuery> ();
		string result;
		// Act
		result = sq.GetTeamNamesString();
		// Assert
		Assert.AreEqual (expected_result, result);
	}

	[Test]
	[ExpectedException(typeof(ServerQuery.NegativePlayerIndexException))]
	public void GetPlayerPositionString_Negative_Index()
	{
		// Arrange
		ServerQuery sq = Substitute.For<ServerQuery> ();
		// Act
		sq.GetPlayerPositionString(-1);
	}

	[Test]
	public void GetPlayerPositionString_Zero_Index()
	{
		// Arrange
		string expected_result = "ppo #0\n";
		ServerQuery sq = Substitute.For<ServerQuery> ();
		string result;
		// Act
		result = sq.GetPlayerPositionString(0);
		// Assert
		Assert.AreEqual (expected_result, result);
	}

	[Test]
	public void GetPlayerPositionString_MaxInt_Index()
	{
		// Arrange
		string expected_result = "ppo #" + int.MaxValue + "\n";
		ServerQuery sq = Substitute.For<ServerQuery> ();
		string result;
		// Act
		result = sq.GetPlayerPositionString(int.MaxValue);
		// Assert
		Assert.AreEqual (expected_result, result);
	}

	[Test]
	[ExpectedException(typeof(ServerQuery.NegativePlayerIndexException))]
	public void GetPlayerLevelString_Negative_Index()
	{
		// Arrange
		ServerQuery sq = Substitute.For<ServerQuery> ();
		// Act
		sq.GetPlayerLevelString(-1);
	}
	
	[Test]
	public void GetPlayerLevelString_Zero_Index()
	{
		// Arrange
		string expected_result = "plv #0\n";
		ServerQuery sq = Substitute.For<ServerQuery> ();
		string result;
		// Act
		result = sq.GetPlayerLevelString(0);
		// Assert
		Assert.AreEqual (expected_result, result);
	}
	
	[Test]
	public void GetPlayerLevelString_MaxInt_Index()
	{
		// Arrange
		string expected_result = "plv #" + int.MaxValue + "\n";
		ServerQuery sq = Substitute.For<ServerQuery> ();
		string result;
		// Act
		result = sq.GetPlayerLevelString(int.MaxValue);
		// Assert
		Assert.AreEqual (expected_result, result);
	}

	[Test]
	[ExpectedException(typeof(ServerQuery.NegativePlayerIndexException))]
	public void GetPlayerInventoryString_Negative_Index()
	{
		// Arrange
		ServerQuery sq = Substitute.For<ServerQuery> ();
		// Act
		sq.GetPlayerInventoryString(-1);
	}
	
	[Test]
	public void GetPlayerInventoryString_Zero_Index()
	{
		// Arrange
		string expected_result = "pin #0\n";
		ServerQuery sq = Substitute.For<ServerQuery> ();
		string result;
		// Act
		result = sq.GetPlayerInventoryString(0);
		// Assert
		Assert.AreEqual (expected_result, result);
	}
	
	[Test]
	public void GetPlayerInventoryString_MaxInt_Index()
	{
		// Arrange
		string expected_result = "pin #" + int.MaxValue + "\n";
		ServerQuery sq = Substitute.For<ServerQuery> ();
		string result;
		// Act
		result = sq.GetPlayerInventoryString(int.MaxValue);
		// Assert
		Assert.AreEqual (expected_result, result);
	}

	[Test]
	public void GetCurrentTimeUnitString_Correct_String()
	{
		// Arrange
		string expected_result = "sgt\n";
		ServerQuery sq = Substitute.For<ServerQuery> ();
		string result;
		// Act
		result = sq.GetCurrentTimeUnitString ();
		// Assert
		Assert.AreEqual (expected_result, result);
	}

	[Test]
	[ExpectedException(typeof(ServerQuery.NegativeTimeUnitException))]
	public void GetTimeUnitChangeString_Negative_Index()
	{
		// Arrange
		ServerQuery sq = Substitute.For<ServerQuery> ();
		// Act
		sq.GetTimeUnitChangeString(-1);
	}
	
	[Test]
	public void GetTimeUnitChangeString_Zero_Index()
	{
		// Arrange
		string expected_result = "sst 0\n";
		ServerQuery sq = Substitute.For<ServerQuery> ();
		string result;
		// Act
		result = sq.GetTimeUnitChangeString(0);
		// Assert
		Assert.AreEqual (expected_result, result);
	}
	
	[Test]
	public void GetTimeUnitChangeString_MaxInt_Index()
	{
		// Arrange
		string expected_result = "sst " + int.MaxValue + "\n";
		ServerQuery sq = Substitute.For<ServerQuery> ();
		string result;
		// Act
		result = sq.GetTimeUnitChangeString(int.MaxValue);
		// Assert
		Assert.AreEqual (expected_result, result);
	}

	[Test]
	public void GetWelcomeMessageString_Correct_String()
	{	
		// Arrange
		string expected_result = "GRAPHIC\n";
		ServerQuery sq = Substitute.For<ServerQuery> ();
		string result;
		// Act
		result = sq.GetWelcomeMessageString ();
		// Assert
		Assert.AreEqual (expected_result, result);
	}
}
