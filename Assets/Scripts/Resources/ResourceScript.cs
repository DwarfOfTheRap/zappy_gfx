using UnityEngine;
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

	void Awake()
	{
		controller = new ResourceController(this);
	}

	public Color GetColor()
	{
		Color color = GetComponentInChildren<Renderer>().material.color;
		return new Color(color.r, color.g, color.b, 1);
	}

	public void Enable (bool state)
	{
		GetComponentInChildren<Renderer>().enabled = state;
		GetComponentInChildren<Animation>().enabled = state;
	}

	void Update()
	{
		controller.Update ();
	}
}

[System.Serializable]
public class ResourceController
{
	public uint			count;
	IResourceEnabler	motor;

	public ResourceController(IResourceEnabler motor)
	{
		this.motor = motor;
	}

	void Enable()
	{
		motor.Enable (count > 0);
	}

	public Color GetColor()
	{
		return motor.GetColor ();
	}

	public void Update()
	{
		Enable ();
	}
}

public interface IResourceEnabler {
	void Enable(bool state);
	Color GetColor();
}