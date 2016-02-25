using UnityEngine;
using System.Collections;

public class TimeManager {
	public float		timeSpeed;

	public TimeManager()
	{
		timeSpeed = 10.0f;
	}

	public void ChangeTimeSpeed(float value)
	{
		timeSpeed = value;
	}
}
