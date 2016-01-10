using UnityEngine;
using System.Collections;

public class TeamButton : MonoBehaviour {

	public GameObject		teamDetails;

	public void ActivateTeamDetails ()
	{
		teamDetails.GetComponent<CanvasGroup> ().alpha = 1;
	}
}
