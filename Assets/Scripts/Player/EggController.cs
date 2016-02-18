using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;

[Serializable]
public class EggController {
	public int							index { get; private set; }
	public PlayerController				parent { get; private set; } 
	public Team							team { get { return parent.team; } set {} }
									
	public	ISquare						currentSquare;
									
	public bool							dead { get; private set; }

	public IAnimatorController			animatorController;
	public IEggMotorController			motorController;
		
#if UNITY_EDITOR

#endif
	public void							SetAnimatorController(IAnimatorController animatorController)
	{
		this.animatorController = animatorController;
	}

	public void							SetMotorController(IEggMotorController motorController)
	{
		this.motorController = motorController;
	}

	public EggController				Init(int x, int y, int index, PlayerController parent, GridController gridController)
	{
		try
		{
			this.currentSquare = gridController.GetSquare (x, y);
			this.index = index;
			this.parent = parent;
			this.motorController.SetTeamColor (parent.team.color);
			return this;
		}
		catch (GridController.GridOutOfBoundsException)
		{
			return null;
		}
	}

	public void Hatch ()
	{
		throw new NotImplementedException ();
	}

	public void Die ()
	{
		throw new NotImplementedException ();
	}

	public void PlayerConnection ()
	{
		throw new NotImplementedException ();
	}
}
