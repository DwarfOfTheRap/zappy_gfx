using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeamButton : MonoBehaviour {

	public GameObject		teamDetails;
	public Team				team;
	public Text				teamName;
	public Text				teamCompletion;

	void Start ()
	{
		teamDetails = GameObject.FindGameObjectWithTag("TeamCompositionWindow");
	}

	public void ActivateTeamDetails ()
	{
		teamDetails.GetComponent<CanvasGroup> ().alpha = 1;
		teamDetails.GetComponent<PlayerList> ().DisplayDetails(team);
	}
}
