using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SquareContentUI : MonoBehaviour {

    public ISquare square;
    public Text linemateNumber;
    public Text deraumereNumber;
    public Text siburNumber;
    public Text mendianeNumber;
    public Text phirasNumber;
    public Text thystameNumber;
    public Text nourritureNumber;
    public Text playersNumber;
	public Text eggsNumber;

    void DisplayResources (ISquare square)
    {
        linemateNumber.text = square.GetResources ().linemate.count.ToString ();
		linemateNumber.color = ResourceController.linemateColor;
        deraumereNumber.text = square.GetResources ().deraumere.count.ToString ();
		deraumereNumber.color = ResourceController.deraumereColor;
        siburNumber.text = square.GetResources ().sibur.count.ToString ();
		siburNumber.color = ResourceController.siburColor;
        mendianeNumber.text = square.GetResources ().mendiane.count.ToString ();
		mendianeNumber.color = ResourceController.mendianeColor;
        phirasNumber.text = square.GetResources ().phiras.count.ToString ();
		phirasNumber.color = ResourceController.phirasColor;
        thystameNumber.text = square.GetResources ().thystame.count.ToString ();
		thystameNumber.color = ResourceController.thystameColor;
        nourritureNumber.text = square.GetResources ().nourriture.count.ToString ();
		nourritureNumber.color = ResourceController.foodColor;
        playersNumber.text = square.GetResources ().players.Count.ToString ();
		eggsNumber.text = square.GetResources ().eggs.Count.ToString ();
    }

    void DisplayWindow (ClickEventArgs args)
    {
		if (!args.target.IsSquare())
		{
			HideWindow ();
			return ;
		}
		this.square = (ISquare)args.target;
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
        GameManagerScript.instance.inputManager.OnLeftClicking += DisplayWindow;
        GameManagerScript.instance.inputManager.OnRightClicking += HideWindow;
    }

	void OnDisable ()
	{
		GameManagerScript.instance.inputManager.OnLeftClicking -= DisplayWindow;
		GameManagerScript.instance.inputManager.OnRightClicking -= HideWindow;
	}

    void Update () {
        if (square != null)
            DisplayResources(square);
    }
}
