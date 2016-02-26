using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {

	void DisplayWindow (GameOverEventArgs ev)
	{
		string hexTeamColor;

		hexTeamColor = "#" + ((int)(ev.team.color.r * 255)).ToString("X") + ((int)(ev.team.color.g * 255)).ToString("X") + ((int)(ev.team.color.b * 255)).ToString("X") + "ff";
		gameObject.GetComponent<CanvasGroup> ().alpha = 1;
		gameObject.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		gameObject.GetComponentInChildren<Text> ().text = "Game Over\nTeam " + "<color=" + hexTeamColor + ">" + ev.team.name + "</color>" + "\nWins!";
	}

	void Start ()
	{
		GameManagerScript.instance.OnGameOver += DisplayWindow;
	}
}
