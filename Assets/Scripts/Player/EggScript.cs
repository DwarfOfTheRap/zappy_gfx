﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class EggScript : MonoBehaviour, IAnimatorController, IEggMotorController {
	public EggController	controller;

	void Awake() {
		controller = new EggController();
		controller.SetAnimatorController(this);
		controller.SetMotorController(this);
	}
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

	#region IEggMotorController implementation

	public void Init()
	{
	}

	public void SetTeamColor (Color color)
	{
		foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
		{
			foreach (Material material in renderer.materials)
				material.color = color;
		}
		GetComponentInChildren<Light>().color = color;
		GetComponentInChildren<ParticleSystem>().startColor = color;
	}
	
	public void SetPosition (Vector3 position)
	{
		this.transform.position = new Vector3(position.x, this.transform.position.y, position.z);
	}

	#endregion

	public void OnInitAnimationEnd()
	{
		GetComponentInChildren<ParticleSystem>().Play();
	}

	public void OnDeathAnimationEnd()
	{
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


public interface IEggMotorController
{
	void SetTeamColor(Color color);
	void SetPosition(Vector3 position);
	void Init ();
}
