using UnityEngine;
using System.Collections;
using System;

#if UNITY_EDITOR
[RequireComponent(typeof(SquareScript))]
public class SquareTester : MonoBehaviourTester {
	public MonoBehaviourTest resourcesTest;
	public int	numberOfEnabledResources;
	protected override void InitTest()
	{
		if (resourcesTest.enabled)
			StartCoroutine (WaitForTest (resourcesTest, TestResources));
	}

	void TestResources()
	{
		GetComponent<SquareScript>().SetResources (1, 0, 1, 0, 1, 0, 1);
	}

	void Update()
	{
		numberOfEnabledResources = Array.FindAll(GetComponentsInChildren<Renderer>(), x => (x.enabled && x != GetComponent<Renderer>())).Length;
	}
}
#endif