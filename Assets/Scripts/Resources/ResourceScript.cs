﻿using UnityEngine;
using System.Collections;

public class ResourceScript : MonoBehaviour, IResourceEnabler {
	public ResourceController controller;

	void OnEnable()
	{
		Animation animation = GetComponentInChildren<Animation>();
		foreach (AnimationState state in animation)
		{
			state.time = Random.Range (0, state.length);
		}
		animation.Play();
	}

	public void Init()
	{
		Color color = GetComponentInChildren<Renderer>().material.color;
		controller = new ResourceController(this, new Color(color.r, color.g, color.b, 1));
	}

	public void Enable (bool state)
	{
		GetComponentInChildren<Renderer>().enabled = state;
		GetComponentInChildren<Animation>().enabled = state;
		this.gameObject.SetActive(state);
	}
}

[System.Serializable]
public class ResourceController
{

	public Color		color { get; private set;}
	IResourceEnabler	motor;
	[SerializeField]
	private uint		_count;
	public uint			count {
		get {
			return _count;
		}
		set { 
			_count = value;
			this.Enable (count > 0);
			}
	}

	public ResourceController(IResourceEnabler motor, Color color)
	{
		this.motor = motor;
		this.color = color;
		this.count = count;
	}

	void Enable(bool state)
	{
		motor.Enable (state);
	}
}

public interface IResourceEnabler {
	void Enable(bool state);
}