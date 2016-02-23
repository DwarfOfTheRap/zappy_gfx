﻿using UnityEngine;
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
	public MonoBehaviourTest		constantWalkingTest;
	public MonoBehaviourTest		doingEightsTest;
	public MonoBehaviourTest		hopASquareTest;
	public MonoBehaviourTest		rotateOnceTest;
	public Orientation				orientation;
	public SquareScript				destinationSquare;
	public SquareScript				originSquare;
	public int[]					destinationVector;
	public Team						team;
	public int[]					initVector;

	[HideInInspector]
	public Color	color;

	public class Tuple<T1, T2, T3>
	{
		public T1 First { get; private set; }
		public T2 Second { get; private set; }
		public T3 Third { get; private set; }
		internal Tuple(T1 first, T2 second, T3 third)
		{
			First = first;
			Second = second;
			Third = third;
		}
	}

	public static class Tuple
	{
		public static Tuple<T1, T2, T3> New<T1, T2, T3>(T1 first, T2 second, T3 third)
		{
			var tuple = new Tuple<T1, T2, T3>(first, second, third);
			return tuple;
		}
	}

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
		if (constantWalkingTest.enabled)
			StartCoroutine (WaitForTest (constantWalkingTest, TestWalk));
		if (doingEightsTest.enabled)
			StartCoroutine (WaitForTest (doingEightsTest, TestDoEights));
		if (hopASquareTest.enabled)
			StartCoroutine (WaitForTest (hopASquareTest, TestOneSquare));
		if (rotateOnceTest.enabled)
			StartCoroutine (WaitForTest (rotateOnceTest, TestRotateOnce));
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
		//GetComponent<PlayerScript>().controller.EnableHighlight ();
		GameManagerScript.instance.playerManager.players.Add (GetComponent<PlayerScript>().controller);
	}

	void TestWalk()
	{
		GetComponent<PlayerScript>().controller.Init (initVector[0], initVector[1], orientation, 1, 1, GameManagerScript.instance.teamManager.createTeam ("test" + Random.Range (0, 2048).ToString ("0000")), GameManagerScript.instance.grid.controller);
		StartCoroutine (WalkMore(initVector[0], initVector[1]));
		GameManagerScript.instance.playerManager.players.Add (GetComponent<PlayerScript>().controller);
	}

	void TestDoEights()
	{
		GetComponent<PlayerScript>().controller.Init (initVector[0], initVector[1], orientation, 1, 1, GameManagerScript.instance.teamManager.createTeam ("test" + Random.Range (0, 2048).ToString ("0000")), GameManagerScript.instance.grid.controller);
		StartCoroutine (DoEight());
		GameManagerScript.instance.playerManager.players.Add (GetComponent<PlayerScript>().controller);
	}

	void TestOneSquare()
	{
		GetComponent<PlayerScript>().controller.Init (initVector[0], initVector[1], orientation, 1, 1, GameManagerScript.instance.teamManager.createTeam ("test" + Random.Range (0, 2048).ToString ("0000")), GameManagerScript.instance.grid.controller);
		StartCoroutine (HopASquare());
		GameManagerScript.instance.playerManager.players.Add (GetComponent<PlayerScript>().controller);
	}

	void TestRotateOnce()
	{
		GetComponent<PlayerScript>().controller.Init (initVector[0], initVector[1], orientation, 1, 1, GameManagerScript.instance.teamManager.createTeam ("test" + Random.Range (0, 2048).ToString ("0000")), GameManagerScript.instance.grid.controller);
		StartCoroutine (RotateOnce(Orientation.EAST));
		GameManagerScript.instance.playerManager.players.Add (GetComponent<PlayerScript>().controller);
	}

	IEnumerator WalkMore(int x, int y)
	{
		while (true) {
			yield return new WaitForEndOfFrame();
			if (GetComponent<PlayerScript>().controller.destination == GetComponent<PlayerScript>().transform.position)
			{
				y = (GameManagerScript.instance.grid.controller.height + (y - 1)) % GameManagerScript.instance.grid.controller.height;
				GetComponent<PlayerScript> ().controller.SetDestination (GameManagerScript.instance.grid.controller.GetSquare (x, y), GameManagerScript.instance.grid.controller);
			}
		}
	}

	IEnumerator DoEight()
	{
		Tuple<int, int, Orientation>[] tuples = {
			(new Tuple<int, int, Orientation> (4, 4, Orientation.SOUTH)),
			(new Tuple<int, int, Orientation> (4, 2, Orientation.SOUTH)),
			(new Tuple<int, int, Orientation> (2, 2, Orientation.WEST)),
			(new Tuple<int, int, Orientation> (2, 4, Orientation.NORTH)),
			(new Tuple<int, int, Orientation> (4, 4, Orientation.EAST)),
			(new Tuple<int, int, Orientation> (6, 4, Orientation.EAST)),
			(new Tuple<int, int, Orientation> (6, 6, Orientation.NORTH)),
			(new Tuple<int, int, Orientation> (4, 6, Orientation.WEST))
		};

		int i = 0;

		while (true) {
			yield return new WaitForEndOfFrame();
			if (GetComponent<PlayerScript>().controller.destination == GetComponent<PlayerScript>().transform.position)
			{

				GetComponent<PlayerScript> ().controller.SetDestination (GameManagerScript.instance.grid.controller.GetSquare (tuples[i].First, tuples[i].Second), GameManagerScript.instance.grid.controller);
				GetComponent<PlayerScript> ().controller.SetPlayerOrientation (tuples[i].Third);
				i++;
				i %= 8;
			}
		}
	}


	IEnumerator HopASquare ()
	{
		float startTime = Time.realtimeSinceStartup;

		GetComponent<PlayerScript> ().controller.SetDestination(GameManagerScript.instance.grid.controller.GetSquare (3, 2), GameManagerScript.instance.grid.controller);
		while (GetComponent<PlayerScript>().controller.destination != GetComponent<PlayerScript>().transform.position) {
			yield return new WaitForEndOfFrame();
		}
		Debug.Log ("HopASquare time " + (Time.realtimeSinceStartup - startTime).ToString ());
	}

	IEnumerator RotateOnce (Orientation orientation)
	{
		float startTime = Time.realtimeSinceStartup;

		GetComponent<PlayerScript> ().controller.SetPlayerOrientation (orientation);
		while (OrientationManager.GetRotation(orientation) != GetComponent<PlayerScript>().transform.rotation) {
			yield return new WaitForEndOfFrame();
		}
		Debug.Log ("RotateOnce time " + (Time.realtimeSinceStartup - startTime).ToString());
	}

	void Update()
	{
		color = GetComponentInChildren<Renderer>().materials[1].color;
	}
}
#endif
