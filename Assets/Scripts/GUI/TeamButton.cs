using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeamButton : MonoBehaviour {

	public GameObject		teamComposition = null;
	public Team				team;
	public Text				teamName;
	public Text				teamCompletion;

	void Start ()
	{
		teamComposition = GameObject.FindGameObjectWithTag("TeamCompositionWindow");
	}

	public void ActivateTeamDetails ()
	{
		teamComposition.GetComponent<CanvasGroup> ().alpha = 1;
		teamComposition.GetComponent<PlayerList> ().DisplayDetails(team);
	}
}
