using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeCount : MonoBehaviour {
	private int				totalGameSeconds = 0;

	IEnumerator TimeUpdate ()
	{
		while (true) {
			if (GameManagerScript.instance.timeSpeed > 0) {
				yield return new WaitForSeconds (1.0f / GameManagerScript.instance.timeSpeed);
				if (GameManagerScript.instance.timeSpeed > 0)
					totalGameSeconds += 1;
			}
			else
				yield return new WaitForEndOfFrame();
		}
	}

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Text> ().text = "00:00";

		StartCoroutine (TimeUpdate());
	}
	
	// Update is called once per frame
	void Update () {
		int seconds;
		int minutes;

		seconds = totalGameSeconds % 60;
		minutes = totalGameSeconds / 60;

		this.gameObject.GetComponent<Text> ().text = minutes.ToString ("00") + ":" + seconds.ToString ("00");
	}
}
