using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeSliderText : MonoBehaviour {

	public void ChangeTextOnEvent(float value)
	{
		this.gameObject.GetComponent<Text> ().text = "t = " + ((int)value).ToString();
	}

	void Start ()
	{
		this.gameObject.GetComponent<Text> ().text = "t = " + GameManagerScript.instance.timeManager.timeSpeed;
	}
}
