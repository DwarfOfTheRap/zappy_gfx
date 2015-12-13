using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using System.Collections;

[TestFixture]
public class ServerReaderTests {
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
		string test7_string = "sog ew\n";

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
}
