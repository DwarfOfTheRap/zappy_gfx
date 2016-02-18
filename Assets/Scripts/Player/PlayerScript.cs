using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PlayerScript : MonoBehaviour, IAnimatorController, IPlayerMotorController{
	public PlayerController 	controller;
	public Orientation			orientation;

	private const float				_highlight_width = 0.0025f;

	private void OnEnable()
	{
		controller.SetAnimatorController(this);
		controller.SetPlayerMovementController(this);
		controller.SetPlayerOrientation(orientation);
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

	#region IPlayerMotorController implementation

	public bool IsMoving (Vector3 destination)
	{
		return this.transform.position != destination;
	}

	public Vector3 SetDestination (Vector3 destination)
	{
		return new Vector3(destination.x, transform.position.y, destination.z);
	}

	public void SetPosition (Vector3 vector3)
	{
		this.transform.position = new Vector3(vector3.x, this.transform.position.y, vector3.z);
	}

	public void MoveToDestination (Vector3 destination, float speed)
	{
		this.transform.position = Vector3.MoveTowards (this.transform.position, destination, Time.deltaTime * speed);
	}

	public void MoveToRotation (Quaternion rotation, float rotSpeed)
	{
		this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotation, Time.deltaTime * rotSpeed);
	}

	public void StopExpulsion()
	{
	} 

	public void Expulsed(Orientation orientation)
	{
	}

	public void EnableHighlight (Color color)
	{
		foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
		{
			renderer.materials[0].SetFloat ("_Outline", _highlight_width);
			renderer.materials[0].SetColor ("_OutlineColor", color);
		}
	}

	public void DisableHighlight ()
	{
		foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
		{
			renderer.materials[0].SetFloat ("_Outline", 0);
			renderer.materials[0].SetColor ("_OutlineColor", Color.black);
		}
	}

	public void SetTeamColor (Color color)
	{
		foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
		{
			renderer.materials[1].color = color;
		}
		GetComponentInChildren<Light>().color = color;
	}

	#endregion

	void Update()
	{
		controller.Update (this.transform.position);
	}
}
