using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using System.Collections;

[TestFixture]
public class ServerReaderTests {

	[Test]
	public void IsMapSizeString_Regex_Testing()
	{
		// Arrange
		bool test1_expected_result = true;
		bool test2_expected_result = true;
		bool test3_expected_result = false;
		bool test4_expected_result = false;
		bool test5_expected_result = false;
		bool test6_expected_result = false;
		bool test7_expected_result = false;
		bool test8_expected_result = false;
		ServerReader sr = Substitute.For<ServerReader>();
		
		// Act
		bool test1_result = sr.IsMapSizeString("msz 0 0\n");
		bool test2_result = sr.IsMapSizeString("msz 99999 99999\n");
		bool test3_result = sr.IsMapSizeString("msz -1 0\n");
		bool test4_result = sr.IsMapSizeString("msz 0 -1\n");
		bool test5_result = sr.IsMapSizeString("msz\n");
		bool test6_result = sr.IsMapSizeString("msz 0\n");
		bool test7_result = sr.IsMapSizeString("mgz 0 0\n");
		bool test8_result = sr.IsMapSizeString("");
		
		// Assert
		Assert.AreEqual (test1_expected_result, test1_result);
		Assert.AreEqual (test2_expected_result, test2_result);
		Assert.AreEqual (test3_expected_result, test3_result);
		Assert.AreEqual (test4_expected_result, test4_result);
		Assert.AreEqual (test5_expected_result, test5_result);
		Assert.AreEqual (test6_expected_result, test6_result);
		Assert.AreEqual (test7_expected_result, test7_result);
		Assert.AreEqual (test8_expected_result, test8_result);
	}

	[Test]
	public void IsSquareContentString_Regex_Testing()
	{
		// Arrange
		bool test1_expected_result = true;
		bool test2_expected_result = true;
		bool test3_expected_result = true;
		bool test4_expected_result = false;
		bool test5_expected_result = true;
		bool test6_expected_result = false;
		bool test7_expected_result = false;
		bool test8_expected_result = false;
		ServerReader sr = Substitute.For<ServerReader>();
		
		// Act
		bool test1_result = sr.IsSquareContentString("bct 0 0 0 0 0 0 0 0 0\n");
		bool test2_result = sr.IsSquareContentString("bct 9999 9999 6 6 6 6 6 6 6\n");
		bool test3_result = sr.IsSquareContentString("bct 9999 9999 9999 9999 9999 9999 9999 9999 9999\n");
		bool test4_result = sr.IsSquareContentString("bct 0 0 0 0 0 0 0 0\n");
		bool test5_result = sr.IsSquareContentString("bct 1 2 3 4 5 6 7 8 9\n");
		bool test6_result = sr.IsSquareContentString("bct -1 0 1 2 3 4 5 6 7 8\n");
		bool test7_result = sr.IsSquareContentString("bct\n");
		bool test8_result = sr.IsSquareContentString("");
		
		// Assert
		Assert.AreEqual (test1_expected_result, test1_result);
		Assert.AreEqual (test2_expected_result, test2_result);
		Assert.AreEqual (test3_expected_result, test3_result);
		Assert.AreEqual (test4_expected_result, test4_result);
		Assert.AreEqual (test5_expected_result, test5_result);
		Assert.AreEqual (test6_expected_result, test6_result);
		Assert.AreEqual (test7_expected_result, test7_result);
		Assert.AreEqual (test8_expected_result, test8_result);
	}

