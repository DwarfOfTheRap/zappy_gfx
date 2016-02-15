using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class EggScript : MonoBehaviour, IAnimatorController {
	public EggController	controller;

	void Awake() {
		controller = new EggController();

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
	
	// Update is called once per frame
	void Update () {
	
	}
}
