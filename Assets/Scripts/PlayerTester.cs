using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerScript))]
public class PlayerTester : MonoBehaviour {
	public PlayerTest		deathTest;
	public PlayerTest		orientationTest;
	public PlayerTest		incantateTest;
	public PlayerTest		incantate_stopTest;
	public PlayerTest		setDestinationTest;
	public PlayerTest		setDestinationTestLegit;
	public PlayerTest		expulsedTest;
	public delegate void	TestMethod();
	public Orientation		orientation;
	public SquareScript		destinationSquare;
	public int[]			destinationVector;
	public int[]			initVector;

	void Start()
	{
		TestInit ();
	}

	void TestInit()
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
		GetComponent<PlayerScript>().controller.Init (initVector[0], initVector[1], GameManagerScript.instance.grid.controller);
		GetComponent<PlayerScript>().controller.SetPosition (destinationVector[0], destinationVector[1], GameManagerScript.instance.grid.controller);
	}

	void TestExpulsed ()
	{
		GetComponent<PlayerScript>().controller.SetDestination (destinationSquare, null);
		GetComponent<PlayerScript>().controller.BeExpulsed (orientation);
	}

	IEnumerator WaitForTest(PlayerTest test, TestMethod method)
	{
		if (test.time > 0.0f)
			yield return new WaitForSeconds(test.time);
		method();
	}

	[System.Serializable]
	public class PlayerTest
	{
		public bool enabled = false;
		public float time = 0.0f;
	}
}
