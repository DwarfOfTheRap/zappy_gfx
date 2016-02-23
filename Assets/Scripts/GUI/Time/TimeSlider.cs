using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeSlider : MonoBehaviour {

	public void ChangeTextOnEvent(float value)
	{
		this.gameObject.GetComponent<Text> ().text = "t = " + ((int)value).ToString();
	}

	void Start ()
	{
		this.gameObject.GetComponent<Text> ().text = "t = " + GameManagerScript.instance.timeSpeed;
	}
}
