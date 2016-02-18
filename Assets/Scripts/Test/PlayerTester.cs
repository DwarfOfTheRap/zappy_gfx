using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
[RequireComponent(typeof(PlayerScript))]
public class PlayerTester : MonoBehaviourTester {
	public MonoBehaviourTest		deathTest;
	public MonoBehaviourTest		orientationTest;
	public MonoBehaviourTest		incantateTest;
	public MonoBehaviourTest		incantate_stopTest;
	public MonoBehaviourTest		setDestinationTest;
	public MonoBehaviourTest		setDestinationTestLegit;
	public MonoBehaviourTest		expulsedTest;
	public MonoBehaviourTest		teamTest;
	public Orientation				orientation;
	public SquareScript				destinationSquare;
	public SquareScript				originSquare;
	public int[]					destinationVector;
	public Team						team;
	public int[]					initVector;

	[HideInInspector]
	public Color	color;

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
		if (teamTest.enabled)
			StartCoroutine (WaitForTest (teamTest, TestTeam));
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
		GetComponent<PlayerScript>().controller.Init (initVector[0], initVector[1], orientation, 1, 1, GameManagerScript.instance.teamManager.createTeam ("test" + Random.Range (0, 2048).ToString ("0000")), GameManagerScript.instance.grid.controller);
		GetComponent<PlayerScript>().controller.SetPosition (destinationVector[0], destinationVector[1], GameManagerScript.instance.grid.controller);
	}

	void TestExpulsed ()
	{
		GetComponent<PlayerScript>().controller.SetDestination (destinationSquare, null);
		GetComponent<PlayerScript>().controller.BeExpulsed (orientation);
	}

	void TestTeam()
	{
		GetComponent<PlayerScript>().controller.Init (initVector[0], initVector[1], orientation, 1, 1, GameManagerScript.instance.teamManager.createTeam ("test" + Random.Range (0, 2048).ToString ("0000")), GameManagerScript.instance.grid.controller);
		GetComponent<PlayerScript>().controller.EnableHighlight ();
		GameManagerScript.instance.playerManager.players.Add (GetComponent<PlayerScript>().controller);
	}

	void Update()
	{
		color = GetComponentInChildren<Renderer>().materials[1].color;
	}
}
#endif
