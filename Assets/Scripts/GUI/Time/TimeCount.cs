using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeCount : MonoBehaviour {
	private float			_totalGameSeconds = 0;

	IEnumerator TimeUpdate ()
	{
		var time = Time.realtimeSinceStartup;
		while (true) {
			var tmp = Time.realtimeSinceStartup - time;
			if (tmp >= (1.0f / GameManagerScript.instance.timeManager.timeSpeed))
			{
				_totalGameSeconds += tmp / (1.0f / GameManagerScript.instance.timeManager.timeSpeed);
				time = Time.realtimeSinceStartup;
			}
			yield return null;
		}
	}

	void StopTimeCount (GameOverEventArgs ev)
	{
		StopAllCoroutines ();
	}

	void Start () {
		GameManagerScript.instance.OnGameOver += StopTimeCount;
		this.gameObject.GetComponent<Text> ().text = "0";
		StartCoroutine (TimeUpdate());
	}
	
	void Update () {
		this.gameObject.GetComponent<Text> ().text = ((int)_totalGameSeconds).ToString ();
	}
}
