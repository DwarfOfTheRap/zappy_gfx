using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerScript))]
public class PlayerTester : MonoBehaviourTester {
	public MonoBehaviourTest		deathTest;
	public MonoBehaviourTest		orientationTest;
	public MonoBehaviourTest		incantateTest;
	public MonoBehaviourTest		incantate_stopTest;
	public MonoBehaviourTest		setDestinationTest;
	public MonoBehaviourTest		setDestinationTestLegit;
	public MonoBehaviourTest		expulsedTest;
	public Orientation		orientation;
	public SquareScript		destinationSquare;
	public SquareScript		originSquare;
	public int[]			destinationVector;
	public int[]			initVector;

	protected override void InitTest()
	{
		if (orientationTest.enabled)
			StartCoroutine (WaitForTest(orientationTest, TestOrientation));
		if (deathTest.enabled)
			StartCoroutine (WaitForTest (deathTest, TestDie));
		if (incantateTest.enabled)
			StartCoroutine (WaitForTest (incantateTest, TestIncantate));
		if (incantate_stopTest.enabled)
			StartCoroutine (WaitForTest (incantate_stopTest, TestIncantateStop));
		if (setDestinationTest.enabled)
			StartCoroutine (WaitForTest (setDestinationTest, TestDestination));
		if (expulsedTest.enabled)
			StartCoroutine (WaitForTest (expulsedTest, TestExpulsed));
		if (setDestinationTestLegit.enabled)
			StartCoroutine (WaitForTest (setDestinationTestLegit, TestDestinationLegit));
	}

	void TestOrientation ()
	{
		GetComponent<PlayerScript>().controller.SetPlayerOrientation(orientation);
	}

	void TestDie()
	{
		GetComponent<PlayerScript>().controller.Die ();
	}

	void TestIncantate()
	{
		GetComponent<PlayerScript>().controller.Incantate ();
	}

	void TestIncantateStop()
	{
		GetComponent<PlayerScript>().controller.StopIncantating ();
	}

	void TestDestination()
	{
		GetComponent<PlayerScript>().controller.SetDestination (destinationSquare, null);
	}

	void TestDestinationLegit()
	{
		GetComponent<PlayerScript>().controller.Init (initVector[0], initVector[1], orientation, 1, 1, null, GameManagerScript.instance.grid.controller);
		GetComponent<PlayerScript>().controller.SetPosition (destinationVector[0], destinationVector[1], GameManagerScript.instance.grid.controller);
	}

	void TestExpulsed ()
	{
		GetComponent<PlayerScript>().controller.SetDestination (destinationSquare, null);
		GetComponent<PlayerScript>().controller.BeExpulsed (orientation);
	}
}
