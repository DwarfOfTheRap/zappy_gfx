using UnityEngine;
using System.Collections;

public class InputManager : AInputManager 
{
	private float 				lastClickTime = 0.0f;
	private float 				catchTime = 0.25f;

	#region AInputManager implementation

	public override bool MoveLeft ()
	{
		return (Input.GetAxis ("Horizontal") < 0);
	}

	public override bool MoveRight ()
	{
		return (Input.GetAxis ("Horizontal") > 0);
	}

	public override float HorizontalMovementValue ()
	{
		return (Input.GetAxis ("Horizontal"));
	}		

	public override bool MoveForward ()
	{
		return (Input.GetAxis ("Vertical") > 0);
	}

	public override bool MoveBackward ()
	{
		return (Input.GetAxis ("Vertical") < 0);
	}

	public override float VerticalMovementValue ()
	{
		return (Input.GetAxis ("Vertical"));
	}

	public override bool MoveUp ()
	{
		return (Input.GetKey (KeyCode.Q));
	}

	public override bool MoveDown ()
	{
		return (Input.GetKey (KeyCode.E));
	}

	public override bool ResetCamera ()
	{
		return (Input.GetKeyUp (KeyCode.Space));
	}

	public override bool ScrollUp ()
	{
		return (Input.GetAxis ("Mouse ScrollWheel") < 0);
	}

	public override bool ScrollDown ()
	{
		return (Input.GetAxis ("Mouse ScrollWheel") > 0);
	}

	public override float DeltaScroll ()
	{
		return (Input.GetAxis ("Mouse ScrollWheel"));
	}

	public override bool DoubleMoveSpeed ()
	{
		return (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift));
	}

	public override bool StandardMoveSpeed ()
	{
		return (Input.GetKeyUp (KeyCode.LeftShift) || Input.GetKeyUp (KeyCode.RightShift));
	}

	public override bool LeftClick ()
	{
		return (Input.GetMouseButtonUp (0));
	}

	public override bool DoubleLeftClick ()
	{
		return (Time.time - lastClickTime < catchTime);
	}

	public override bool RightClick ()
	{
		return (Input.GetMouseButtonUp (1));
	}

	public override bool MousingOverGameObject ()
	{
		return (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ());
	}
	
	#endregion

	public void CheckInput ()
	{
		if (LeftClick ())
		{
			LeftMouseClick ();
			lastClickTime = Time.time;
		}
		if (RightClick ())
			RightMouseClick ();
	}

	void LeftMouseClick ()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			IClickTarget target = null;
			if ((hit.collider.gameObject.tag == "Floor1" || hit.collider.gameObject.tag == "Floor2") && !MousingOverGameObject())
				target = hit.collider.gameObject.GetComponent<SquareScript> ();
			if ((hit.collider.gameObject.tag == "Player") && !MousingOverGameObject ())
				target = hit.collider.gameObject.GetComponent<PlayerScript> ();
			if (target != null)
			{
				this.OnLeftClick (target);
				if (DoubleLeftClick())
					this.OnDoubleClick (target);
			}
		}
	}

	void RightMouseClick ()
	{
		this.OnRightClick ();
	}
}