	[Test]
	public void IsTeamNamesString_Regex_Testing()
	{
		// Arrange
		bool test1_expected_result = true;
		bool test2_expected_result = true;
		bool test3_expected_result = false;
		bool test4_expected_result = true;
		bool test5_expected_result = false;
		bool test6_expected_result = false;
		bool test7_expected_result = false;
		bool test8_expected_result = false;
		ServerReader sr = Substitute.For<ServerReader>();
		
		// Act
		bool test1_result = sr.IsTeamNamesString("tna bidule\n");
		bool test2_result = sr.IsTeamNamesString("tna Thequickbrownfoxjumpsoverthelazydog\n");
		bool test3_result = sr.IsTeamNamesString("tna \n");
		bool test4_result = sr.IsTeamNamesString("tna 01234567890123456789\n");
		bool test5_result = sr.IsTeamNamesString("tna\n");
		bool test6_result = sr.IsTeamNamesString("");
		bool test7_result = sr.IsTeamNamesString("tna");
		bool test8_result = sr.IsTeamNamesString("tna ---\n");
		
		// Assert
		Assert.AreEqual (test1_expected_result, test1_result);
		Assert.AreEqual (test2_expected_result, test2_result);
		Assert.AreEqual (test3_expected_result, test3_result);
		Assert.AreEqual (test4_expected_result, test4_result);
		Assert.AreEqual (test5_expected_result, test5_result);
		Assert.AreEqual (test6_expected_result, test6_result);
		Assert.AreEqual (test7_expected_result, test7_result);
		Assert.AreEqual (test8_expected_result, test8_result);
	}

	[Test]
	public void IsPlayerConnectionString_Regex_Testing()
	{
		//Arrange
		bool test1_expected_result = true;
		bool test2_expected_result = true;
		bool test3_expected_result = true;
		bool test4_expected_result = true;
		bool test5_expected_result = false;
		bool test6_expected_result = false;
		bool test7_expected_result = false;
		bool test8_expected_result = false;
		bool test9_expected_result = false;
		bool test10_expected_result = false;
		bool test11_expected_result = false;
		bool test12_expected_result = false;

		string test1_string = "pnw #40 4 3 2 8 test\n";
		string test2_string = "pnw #40 4 3 2 8 testtesttesttesttesttesttesttesttesttesttest\n";
		string test3_string = "pnw #0 0 0 1 1 0\n";
		string test4_string = "pnw #99999 99999 99999 4 8 Thequickbrownfoxjumpsoverthelazydog\n";
		string test5_string = "bmw #99999 99999 99999 4 8 Thequickbrownfoxjumpsoverthelazydog\n";
		string test6_string = "";
		string test7_string = "string\n";
		string test8_string = "pnw #99999 99999 99999 4 8 Thequickbrownfoxjumpsoverthelazydog";
		string test9_string = "pnw #99999 99999 99999 4 8 Thequickbrownfoxjumpsoverthelazydog 8\n";
		string test10_string = "pnw #99999 99999 99999 4 8 Thequickbrownfoxjumpsoverthelazydog\ngwrgwgweg";
		string test11_string = "pnw #0 0 0 0 0 0\n";
		string test12_string = "pnw #0 0 0 5 9 0\n";

		ServerReader sr = Substitute.For<ServerReader>();

		// Act
		bool test1_result = sr.IsPlayerConnectionString(test1_string);
		bool test2_result = sr.IsPlayerConnectionString(test2_string);
		bool test3_result = sr.IsPlayerConnectionString(test3_string);
		bool test4_result = sr.IsPlayerConnectionString(test4_string);
		bool test5_result = sr.IsPlayerConnectionString(test5_string);
		bool test6_result = sr.IsPlayerConnectionString(test6_string);
		bool test7_result = sr.IsPlayerConnectionString(test7_string);
		bool test8_result = sr.IsPlayerConnectionString(test8_string);
		bool test9_result = sr.IsPlayerConnectionString(test9_string);
		bool test10_result = sr.IsPlayerConnectionString(test10_string);
		bool test11_result = sr.IsPlayerConnectionString(test11_string);
		bool test12_result = sr.IsPlayerConnectionString(test12_string);

		// Assert
		Assert.AreEqual (test1_expected_result, test1_result);
		Assert.AreEqual (test2_expected_result, test2_result);
		Assert.AreEqual (test3_expected_result, test3_result);
		Assert.AreEqual (test4_expected_result, test4_result);
		Assert.AreEqual (test5_expected_result, test5_result);
		Assert.AreEqual (test6_expected_result, test6_result);
		Assert.AreEqual (test7_expected_result, test7_result);
		Assert.AreEqual (test8_expected_result, test8_result);
		Assert.AreEqual (test9_expected_result, test9_result);
		Assert.AreEqual (test10_expected_result, test10_result);
		Assert.AreEqual (test11_expected_result, test11_result);
		Assert.AreEqual (test12_expected_result, test12_result);
	}

