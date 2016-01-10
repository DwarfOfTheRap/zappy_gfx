using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeamButton : MonoBehaviour {

	public GameObject		teamDetails;

	void Start ()
	{
		teamDetails = GameObject.FindGameObjectWithTag("TeamCompositionWindow");
	}

	public void ActivateTeamDetails ()
	{
		Text[]	texts = gameObject.GetComponentsInChildren<Text> ();

		teamDetails.GetComponent<CanvasGroup> ().alpha = 1;
		//teamDetails.GetComponent<TeamDetails> ().team = texts[1].gameObject.GetComponent<TeamCompletion> ().team;
	}
}
