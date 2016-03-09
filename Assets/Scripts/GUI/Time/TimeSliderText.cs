using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeSliderText : MonoBehaviour {
	public void OnSliderValueChange(GameObject slider)
	{
		this.gameObject.GetComponent<Text> ().text = "t = " + slider.GetComponent<Slider>().value;
	}

	void Start()
	{
		this.gameObject.GetComponent<Text> ().text = "t = " + GameManagerScript.instance.timeManager.timeSpeed.ToString ();
	}
}
