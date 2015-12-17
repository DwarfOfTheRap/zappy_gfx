using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class PlayerController {
	public int			index;
	public float		speed = 1.0f;
	public bool 		isIncantating { get; private set; }
	private Vector2		positionIndex;
	public Vector3		destination;
	
	private IAnimatorController	animatorController;
	private IPlayerMovementController playerMovementController;

	public void SetAnimatorController(IAnimatorController animatorController)
	{
		this.animatorController = animatorController;
	}

	public void SetPlayerMovementController(IPlayerMovementController playerMovementController)
	{
		this.playerMovementController = playerMovementController;
	}

	public void Incantate()
	{
		isIncantating = true;
		animatorController.SetBool ("Incantate", true);
	}

	public void StopIncantating()
	{
		isIncantating = false;
		animatorController.SetBool("Incante", false);
	}

	public void SetPosition(int x, int y)
	{
		if (this.positionIndex.x != x || this.positionIndex.y != y)
			playerMovementController.SetDestination (x, y);
		this.positionIndex.x = x;
		this.positionIndex.y = y;
	}

	void GoToDestination()
	{
		animatorController.SetBool ("Walk", playerMovementController.IsMoving ());
	}
	
	public void Update()
	{
		GoToDestination ();
	}
}
