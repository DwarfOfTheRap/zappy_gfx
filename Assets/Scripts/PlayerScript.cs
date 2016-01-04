using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PlayerScript : MonoBehaviour, IAnimatorController, IPlayerMovementController {
	public PlayerController controller;
	public PlayerLegsScript[] legs;
	public Orientation		orientation;

	private void OnEnable()
	{
		controller.SetAnimatorController(this);
		controller.SetPlayerMovementController(this);
		controller.SetPlayerOrientation(orientation);
		legs = GetComponentsInChildren<PlayerLegsScript>();
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

	#region IPlayerMovementController implementation

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
		foreach (PlayerLegsScript leg in legs)
			leg.DisableSparksAnimation();
	} 

	public void Expulsed(Orientation orientation)
	{
		foreach (PlayerLegsScript leg in legs)
			leg.EnableSparksAnimation(orientation);
	}

	#endregion

	void Update()
	{
		controller.Update (this.transform.position);
	}
}
