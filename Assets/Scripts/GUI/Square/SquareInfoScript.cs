using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SquareInfoScript : MonoBehaviour {

	public ISquare square;
	public Text linemateNumber;
	public Text deraumereNumber;
	public Text siburNumber;
	public Text mendianeNumber;
	public Text phirasNumber;
	public Text thystameNumber;
	public Text nourritureNumber;
	public Text playersNumber;

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
		linemateNumber.text = square.GetResources ().linemate.count.ToString ();
		linemateNumber.color = square.GetResources ().linemate.GetColor ();
		deraumereNumber.text = square.GetResources ().deraumere.count.ToString ();
		deraumereNumber.color = square.GetResources ().deraumere.GetColor ();
		siburNumber.text = square.GetResources ().sibur.count.ToString ();
		siburNumber.color = square.GetResources ().sibur.GetColor ();
		mendianeNumber.text = square.GetResources ().mendiane.count.ToString ();
		mendianeNumber.color = square.GetResources ().mendiane.GetColor ();
		phirasNumber.text = square.GetResources ().phiras.count.ToString ();
		phirasNumber.color = square.GetResources ().phiras.GetColor ();
		thystameNumber.text = square.GetResources ().thystame.count.ToString ();
		thystameNumber.color = square.GetResources ().thystame.GetColor ();
		nourritureNumber.text = square.GetResources ().nourriture.count.ToString ();
		nourritureNumber.color = square.GetResources ().nourriture.GetColor ();
		playersNumber.text = square.GetResources ().players.Count.ToString ();
	}
	
	void Update () {
		CheckInput ();
		if (square != null)
			DisplayResources(square);
	}
}
