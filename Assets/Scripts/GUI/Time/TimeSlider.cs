using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeSlider : MonoBehaviour {
	
	private string[]		speeds = {"Stop", "Slow", "Play", "x2", "x4"};

	public void ChangeTextOnEvent(float value)
	{
		this.gameObject.GetComponent<Text> ().text = speeds [((int)value)];
	}

	void Start ()
	{
		this.gameObject.GetComponent<Text> ().text = "Play";
	}
}
