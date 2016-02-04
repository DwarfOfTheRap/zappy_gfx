using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SquareContentUI : MonoBehaviour {

	public ISquare square;
	public Text linemate;
	public Text deraumere;
	public Text sibur;
	public Text mendiane;
	public Text phiras;
	public Text thystame;
	public Text nourriture;
	public Text players;

	public void DisplayResources (ISquare square)
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

	void DisplayWindow (ISquare square)
	{
		this.square = square;
		this.GetComponent<CanvasGroup> ().alpha = 1;
		this.GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}

	void HideWindow ()
	{
		this.square = null;
		this.GetComponent<CanvasGroup> ().alpha = 0;
		this.GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	void Start () {
		InputManager.OnLeftClick += DisplayWindow;
		InputManager.OnRightClick += HideWindow;
	}
	
	void Update () {
		if (square != null)
			DisplayResources(square);
	}
}
