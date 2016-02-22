using UnityEngine;
using System.Collections;

public class ResourceScript : MonoBehaviour, IResource {
	public ResourceController	controller;
	
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
	}
}

[System.Serializable]
public class ResourceController
{
	public static Color	linemateColor { get { return Color.white; } }
	public static Color deraumereColor { get { return new Color(119/255.0f, 13/255.0f, 80/255.0f); }}
	public static Color	siburColor { get { return new Color(242/255.0f, 29/255.0f, 68/255.0f); }}
	public static Color	mendianeColor { get { return new Color(255/255.0f, 137/255.0f, 48/255.0f); }}
	public static Color	phirasColor { get { return new Color(255/255.0f, 212/255.0f, 53/255.0f); }}
	public static Color	thystameColor { get { return Color.gray; }}
	public static Color	foodColor { get { return Color.cyan; }}
	public Color		color { get; private set;}
	IResource	motor;
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
	
	public ResourceController(IResource motor, Color color)
	{
		this.motor = motor;
		this.color = color;
		this.count = 1;
	}
	
	void Enable(bool state)
	{
		motor.Enable (state);
	}
	
	public override string ToString()
	{
		return _count.ToString ();
	}
}

public interface IResource {
	void Enable(bool state);
}