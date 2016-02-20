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

    public void DisplayResources (ISquare square)
    {
        linemateNumber.text = square.GetResources ().linemate.count.ToString ();
        linemateNumber.color = square.GetResources ().linemate.color;
        deraumereNumber.text = square.GetResources ().deraumere.count.ToString ();
        deraumereNumber.color = square.GetResources ().deraumere.color;
        siburNumber.text = square.GetResources ().sibur.count.ToString ();
        siburNumber.color = square.GetResources ().sibur.color;
        mendianeNumber.text = square.GetResources ().mendiane.count.ToString ();
        mendianeNumber.color = square.GetResources ().mendiane.color;
        phirasNumber.text = square.GetResources ().phiras.count.ToString ();
        phirasNumber.color = square.GetResources ().phiras.color;
        thystameNumber.text = square.GetResources ().thystame.count.ToString ();
        thystameNumber.color = square.GetResources ().thystame.color;
        nourritureNumber.text = square.GetResources ().nourriture.count.ToString ();
        nourritureNumber.color = square.GetResources ().nourriture.color;
        playersNumber.text = square.GetResources ().players.Count.ToString ();
    }

    void DisplayWindow (ClickEventArgs args)
    {
        this.square = args.square;
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

    void Update () {
        if (square != null)
            DisplayResources(square);
    }
}
