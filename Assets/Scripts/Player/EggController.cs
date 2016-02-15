using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;

[Serializable]
public class EggController {
	public int							index { get; private set; }
	public PlayerController				player { get; private set; } 
									
	public	ISquare						currentSquare;
									
	public bool							dead { get; private set; }

	public IAnimatorController			animatorController;
		
#if UNITY_EDITOR

#endif
	public void							SetAnimatorController(IAnimatorController animatorController)
	{
		this.animatorController = animatorController;
	}

	public EggController				Init(int x, int y, int index, GridController gridController)
	{
		try
		{
			this.currentSquare = gridController.GetSquare (x, y);
			this.index = index;
			return this;
		}
		catch (GridController.GridOutOfBoundsException)
		{
			return null;
		}
	}
}
