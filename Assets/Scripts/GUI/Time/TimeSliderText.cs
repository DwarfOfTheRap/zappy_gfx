using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeSliderText : MonoBehaviour {
	void Update ()
	{
		this.gameObject.GetComponent<Text> ().text = "t = " + GameManagerScript.instance.timeManager.timeSpeed;
	}
}
