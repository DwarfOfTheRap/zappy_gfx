using UnityEngine;
using System.Collections;

public class InputManager : IInputManager 
{
	private float 				lastClickTime = 0.0f;
	private float 				catchTime = 0.25f;

	#region IInputManager implementation

	public bool MoveLeft ()
	{
		throw new System.NotImplementedException ();
	}

	public float LeftMovementValue ()
	{
		throw new System.NotImplementedException ();
	}

	public bool MoveRight ()
	{
		throw new System.NotImplementedException ();
	}

	public float RightMovementValue ()
	{
		throw new System.NotImplementedException ();
	}

	public bool MoveForward ()
	{
		throw new System.NotImplementedException ();
	}

	public float ForwardMovementValue ()
	{
		throw new System.NotImplementedException ();
	}

	public bool MoveBackward ()
	{
		throw new System.NotImplementedException ();
	}

	public float BackwardMovementValue ()
	{
		throw new System.NotImplementedException ();
	}

	public bool MoveUp ()
	{
		throw new System.NotImplementedException ();
	}

	public bool MoveDown ()
	{
		throw new System.NotImplementedException ();
	}

	public bool ResetCamera ()
	{
		throw new System.NotImplementedException ();
	}

	public bool ScrollUp ()
	{
		throw new System.NotImplementedException ();
	}

	public bool ScrollDown ()
	{
		throw new System.NotImplementedException ();
	}

	public float DeltaScroll ()
	{
		throw new System.NotImplementedException ();
	}

	public bool DoubleMoveSpeed ()
	{
		throw new System.NotImplementedException ();
	}

	public bool LeftClick ()
	{
		throw new System.NotImplementedException ();
	}

	public bool DoubleLeftClick ()
	{
		throw new System.NotImplementedException ();
	}

	public bool RightClick ()
	{
		throw new System.NotImplementedException ();
	}

	public bool MousingOverGameObject ()
	{
		throw new System.NotImplementedException ();
	}

	#endregion
}
