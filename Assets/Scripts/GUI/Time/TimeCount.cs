using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeCount : MonoBehaviour {
	private int				_totalGameSeconds = 0;

	IEnumerator TimeUpdate ()
	{
		while (true) {
			if (GameManagerScript.instance.timeManager.timeSpeed > 0) {
				yield return new WaitForSeconds (1.0f / GameManagerScript.instance.timeManager.timeSpeed);
				if (GameManagerScript.instance.timeManager.timeSpeed > 0)
					_totalGameSeconds += 1;
			}
			else
				yield return new WaitForEndOfFrame();
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
		this.gameObject.GetComponent<Text> ().text = _totalGameSeconds.ToString ();
	}
}
