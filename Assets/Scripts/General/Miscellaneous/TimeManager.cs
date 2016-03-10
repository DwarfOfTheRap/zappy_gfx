using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeManager {
	public float		timeSpeed { get; private set; }

	private Slider		_slider;

	public TimeManager()
	{
		timeSpeed = 10.0f;
	}

	public void SetSlider (Slider slider)
	{
		this._slider = slider;
		slider.value = timeSpeed;
	}

	public virtual void ChangeTimeSpeedClient(float value)
	{
		timeSpeed = value;
		var query = new ServerQuery();
		query.SendTimeUnitChangeQuery ((int)value);
	}

	public virtual void ChangeTimeSpeedServer(float value)
	{
		timeSpeed = value;
		if (_slider)
			this._slider.value = value;
	}
}
