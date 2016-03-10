using UnityEngine;
using System.Collections;

public abstract class AInputManager
{
	// Click Events
	public delegate void SquareClickEventHandler (ClickEventArgs ev);
	public delegate void ClickEventHandler ();
	public event SquareClickEventHandler OnLeftClicking;
	public event ClickEventHandler OnRightClicking;
	public event SquareClickEventHandler OnDoubleClicking;

	// Move
	public abstract bool MoveLeft();
	public abstract bool MoveRight();
	public abstract bool MoveUp();
	public abstract bool MoveDown();
	public abstract bool MoveForward();
	public abstract bool MoveBackward();
	
	public abstract bool DoubleMoveSpeed();
	public abstract bool StandardMoveSpeed();
	public abstract float HorizontalMovementValue();
	public abstract float VerticalMovementValue();

	// Click
	public abstract bool LeftClick();
	public abstract bool DoubleLeftClick();
	public abstract bool RightClick();

	// Scroll
	public abstract bool ScrollUp();
	public abstract bool ScrollDown();
	public abstract float DeltaScroll();

	// Action
	public abstract bool ResetCamera();
	public abstract bool MenuKey();

	// Other
	public abstract bool MousingOverGameObject();

	protected void OnLeftClick (IClickTarget target)
	{
		if (OnLeftClicking != null)
			OnLeftClicking(new ClickEventArgs { target = target } );
	}

	protected void OnDoubleClick (IClickTarget target)
	{
		if (OnDoubleClicking != null)
			OnDoubleClicking(new ClickEventArgs { target = target } );
	}

	protected void OnRightClick ()
	{
		if (OnRightClicking != null)
			OnRightClicking();
	}
}

public class ClickEventArgs : System.EventArgs
{
	public IClickTarget target { get; set; }
}

public interface IClickTarget
{
	bool IsSquare();
	bool IsPlayer();
	Vector3 GetPosition();
}