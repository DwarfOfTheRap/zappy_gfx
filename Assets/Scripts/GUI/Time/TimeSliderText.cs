using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeSliderText : MonoBehaviour {

	public Slider slider;

	public void OnSliderValueChange(GameObject slider)
	{
		this.gameObject.GetComponent<InputField>().text = slider.GetComponent<Slider>().value.ToString ();
	}

	void OnEndEdit (string input)
	{
		int value = int.Parse(input);
		GameManagerScript.instance.timeManager.ChangeTimeSpeedInput (Mathf.Clamp (value, slider.minValue, slider.maxValue));
	}

	void Start()
	{
		this.gameObject.GetComponent<InputField>().text = GameManagerScript.instance.timeManager.timeSpeed.ToString ();
		this.gameObject.GetComponent<InputField>().onEndEdit.AddListener (OnEndEdit);
	}
}