	[Test]
	public void IsPlayerPositionString_Regex_Testing()
	{
		//Arrange
		bool test1_expected_result = true;
		bool test2_expected_result = true;
		bool test3_expected_result = false;
		bool test4_expected_result = false;
		bool test5_expected_result = false;
		bool test6_expected_result = false;
		bool test7_expected_result = false;
		
		string test1_string = "ppo #0 0 0 1\n";
		string test2_string = "ppo #99999 99999 99999 4\n";
		string test3_string = "ppo #0 0 0 5";
		string test4_string = "ppo #0 0 0 0\n";
		string test5_string = "";
		string test6_string = "string\n";
		string test7_string = "ppo #1\n";
		
		ServerReader sr = Substitute.For<ServerReader>();
		
		// Act
		bool test1_result = sr.IsPlayerPositionString(test1_string);
		bool test2_result = sr.IsPlayerPositionString(test2_string);
		bool test3_result = sr.IsPlayerPositionString(test3_string);
		bool test4_result = sr.IsPlayerPositionString(test4_string);
		bool test5_result = sr.IsPlayerPositionString(test5_string);
		bool test6_result = sr.IsPlayerPositionString(test6_string);
		bool test7_result = sr.IsPlayerPositionString(test7_string);
		
		// Assert
		Assert.AreEqual (test1_expected_result, test1_result);
		Assert.AreEqual (test2_expected_result, test2_result);
		Assert.AreEqual (test3_expected_result, test3_result);
		Assert.AreEqual (test4_expected_result, test4_result);
		Assert.AreEqual (test5_expected_result, test5_result);
		Assert.AreEqual (test6_expected_result, test6_result);
		Assert.AreEqual (test7_expected_result, test7_result);
	}

	[Test]
	public void IsPlayerLevelString_Regex_Testing()
	{
		//Arrange
		bool test1_expected_result = true;
		bool test2_expected_result = true;
		bool test3_expected_result = false;
		bool test4_expected_result = false;
		bool test5_expected_result = false;
		bool test6_expected_result = false;
		bool test7_expected_result = false;
		
		string test1_string = "plv #0 1\n";
		string test2_string = "plv #99999 8\n";
		string test3_string = "plv #0 0";
		string test4_string = "plv #0 9\n";
		string test5_string = "plv #-1 1";
		string test6_string = "";
		string test7_string = "plv";
		
		ServerReader sr = Substitute.For<ServerReader>();
		
		// Act
		bool test1_result = sr.IsPlayerLevelString(test1_string);
		bool test2_result = sr.IsPlayerLevelString(test2_string);
		bool test3_result = sr.IsPlayerLevelString(test3_string);
		bool test4_result = sr.IsPlayerLevelString(test4_string);
		bool test5_result = sr.IsPlayerLevelString(test5_string);
		bool test6_result = sr.IsPlayerLevelString(test6_string);
		bool test7_result = sr.IsPlayerLevelString(test7_string);
		
		// Assert
		Assert.AreEqual (test1_expected_result, test1_result);
		Assert.AreEqual (test2_expected_result, test2_result);
		Assert.AreEqual (test3_expected_result, test3_result);
		Assert.AreEqual (test4_expected_result, test4_result);
		Assert.AreEqual (test5_expected_result, test5_result);
		Assert.AreEqual (test6_expected_result, test6_result);
		Assert.AreEqual (test7_expected_result, test7_result);
	}

