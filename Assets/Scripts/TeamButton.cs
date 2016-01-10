using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeamButton : MonoBehaviour {

	public GameObject		teamDetails;

	public void ActivateTeamDetails ()
	{
		GameObject[]	objs = gameObject.GetComponentsInChildren<Text> ();

		teamDetails.GetComponent<CanvasGroup> ().alpha = 1;
		teamDetails.GetComponent<TeamDetails> ().team = objs[1].gameObject.GetComponent<TeamCompletion> ().team;
	}
}
