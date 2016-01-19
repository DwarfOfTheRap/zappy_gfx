using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SquareInfoScript : MonoBehaviour {

	public ISquare square;
	public Text linemate;
	public Text deraumere;
	public Text sibur;
	public Text mendiane;
	public Text phiras;
	public Text thystame;
	public Text nourriture;
	public Text players;

	void CheckInput()
	{
		if (Input.GetMouseButtonUp (0)) {
			if (square != null)
				square.Standard();
			square = OnLeftMouseClick (Input.mousePosition);

		}
		if (Input.GetMouseButtonUp (1)) {
			if (square != null)
				square.Standard();
			square = null;
			GetComponent<CanvasGroup> ().alpha = 0;
		}
	}

	public ISquare OnLeftMouseClick(Vector3 target)
	{
		ISquare square = null;
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(target);
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider.gameObject.tag == "Floor1" || hit.collider.gameObject.tag == "Floor2")
			{
				GetComponent<CanvasGroup> ().alpha = 1;
				square = hit.collider.gameObject.GetComponent<SquareScript>() as ISquare;
				square.Highlighted();
				return square;
			}
		}
		GetComponent<CanvasGroup> ().alpha = 0;
		return null;
	}

	void DisplayResources (ISquare square)
	{
		linemate.text = square.GetResources ().linemate.ToString ();
		deraumere.text = square.GetResources ().deraumere.ToString ();
		sibur.text = square.GetResources ().sibur.ToString ();
		mendiane.text = square.GetResources ().mendiane.ToString ();
		phiras.text = square.GetResources ().phiras.ToString ();
		thystame.text = square.GetResources ().thystame.ToString ();
		nourriture.text = square.GetResources ().nourriture.ToString ();
		players.text = square.GetResources ().players.Count.ToString ();
	}
	
	void Update () {
		CheckInput ();
		if (square != null)
			DisplayResources(square);
	}
}
