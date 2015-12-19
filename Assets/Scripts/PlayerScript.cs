using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PlayerScript : MonoBehaviour, IAnimatorController, IPlayerMovementController {
	public PlayerController controller;

	public Vector3			destination;

	private void OnEnable()
	{
		controller.SetAnimatorController(this);
		controller.SetPlayerMovementController(this);
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

	public bool IsMoving ()
	{
		return this.transform.position != destination;
	}

	public void SetDestination (int x, int y)
	{
		Vector3 tmp = GameManagerScript.instance.grid.controller.GetSquare (x, y).GetPosition();
		destination = new Vector3(tmp.x, transform.position.y, tmp.z);
	}

	public void MoveToDestination (float speed)
	{
		this.transform.position = Vector3.MoveTowards (this.transform.position, destination, Time.deltaTime * speed);
	}

	public void MoveToRotation (Quaternion rotation, float rotSpeed)
	{
		this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotation, Time.deltaTime * rotSpeed);
	}

	#endregion

	void Update()
	{
		controller.Update (this.transform.position, this.destination);
	}
}
