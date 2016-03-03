using UnityEngine;
using System.Collections;

public class GameManagerTester : MonoBehaviourTester {
	public MonoBehaviourTest eggTest;
	public int[]			initVector;
	
	protected override void InitTest ()
	{
		if (eggTest.enabled)
			StartCoroutine (WaitForTest(eggTest, TestEgg));
	}

	void TestEgg ()
	{
		StartCoroutine(EggCoroutine());
	}

	IEnumerator EggCoroutine()
	{
		GetComponent<GameManagerScript>().playerManager.SetEggCreation (3, GetComponent<GameManagerScript>().playerManager.players[0].index, initVector[0], initVector[1]);
		GetComponent<GameManagerScript>().playerManager.SetEggCreation (2, GetComponent<GameManagerScript>().playerManager.players[1].index, 2, 8);
		GetComponent<GameManagerScript>().playerManager.SetEggCreation (1, GetComponent<GameManagerScript>().playerManager.players[2].index, 0, 0);
		yield return new WaitForSeconds(5);
		GetComponent<GameManagerScript>().playerManager.SetEggHatch (3);
		GetComponent<GameManagerScript>().playerManager.SetEggHatch (2);
		GetComponent<GameManagerScript>().playerManager.SetEggHatch (1);
		yield return new WaitForSeconds(10);
		GetComponent<GameManagerScript>().playerManager.SetPlayerToEggConnection (3);
		GetComponent<GameManagerScript>().playerManager.SetPlayerConnection (Random.Range (0, 10000), initVector[0], initVector[1], Orientation.SOUTH, Random.Range (0, 7), GetComponent<GameManagerScript>().teamManager.teams[0].name);
		GetComponent<GameManagerScript>().playerManager.SetPlayerToEggConnection (2);
		GetComponent<GameManagerScript>().playerManager.SetPlayerConnection (Random.Range (0, 10000), 2, 8, Orientation.SOUTH, Random.Range (0, 7), GetComponent<GameManagerScript>().teamManager.teams[1].name);
		GetComponent<GameManagerScript>().playerManager.SetPlayerToEggConnection (1);
		GetComponent<GameManagerScript>().playerManager.SetPlayerConnection (Random.Range (0, 10000), 0, 0, Orientation.SOUTH, Random.Range (0, 7), GetComponent<GameManagerScript>().teamManager.teams[2].name);
	}
}
