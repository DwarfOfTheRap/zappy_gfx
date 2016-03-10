using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class EggScript : MonoBehaviour, IAnimatorController, IEggMotorController {
	public EggController		controller { get; private set; }
	public HologramScript		hologramPrefab;

	private HologramController	_hologram;

	void Awake() {
		controller = new EggController();
		controller.SetAnimatorController(this);
		controller.SetMotorController(this);
	}

	void Update()
	{
		GetComponent<Animator>().SetFloat ("Speed", GameManagerScript.instance.timeManager.timeSpeed / 10.0f);
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

	public void Hatch ()
	{
		GetComponentInChildren<ParticleSystem>().Stop ();
		var clone = Instantiate (hologramPrefab) as HologramScript;
		clone.transform.SetParent (this.transform);
		clone.transform.localPosition = Vector3.zero;
		clone.controller.SetTeamColors (controller.team.color);
		_hologram = clone.controller;
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

	public void Die ()
	{
		_hologram.Die ();
	}

	public void Destroy ()
	{
		Destroy (gameObject);
	}

	// Animation events
	public void OnInitAnimationEnd()
	{
		GetComponentInChildren<ParticleSystem>().Play();
	}

	public void OnDeathAnimationEnd()
	{
		Destroy (gameObject);
	}
}


public interface IEggMotorController
{
	void Hatch();
	void SetTeamColor(Color color);
	void SetPosition(Vector3 position);
	void Die ();
	void Destroy ();
}
