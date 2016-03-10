using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
public abstract class MonoBehaviourTester : MonoBehaviour {
	protected delegate void TestMethod();

	protected void Start()
	{
		InitTest();
	}

	protected abstract void InitTest ();

	protected IEnumerator WaitForTest(MonoBehaviourTest test, TestMethod method)
	{
		if (test.time > 0.0f)
			yield return new WaitForSeconds(test.time);
		method();
	}

	[System.Serializable]
	public class MonoBehaviourTest
	{
		public bool enabled = false;
		public float time = 0.0f;
	}
}
#endif  