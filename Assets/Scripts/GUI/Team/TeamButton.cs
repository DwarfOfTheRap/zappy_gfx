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
	}

	public void ActivateTeamDetails ()
	{
		PlayerListUI playerList = teamComposition.GetComponentInChildren<PlayerListUI> ();
		Transform[] children = playerList.gameObject.GetComponentsInChildren<Transform>();
		
		foreach(Transform trans in children)
		{
			if (trans != playerList.transform)
				Destroy(trans.gameObject);
		}
		playerList.DisplayDetails(team);
		teamComposition.GetComponent<CanvasGroup> ().alpha = 1;
		teamComposition.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		foreach (var image in teamComposition.GetComponentsInChildren<Image>())
		{
			image.color = new Color(team.color.r, team.color.g, team.color.b, image.color.a);
		}
	}
}
