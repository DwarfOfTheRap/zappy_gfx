using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
[RequireComponent(typeof(GameManagerScript))]
public class GameOverTester : MonoBehaviourTester {
	public MonoBehaviourTest		gameOverTest;

	protected override void InitTest()
	{
		if (gameOverTest.enabled)
			StartCoroutine (WaitForTest (gameOverTest, TestGameOver));
	}
	
	void TestGameOver ()
	{
		GameManagerScript.instance.GameOver (GameManagerScript.instance.teamManager.CreateTeam ("Test Team"));
	}
}
#endif
