using UnityEngine;
using System.Collections;

public class InputManager : IInputManager 
{
	public delegate void LeftClickEvent (ISquare square);
	public delegate void RightClickEvent ();
	public delegate void DoubleClickEvent (ISquare square);
	public static event LeftClickEvent OnLeftClick;
	public static event RightClickEvent OnRightClick;
	public static event DoubleClickEvent OnDoubleClick;

	private float 				lastClickTime = 0.0f;
	private float 				catchTime = 0.25f;

	#region IInputManager implementation

	public bool MoveLeft ()
	{
		return (Input.GetAxis ("Horizontal") < 0);
	}

	public bool MoveRight ()
	{
		return (Input.GetAxis ("Horizontal") > 0);
	}

	public float HorizontalMovementValue ()
	{
		return (Input.GetAxis ("Horizontal"));
	}		

	public bool MoveForward ()
	{
		return (Input.GetAxis ("Vertical") > 0);
	}

	public bool MoveBackward ()
	{
		return (Input.GetAxis ("Vertical") < 0);
	}

	public float VerticalMovementValue ()
	{
		return (Input.GetAxis ("Vertical"));
	}

	public bool MoveUp ()
	{
		return (Input.GetKey (KeyCode.Q));
	}

	public bool MoveDown ()
	{
		return (Input.GetKey (KeyCode.E));
	}

	public bool ResetCamera ()
	{
		return (Input.GetKeyUp (KeyCode.Space));
	}

	public bool ScrollUp ()
	{
		return (Input.GetAxis ("Mouse ScrollWheel") < 0);
	}

	public bool ScrollDown ()
	{
		return (Input.GetAxis ("Mouse ScrollWheel") > 0);
	}

	public float DeltaScroll ()
	{
		return (Input.GetAxis ("Mouse ScrollWheel"));
	}

	public bool DoubleMoveSpeed ()
	{
		return (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift));
	}

	public bool StandardMoveSpeed ()
	{
		return (Input.GetKeyUp (KeyCode.LeftShift) || Input.GetKeyUp (KeyCode.RightShift));
	}

	public bool LeftClick ()
	{
		return (Input.GetMouseButtonUp (0));
	}

	public bool DoubleLeftClick ()
	{
		return (Time.time - lastClickTime < catchTime);
	}

	public bool RightClick ()
	{
		return (Input.GetMouseButtonUp (1));
	}

	public bool MousingOverGameObject ()
	{
		return (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ());
	}

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
			if (hit.collider.gameObject.tag == "Floor1" || hit.collider.gameObject.tag == "Floor2")
			{
				ISquare square = hit.collider.gameObject.GetComponent<SquareScript> () as ISquare;

				if (OnLeftClick != null)
				{
					OnLeftClick (square);
				}
				if (DoubleLeftClick() && OnDoubleClick != null)
				{
					OnDoubleClick (square);
				}
			}
		}
	}

	void RightMouseClick ()
	{
		if (OnRightClick != null)
			OnRightClick ();
	}

	#endregion
}
