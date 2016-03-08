using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager {
	public float		timeSpeed;
	public Slider		slider { get; private set;}

	public TimeManager()
	{
		timeSpeed = 10.0f;
	}

	public void SetSlider (Slider slider)
	{
		this.slider = slider;
		slider.value = timeSpeed;
	}

	public void ChangeTimeSpeedClient(float value)
	{
		timeSpeed = value;
		var query = new ServerQuery();
		query.SendTimeUnitChangeQuery ((int)value);
	}

	public void ChangeTimeSpeedServer(float value)
	{
		timeSpeed = value;
		this.slider.value = value;
	}
}
