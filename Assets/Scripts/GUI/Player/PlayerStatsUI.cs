using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour {
	
	private PlayerController _player = null;
	public Text title;
	public Text linemateNumber;
	public Text deraumereNumber;
	public Text siburNumber;
	public Text mendianeNumber;
	public Text phirasNumber;
	public Text thystameNumber;
	public Text nourritureNumber;
	public Text levelNumber;
	public Text teamText;
	private Button _refreshButton;

	public void RefreshPlayer()
	{
		if (this._player != null)
		{
			var query = new ServerQuery();
			query.SendPlayerInventoryQuery(this._player.index);
			query.SendPlayerLevelQuery (this._player.index);
		}
	}

	void AnimateRefresh()
	{
		_refreshButton.animator.SetTrigger ("Pressed");
	}
	
	void DisplayResources (PlayerController player)
	{
		title.text = "Player " + player.index + " Stats";
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
		teamText.GetComponent<Outline>().effectColor = new Color(player.team.color.r / 2, player.team.color.g / 2, player.team.color.b / 2, 0.2f);
		foreach (var image in GetComponentsInChildren<Image>())
		{
			image.color = new Color(player.team.color.r, player.team.color.g, player.team.color.b, image.color.a);
		}
	}
	
	void DisplayWindow (ClickEventArgs args)
	{
		if (!args.target.IsPlayer())
		{
			HideWindow ();
			return ;
		}
		this._player = ((PlayerScript)args.target).controller;
		_player.OnRefresh += AnimateRefresh;
		this.GetComponent<CanvasGroup> ().alpha = 1;
		this.GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}
	
	void HideWindow ()
	{
		if (_player != null)
			_player.OnRefresh -= AnimateRefresh;
		this._player = null;
		this.GetComponent<CanvasGroup> ().alpha = 0;
		this.GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}
	
	void Start () {
		_refreshButton = GetComponentInChildren<Button>();
		this._player = null;
		GameManagerScript.instance.inputManager.OnLeftClicking += DisplayWindow;
		GameManagerScript.instance.inputManager.OnRightClicking += HideWindow;
	}

	void OnDisable ()
	{
		GameManagerScript.instance.inputManager.OnLeftClicking -= DisplayWindow;
		GameManagerScript.instance.inputManager.OnRightClicking -= HideWindow;
	}
	
	void Update () {
		if (this._player != null)
			DisplayResources(_player);
	}
}
