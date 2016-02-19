using UnityEngine;
using System.Collections;

public abstract class AInputManager
{
	public delegate void LeftClickEventHandler (ClickEventArgs ev);
	public delegate void RightClickEventHandler ();

	public event LeftClickEventHandler OnLeftClicking;
	public event RightClickEventHandler OnRightClicking;
	public event LeftClickEventHandler OnDoubleClicking;

	public abstract bool MoveLeft();
	public abstract bool MoveRight();
	public abstract float HorizontalMovementValue();

	public abstract bool MoveForward();
	public abstract bool MoveBackward();
	public abstract float VerticalMovementValue();

	public abstract bool MoveUp();
	public abstract bool MoveDown();

	public abstract bool ResetCamera();

	public abstract bool ScrollUp();
	public abstract bool ScrollDown();
	public abstract float DeltaScroll();
	
	public abstract bool DoubleMoveSpeed();
	public abstract bool StandardMoveSpeed();

	public abstract bool LeftClick();
	public abstract bool DoubleLeftClick();
	public abstract bool RightClick();

	public abstract bool MousingOverGameObject();

	public void OnLeftClick (ISquare sq)
	{
		if (OnLeftClicking != null)
			OnLeftClicking(new ClickEventArgs { square = sq } );
	}

	public void OnDoubleClick (ISquare sq)
	{
		if (OnDoubleClicking != null)
			OnDoubleClicking(new ClickEventArgs { square = sq } );
	}

	public void OnRightClick ()
	{
		if (OnRightClicking != null)
			OnRightClicking();
	}
}

public class ClickEventArgs : System.EventArgs
{
	public ISquare square { get; set; }
}