	[Test]
	public void IsPlayerInventoryString_Regex_Testing()
	{
		//Arrange
		bool test1_expected_result = true;
		bool test2_expected_result = true;
		bool test3_expected_result = false;
		bool test4_expected_result = false;
		bool test5_expected_result = false;
		bool test6_expected_result = false;
		bool test7_expected_result = false;
		bool test8_expected_result = false;
		
		string test1_string = "pin #0 0 0 0 0 0 0 0 0 0\n";
		string test2_string = "pin #9999 9999 9999 9999 9999 9999 9999 9999 9999 9999\n";
		string test3_string = "pin #-1 0 0 0 0 0 0 0 0 0\n";
		string test4_string = "pin #0 -1 0 0 0 0 0 0 0 0\n";
		string test5_string = "pin #0 0 -1 0 0 0 0 0 0 0\n";
		string test6_string = "pin #0 0 0 0 0 0 0 0 0";
		string test7_string = "pin\n";
		string test8_string = "";
		
		ServerReader sr = Substitute.For<ServerReader>();
		
		// Act
		bool test1_result = sr.IsPlayerInventoryString(test1_string);
		bool test2_result = sr.IsPlayerInventoryString(test2_string);
		bool test3_result = sr.IsPlayerInventoryString(test3_string);
		bool test4_result = sr.IsPlayerInventoryString(test4_string);
		bool test5_result = sr.IsPlayerInventoryString(test5_string);
		bool test6_result = sr.IsPlayerInventoryString(test6_string);
		bool test7_result = sr.IsPlayerInventoryString(test7_string);
		bool test8_result = sr.IsPlayerInventoryString(test8_string);

		// Assert
		Assert.AreEqual (test1_expected_result, test1_result);
		Assert.AreEqual (test2_expected_result, test2_result);
		Assert.AreEqual (test3_expected_result, test3_result);
		Assert.AreEqual (test4_expected_result, test4_result);
		Assert.AreEqual (test5_expected_result, test5_result);
		Assert.AreEqual (test6_expected_result, test6_result);
		Assert.AreEqual (test7_expected_result, test7_result);
		Assert.AreEqual (test8_expected_result, test8_result);
	}

	[Test]
	public void IsPlayerExpulseString_Regex_Testing()
	{
		// Arrange
		bool test1_expected_result = true;
		bool test2_expected_result = true;
		bool test3_expected_result = false;
		bool test4_expected_result = false;
		bool test5_expected_result = false;
		bool test6_expected_result = false;
		bool test7_expected_result = false;
		bool test8_expected_result = false;
		ServerReader sr = Substitute.For<ServerReader>();
		
		// Act
		bool test1_result = sr.IsPlayerExpulseString("pex #0\n");
		bool test2_result = sr.IsPlayerExpulseString("pex #9999\n");
		bool test3_result = sr.IsPlayerExpulseString("pex #-1\n");
		bool test4_result = sr.IsPlayerExpulseString("pex #\n");
		bool test5_result = sr.IsPlayerExpulseString("pex\n");
		bool test6_result = sr.IsPlayerExpulseString("");
		bool test7_result = sr.IsPlayerExpulseString("pox #0");
		bool test8_result = sr.IsPlayerExpulseString("tna ---\n");
		
		// Assert
		Assert.AreEqual (test1_expected_result, test1_result);
		Assert.AreEqual (test2_expected_result, test2_result);
		Assert.AreEqual (test3_expected_result, test3_result);
		Assert.AreEqual (test4_expected_result, test4_result);
		Assert.AreEqual (test5_expected_result, test5_result);
		Assert.AreEqual (test6_expected_result, test6_result);
		Assert.AreEqual (test7_expected_result, test7_result);
		Assert.AreEqual (test8_expected_result, test8_result);
	}

	[Test]
	public void IsPlayerBroadcastString_Regex_Testing()
	{
		//Arrange
		bool test1_expected_result = true;
		bool test2_expected_result = true;
		bool test3_expected_result = true;
		bool test4_expected_result = false;
		bool test5_expected_result = false;
		bool test6_expected_result = false;
		bool test7_expected_result = false;
		
		string test1_string = "pbc #0 \"\"\n";
		string test2_string = "pbc #99999 \"The Quick Brown Fox Jumps Over The Lazy Dog\nAnd that's pretty much all\"\n";
		string test3_string = "pbc #5 \"Test\"\n";
		string test4_string = "pbc #-1 \"Test\"\n";
		string test5_string = "";
		string test6_string = "string\n";
		string test7_string = "pbc #1\n";
		
		ServerReader sr = Substitute.For<ServerReader>();
		
		// Act
		bool test1_result = sr.IsPlayerBroadcastString(test1_string);
		bool test2_result = sr.IsPlayerBroadcastString(test2_string);
		bool test3_result = sr.IsPlayerBroadcastString(test3_string);
		bool test4_result = sr.IsPlayerBroadcastString(test4_string);
		bool test5_result = sr.IsPlayerBroadcastString(test5_string);
		bool test6_result = sr.IsPlayerBroadcastString(test6_string);
		bool test7_result = sr.IsPlayerBroadcastString(test7_string);
		
		// Assert
		Assert.AreEqual (test1_expected_result, test1_result);
		Assert.AreEqual (test2_expected_result, test2_result);
		Assert.AreEqual (test3_expected_result, test3_result);
		Assert.AreEqual (test4_expected_result, test4_result);
		Assert.AreEqual (test5_expected_result, test5_result);
		Assert.AreEqual (test6_expected_result, test6_result);
		Assert.AreEqual (test7_expected_result, test7_result);
	}

