using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PlayerScript : MonoBehaviour, IAnimatorController, IPlayerMovementController {
	public PlayerController controller;

	private Vector3			destination;

	private void OnEnable()
	{
		controller.SetAnimatorController(this);
		controller.SetPlayerMovementController(this);
		this.destination = this.transform.position;
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
		return this.transform.position != controller.destination;
	}

	public void SetDestination (int x, int y)
	{
		Vector3 tmp = GameManagerScript.instance.grid.GetSquare (x, y).transform.position;
		destination = new Vector3(tmp.x, transform.position.y, tmp.z);
	}

	public void MoveToDestination (float speed)
	{
		this.transform.position = Vector3.MoveTowards (this.transform.position, destination, Time.deltaTime * speed);
	}

	#endregion

	void Update()
	{
		controller.Update ();
	}
}
