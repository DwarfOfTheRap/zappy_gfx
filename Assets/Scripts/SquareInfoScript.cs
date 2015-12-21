using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SquareInfoScript : MonoBehaviour {

	public Material highlightedMaterial;
	public Material standardMaterial;
	public Material highlightedMaterial2;
	public Material standardMaterial2;
	public GameObject associatedGameObject;
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
			square = OnLeftMouseClick (Input.mousePosition);
		}
		if (Input.GetMouseButtonUp (1)) {
			GetComponent<CanvasGroup> ().alpha = 0;
			if (associatedGameObject != null)
				associatedGameObject.GetComponent<MeshRenderer> ().material = standardMaterial;
			associatedGameObject = null;
		}
	}

	public ISquare OnLeftMouseClick(Vector3 target)
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(target);
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider.gameObject.tag == "Floor1" || hit.collider.gameObject.tag == "Floor2")
			{
				GetComponent<CanvasGroup> ().alpha = 1;
				ISquare square = hit.collider.gameObject.GetComponent<SquareScript>() as ISquare;
				associatedGameObject = hit.collider.gameObject;
				return square;
			}
			if (hit.collider.gameObject.tag == "Floor1")
				associatedGameObject.GetComponent<MeshRenderer>().material = highlightedMaterial;
			if (hit.collider.gameObject.tag == "Floor2")
				associatedGameObject.GetComponent<MeshRenderer>().material = highlightedMaterial2;
		}
		GetComponent<CanvasGroup> ().alpha = 0;
		if (associatedGameObject != null) {
			associatedGameObject.GetComponent<MeshRenderer> ().material = standardMaterial;
			associatedGameObject = null;
		}
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