	[Test]
	public void IsPlayerIncantationString_Regex_Testing()
	{
		//Arrange
		bool test1_expected_result = true;
		bool test2_expected_result = true;
		bool test3_expected_result = false;
		bool test4_expected_result = true;
		bool test5_expected_result = false;
		bool test6_expected_result = false;
		bool test7_expected_result = false;
		
		string test1_string = "pic 0 0 1 #0\n";
		string test2_string = "pic 99999 99999 8 #0 #1 #2 #3 #4 #5 #6 #7 #8 #9 #10\n";
		string test3_string = "pic 99999 99999 8";
		string test4_string = "pic 5 5 5 #5 #5 #5 #5 #5\n";
		string test5_string = "";
		string test6_string = "string\n";
		string test7_string = "pic #1\n";
		
		ServerReader sr = Substitute.For<ServerReader>();
		
		// Act
		bool test1_result = sr.IsPlayerIncantationString(test1_string);
		bool test2_result = sr.IsPlayerIncantationString(test2_string);
		bool test3_result = sr.IsPlayerIncantationString(test3_string);
		bool test4_result = sr.IsPlayerIncantationString(test4_string);
		bool test5_result = sr.IsPlayerIncantationString(test5_string);
		bool test6_result = sr.IsPlayerIncantationString(test6_string);
		bool test7_result = sr.IsPlayerIncantationString(test7_string);
		
		// Assert
		Assert.AreEqual (test1_expected_result, test1_result);
		Assert.AreEqual (test2_expected_result, test2_result);
		Assert.AreEqual (test3_expected_result, test3_result);
		Assert.AreEqual (test4_expected_result, test4_result);
		Assert.AreEqual (test5_expected_result, test5_result);
		Assert.AreEqual (test6_expected_result, test6_result);
		Assert.AreEqual (test7_expected_result, test7_result);
	}


