using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;

[Serializable]
public class EggController
{
	// Game attributes
	public int							index { get; private set; }
	public PlayerController				parent { get; private set; }
	public Team							team {
		get { return parent.team; }
		set {}
	}
	public bool							dead { get; private set; }

	// Internal attribute
	public	ISquare						currentSquare { get; private set; }

	// Controller
	public IAnimatorController			animatorController { get; private set; }
	public IEggMotorController			motorController { get; private set; }

	public void							SetAnimatorController (IAnimatorController animatorController)
	{
 		this.animatorController = animatorController;
	}

	public void							SetMotorController (IEggMotorController motorController)
	{
		this.motorController = motorController;
	}

	public EggController				Init (int x, int y, int index, PlayerController parent, GridController gridController)
	{
		this.currentSquare = gridController.GetSquare (x, y);
		motorController.SetPosition (this.currentSquare.GetPosition ());
		this.index = index;
		this.parent = parent;
		this.motorController.SetTeamColor (parent.team.color);
		this.currentSquare.GetResources ().eggs.Add (this);
		return this;
	}

	public void Hatch ()
	{
		motorController.Hatch ();
	}

	public void Die ()
	{
		animatorController.SetTrigger ("Death");
		motorController.Die ();
		this.currentSquare.GetResources ().eggs.Remove (this);
	}

	public void PlayerConnection ()
	{
		animatorController.SetTrigger ("Death");
		motorController.Die ();
		this.currentSquare.GetResources ().eggs.Remove (this);
	}

	public void Destroy ()
	{
		motorController.Destroy();
	}
}
