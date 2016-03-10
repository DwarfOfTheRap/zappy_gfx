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
		this.gameObject.GetComponent<Text> ().text = "00:00";
		StartCoroutine (TimeUpdate());
	}
	
	void Update () {
		int seconds;
		int minutes;
		int hours;

		seconds = _totalGameSeconds % 60;
		minutes = _totalGameSeconds / 60;
		minutes %= 60;
		hours = _totalGameSeconds / 3600;
	
		this.gameObject.GetComponent<Text> ().text = hours.ToString("00") + ":" + minutes.ToString ("00") + ":" + seconds.ToString ("00");
	}
}