	/*
	[Test]
	public void IsGameOverString_Regex_Testing()
	{
		//Arrange
		bool test1_expected_result = true;
		bool test2_expected_result = true;
		bool test3_expected_result = true;
		bool test4_expected_result = false;
		bool test5_expected_result = false;
		bool test6_expected_result = false;
		bool test7_expected_result = false;

		string test1_string = "seg 0\n";
		string test2_string = "seg 99999\n";
		string test3_string = "seg Thequickbrownfoxjumpsoverthelazydog\n";
		string test4_string = "seg Thequickbrownfoxjumpsoverthelazydog\nwrtwrtw";
		string test5_string = "";
		string test6_string = "string\n";
		string test7_string = "seg \n";

		ServerReader sr = Substitute.For<ServerReader>();

		// Act
		bool test1_result = sr.IsGameOverString(test1_string);
		bool test2_result = sr.IsGameOverString(test2_string);
		bool test3_result = sr.IsGameOverString(test3_string);
		bool test4_result = sr.IsGameOverString(test4_string);
		bool test5_result = sr.IsGameOverString(test5_string);
		bool test6_result = sr.IsGameOverString(test6_string);
		bool test7_result = sr.IsGameOverString(test7_string);

		// Assert
		Assert.AreEqual (test1_expected_result, test1_result);
		Assert.AreEqual (test2_expected_result, test2_result);
		Assert.AreEqual (test3_expected_result, test3_result);
		Assert.AreEqual (test4_expected_result, test4_result);
		Assert.AreEqual (test5_expected_result, test5_result);
		Assert.AreEqual (test6_expected_result, test6_result);
		Assert.AreEqual (test7_expected_result, test7_result);
	}

	[Test]
	public void IsPlayerDeathString_Regex_Testing()
	{
		//Arrange
		bool test1_expected_result = true;
		bool test2_expected_result = true;
		bool test3_expected_result = true;
		bool test4_expected_result = false;
		bool test5_expected_result = false;
		bool test6_expected_result = false;
		bool test7_expected_result = false;
		
		string test1_string = "pdi #0\n";
		string test2_string = "pdi #99999\n";
		string test3_string = "pdi #5\n";
		string test4_string = "pdi #-1\n";
		string test5_string = "";
		string test6_string = "string\n";
		string test7_string = "pdi #\n";
		
		ServerReader sr = Substitute.For<ServerReader>();
		
		// Act
		bool test1_result = sr.IsPlayerDeathString(test1_string);
		bool test2_result = sr.IsPlayerDeathString(test2_string);
		bool test3_result = sr.IsPlayerDeathString(test3_string);
		bool test4_result = sr.IsPlayerDeathString(test4_string);
		bool test5_result = sr.IsPlayerDeathString(test5_string);
		bool test6_result = sr.IsPlayerDeathString(test6_string);
		bool test7_result = sr.IsPlayerDeathString(test7_string);
		
		// Assert
		Assert.AreEqual (test1_expected_result, test1_result);
		Assert.AreEqual (test2_expected_result, test2_result);
		Assert.AreEqual (test3_expected_result, test3_result);
		Assert.AreEqual (test4_expected_result, test4_result);
		Assert.AreEqual (test5_expected_result, test5_result);
		Assert.AreEqual (test6_expected_result, test6_result);
		Assert.AreEqual (test7_expected_result, test7_result);
	}

	[Test]
	public void IsRottenEggString_Regex_Testing()
	{
		//Arrange
		bool test1_expected_result = true;
		bool test2_expected_result = true;
		bool test3_expected_result = true;
		bool test4_expected_result = false;
		bool test5_expected_result = false;
		bool test6_expected_result = false;
		bool test7_expected_result = false;
		
		string test1_string = "edi #0\n";
		string test2_string = "edi #99999\n";
		string test3_string = "edi #5\n";
		string test4_string = "edi #-1\n";
		string test5_string = "";
		string test6_string = "string\n";
		string test7_string = "edi #\n";
		
		ServerReader sr = Substitute.For<ServerReader>();
		
		// Act
		bool test1_result = sr.IsRottenEggString(test1_string);
		bool test2_result = sr.IsRottenEggString(test2_string);
		bool test3_result = sr.IsRottenEggString(test3_string);
		bool test4_result = sr.IsRottenEggString(test4_string);
		bool test5_result = sr.IsRottenEggString(test5_string);
		bool test6_result = sr.IsRottenEggString(test6_string);
		bool test7_result = sr.IsRottenEggString(test7_string);
		
		// Assert
		Assert.AreEqual (test1_expected_result, test1_result);
		Assert.AreEqual (test2_expected_result, test2_result);
		Assert.AreEqual (test3_expected_result, test3_result);
		Assert.AreEqual (test4_expected_result, test4_result);
		Assert.AreEqual (test5_expected_result, test5_result);
		Assert.AreEqual (test6_expected_result, test6_result);
		Assert.AreEqual (test7_expected_result, test7_result);
	}

	[Test]
	public void IsHatchedEggString_Regex_Testing()
	{
		//Arrange
		bool test1_expected_result = true;
		bool test2_expected_result = true;
		bool test3_expected_result = true;
		bool test4_expected_result = false;
		bool test5_expected_result = false;
		bool test6_expected_result = false;
		bool test7_expected_result = false;

		string test1_string = "eht #0\n";
		string test2_string = "eht #99999\n";
		string test3_string = "eht #5\n";
		string test4_string = "eht #-1\n";
		string test5_string = "";
		string test6_string = "string\n";
		string test7_string = "eht #\n";
		
		ServerReader sr = Substitute.For<ServerReader>();
		
		// Act
		bool test1_result = sr.IsHatchedEggString(test1_string);
		bool test2_result = sr.IsHatchedEggString(test2_string);
		bool test3_result = sr.IsHatchedEggString(test3_string);
		bool test4_result = sr.IsHatchedEggString(test4_string);
		bool test5_result = sr.IsHatchedEggString(test5_string);
		bool test6_result = sr.IsHatchedEggString(test6_string);
		bool test7_result = sr.IsHatchedEggString(test7_string);
		
		// Assert
		Assert.AreEqual (test1_expected_result, test1_result);
		Assert.AreEqual (test2_expected_result, test2_result);
		Assert.AreEqual (test3_expected_result, test3_result);
		Assert.AreEqual (test4_expected_result, test4_result);
		Assert.AreEqual (test5_expected_result, test5_result);
		Assert.AreEqual (test6_expected_result, test6_result);
		Assert.AreEqual (test7_expected_result, test7_result);
	}

	[Test]
	public void IsEndOfForkString_Regex_Testing()
	{
		//Arrange
		bool test1_expected_result = true;
		bool test2_expected_result = true;
		bool test3_expected_result = true;
		bool test4_expected_result = false;
		bool test5_expected_result = false;
		bool test6_expected_result = false;
		bool test7_expected_result = false;
		
		string test1_string = "enw #0 #0 0 0\n";
		string test2_string = "enw #99999 #99999 99999 99999\n";
		string test3_string = "enw #1 #1 1 1\n";
		string test4_string = "enw # #   \n";
		string test5_string = "";
		string test6_string = "string\n";
		string test7_string = "enw #\n";
		
		ServerReader sr = Substitute.For<ServerReader>();
		
		// Act
		bool test1_result = sr.IsEndOfForkString(test1_string);
		bool test2_result = sr.IsEndOfForkString(test2_string);
		bool test3_result = sr.IsEndOfForkString(test3_string);
		bool test4_result = sr.IsEndOfForkString(test4_string);
		bool test5_result = sr.IsEndOfForkString(test5_string);
		bool test6_result = sr.IsEndOfForkString(test6_string);
		bool test7_result = sr.IsEndOfForkString(test7_string);
		
		// Assert
		Assert.AreEqual (test1_expected_result, test1_result);
		Assert.AreEqual (test2_expected_result, test2_result);
		Assert.AreEqual (test3_expected_result, test3_result);
		Assert.AreEqual (test4_expected_result, test4_result);
		Assert.AreEqual (test5_expected_result, test5_result);
		Assert.AreEqual (test6_expected_result, test6_result);
		Assert.AreEqual (test7_expected_result, test7_result);
	}

	[Test]
	public void IsForkString_Regex_Testing()
	{
		//Arrange
		bool test1_expected_result = true;
		bool test2_expected_result = true;
		bool test3_expected_result = true;
		bool test4_expected_result = false;
		bool test5_expected_result = false;
		bool test6_expected_result = false;
		bool test7_expected_result = false;
		
		string test1_string = "pfk #0\n";
		string test2_string = "pfk #99999\n";
		string test3_string = "pfk #5\n";
		string test4_string = "pfk #-1\n";
		string test5_string = "";
		string test6_string = "string\n";
		string test7_string = "pfk #\n";
		
		ServerReader sr = Substitute.For<ServerReader>();
		
		// Act
		bool test1_result = sr.IsForkString(test1_string);
		bool test2_result = sr.IsForkString(test2_string);
		bool test3_result = sr.IsForkString(test3_string);
		bool test4_result = sr.IsForkString(test4_string);
		bool test5_result = sr.IsForkString(test5_string);
		bool test6_result = sr.IsForkString(test6_string);
		bool test7_result = sr.IsForkString(test7_string);
		
		// Assert
		Assert.AreEqual (test1_expected_result, test1_result);
		Assert.AreEqual (test2_expected_result, test2_result);
		Assert.AreEqual (test3_expected_result, test3_result);
		Assert.AreEqual (test4_expected_result, test4_result);
		Assert.AreEqual (test5_expected_result, test5_result);
		Assert.AreEqual (test6_expected_result, test6_result);
		Assert.AreEqual (test7_expected_result, test7_result);
	}*/
}
