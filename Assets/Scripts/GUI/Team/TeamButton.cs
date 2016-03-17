using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeamButton : MonoBehaviour {

	public GameObject		teamComposition { get; private set; }
	public Team				team;
	public Text				teamName;
	public Text				teamCompletion;

	void Start ()
	{
		teamComposition = GameObject.FindGameObjectWithTag("TeamCompositionWindow");
		GameManagerScript.instance.playerManager.OnNewPlayer += RefreshScrollableArea;
		GameManagerScript.instance.playerManager.OnAPlayerDying += RefreshScrollableArea;
	}

	void RefreshScrollableArea (OnAPlayerEventArgs ev)
	{
		if (ev.player.team == this.team)
			ActivateTeamDetails();
	}

	public void ActivateTeamDetails ()
	{
		PlayerListUI playerList = teamComposition.GetComponentInChildren<PlayerListUI> ();
		PlayerUI[] children = playerList.gameObject.GetComponentsInChildren<PlayerUI>();
		
		foreach(PlayerUI pUI in children)
			DestroyImmediate(pUI.gameObject);
		playerList.DisplayDetails(team);
		teamComposition.GetComponent<CanvasGroup> ().alpha = 1;
		teamComposition.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		foreach (var image in teamComposition.GetComponentsInChildren<Image>(true))
		{
			if (image.name == "BackgroundImage")
				image.color = new Color(team.color.r, team.color.g, team.color.b, image.color.a);
			else if (GameManagerScript.instance.teamManager.triad.ContainsKey (team.color))
			{
				var color = GameManagerScript.instance.teamManager.triad[team.color][0];
				image.color = new Color(color.r, color.g, color.b, image.color.a);
			}
			else
				image.color = new Color(1 - team.color.r, 1 - team.color.g, 1 - team.color.b, image.color.a);
		}
	}
}
