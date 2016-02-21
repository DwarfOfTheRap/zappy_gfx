using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour {
	
	public PlayerController player = null;
	public Text linemateNumber;
	public Text deraumereNumber;
	public Text siburNumber;
	public Text mendianeNumber;
	public Text phirasNumber;
	public Text thystameNumber;
	public Text nourritureNumber;
	public Text levelNumber;
	public Text teamText;
	
	public void DisplayResources (PlayerController player)
	{
		linemateNumber.text = player.inventory.linemate.ToString ();
		linemateNumber.color = ResourceController.linemateColor;
		deraumereNumber.text = player.inventory.deraumere.ToString ();
		deraumereNumber.color = ResourceController.deraumereColor;
		siburNumber.text = player.inventory.sibur.ToString ();
		siburNumber.color = ResourceController.siburColor;
		mendianeNumber.text = player.inventory.mendiane.ToString ();
		mendianeNumber.color = ResourceController.mendianeColor;
		phirasNumber.text = player.inventory.phiras.ToString ();
		phirasNumber.color = ResourceController.phirasColor;
		thystameNumber.text = player.inventory.thystame.ToString ();
		thystameNumber.color = ResourceController.thystameColor;
		nourritureNumber.text = player.inventory.nourriture.ToString ();
		nourritureNumber.color = ResourceController.foodColor;
		levelNumber.text = player.level.ToString ();
		teamText.text = player.team.name;
		teamText.color = player.team.color;
		teamText.GetComponent<Outline>().effectColor = new Color(player.team.color.r, player.team.color.g, player.team.color.b, teamText.GetComponent<Outline>().effectColor.a);
	}
	
	void DisplayWindow (ClickEventArgs args)
	{
		if (!args.target.IsPlayer())
		{
			HideWindow ();
			return ;
		}
		this.player = ((PlayerScript)args.target).controller;
		this.GetComponent<CanvasGroup> ().alpha = 1;
		this.GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}
	
	void HideWindow ()
	{
		this.player = null;
		this.GetComponent<CanvasGroup> ().alpha = 0;
		this.GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}
	
	void Start () {
		this.player = null;
		GameManagerScript.instance.inputManager.OnLeftClicking += DisplayWindow;
		GameManagerScript.instance.inputManager.OnRightClicking += HideWindow;
	}
	
	void Update () {
		if (this.player != null)
			DisplayResources(player);
	}
}
