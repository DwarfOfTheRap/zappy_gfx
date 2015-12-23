using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerScript))]
public class PlayerTester : MonoBehaviour {
	public PlayerTest		deathTest;
	public PlayerTest		orientationTest;
	public PlayerTest		incantateTest;
	public PlayerTest		incantate_stopTest;
	public PlayerTest		setDestinationTest;
	public delegate void	TestMethod();
	public Orientation		orientation;
	public SquareScript		destinationSquare;

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
		GetComponent<PlayerScript>().controller.SetDestination (destinationSquare);
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
