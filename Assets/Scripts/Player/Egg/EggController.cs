﻿using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;

[Serializable]
public class EggController
{
	public int							index { get; private set; }

	public PlayerController				parent { get; private set; }

	public Team							team {
		get { return parent.team; }
		set {}
	}
									
	public	ISquare						currentSquare;
									
	public bool							dead { get; private set; }

	public IAnimatorController			animatorController;
	public IEggMotorController			motorController;
		
#if UNITY_EDITOR

#endif
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
		this.motorController.Init ();
		this.motorController.SetTeamColor (parent.team.color);
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
	}

	public void PlayerConnection ()
	{
		animatorController.SetTrigger ("Death");
		motorController.Die ();
	}
}