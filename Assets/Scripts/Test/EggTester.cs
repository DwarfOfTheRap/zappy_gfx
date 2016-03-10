using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
[RequireComponent(typeof(EggScript))]
public class EggTester : MonoBehaviourTester {
	public MonoBehaviourTest eggInitTest;
	public int[]		initVector;

	protected override void InitTest ()
	{
		if (eggInitTest.enabled)
			StartCoroutine (WaitForTest(eggInitTest, TestEggInit));
	}
	
	void TestEggInit ()
	{
		GetComponent<EggScript>().controller.Init (initVector[0], initVector[1], 1, GameManagerScript.instance.playerManager.players[0], GameManagerScript.instance.gridController);
	}
}
#endif
