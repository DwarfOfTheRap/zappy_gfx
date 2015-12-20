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

	public void CheckInput()
	{
		if (Input.GetMouseButtonUp (0)) {
			square = OnLeftMouseClick ();
		}
		if (Input.GetMouseButtonUp (1)) {
			GetComponent<CanvasGroup> ().alpha = 0;
		}
	}

	public ISquare OnLeftMouseClick()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider.gameObject.tag == "Floor1" || hit.collider.gameObject.tag == "Floor2")
			{
				GetComponent<CanvasGroup> ().alpha = 1;
				ISquare square = hit.collider.gameObject.GetComponent<SquareScript>() as ISquare;
				return square;
			}
		}
		GetComponent<CanvasGroup> ().alpha = 0;
		return null;
	}

	void DisplayResources (ISquare square)
	{
		linemate.text = square.GetResources ()[SquareContent.LINEMATE].ToString ();
		deraumere.text = square.GetResources ()[SquareContent.DERAUMERE].ToString ();
		sibur.text = square.GetResources ()[SquareContent.SIBUR].ToString ();
		mendiane.text = square.GetResources ()[SquareContent.MENDIANE].ToString ();
		phiras.text = square.GetResources ()[SquareContent.PHIRAS].ToString ();
		thystame.text = square.GetResources ()[SquareContent.THYSTAME].ToString ();
		nourriture.text = square.GetResources ()[SquareContent.NOURRITURE].ToString ();
		players.text = square.GetResources ()[SquareContent.PLAYERS].ToString ();
	}
	
	void Update () {
		CheckInput ();
		if (square != null)
			DisplayResources(square);
	}
}
