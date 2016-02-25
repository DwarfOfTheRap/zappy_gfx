using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {
	public float							timeSpeed;

	void OnEnable()
	{
		timeSpeed = 10.0f;
	}
	
	public void ChangeTimeSpeed(float value)
	{
		timeSpeed = value;
	}
}
