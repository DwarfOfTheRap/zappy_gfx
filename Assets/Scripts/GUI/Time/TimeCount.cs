using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeCount : MonoBehaviour {

	private string			timeString;
	private float			totalGameSeconds;

	// Use this for initialization
	void Start () {
		totalGameSeconds += GameManagerScript.instance.timeSpeed * Time.deltaTime;
		this.gameObject.GetComponent<Text> ().text = "00:00";
	}
	
	// Update is called once per frame
	void Update () {
		float seconds;
		float minutes;

		totalGameSeconds += GameManagerScript.instance.timeSpeed * Time.deltaTime;
		seconds = totalGameSeconds % 60.0f;
		minutes = totalGameSeconds / 60.0f;

		if (minutes < 10)
			timeString = "0" + ((int)minutes).ToString () + ":";
		else
			timeString = ((int)minutes).ToString () + ":";
		if (seconds < 10)
			timeString += "0" + ((int)seconds).ToString ();
		else
			timeString += ((int)seconds).ToString ();
		this.gameObject.GetComponent<Text> ().text = timeString;
	}
}
