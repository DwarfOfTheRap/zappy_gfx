using UnityEngine;
using System.Collections;

public class HologramScript : MonoBehaviour, IHologram, IAnimatorController {
	public HologramController		controller;
	// Use this for initialization
	void Awake () {
		controller = new HologramController(this, this);
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Animator>().SetFloat ("Speed", GameManagerScript.instance.timeManager.timeSpeed / 10.0f);
	}

	#region IAnimatorController implementation

	public void SetBool (string name, bool value)
	{
		GetComponent<Animator>().SetBool (name, value);
	}
	
	public void SetFloat (string name, float value)
	{
		GetComponent<Animator>().SetFloat (name, value);
	}
	
	public void SetInteger (string name, int value)
	{
		GetComponent<Animator>().SetInteger (name, value);
	}
	
	public void SetTrigger (string name)
	{
		GetComponent<Animator>().SetTrigger (name);
	}

	#endregion

	#region IHologram implementation

	public void SetTeamColors (Color color)
	{
		foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
		{
			foreach (Material material in renderer.materials)
				material.SetColor ("_RimColor", color == Color.cyan ? Color.white : color);
		}
	}

	#endregion

	public void OnDeathAnimationEnd()
	{
		Destroy (gameObject);
	}
}

public class HologramController
{
	IHologram			motor;
	IAnimatorController animatorController;

	public HologramController (IHologram motor, IAnimatorController animatorController)
	{
		this.motor = motor;
		this.animatorController = animatorController;
	}

	public void SetTeamColors (Color color)
	{
		motor.SetTeamColors(color);
	}

	public void Die()
	{
		animatorController.SetTrigger ("Death");
	}
}

public interface IHologram
{
	void SetTeamColors (Color color);
}
