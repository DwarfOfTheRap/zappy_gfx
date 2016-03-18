using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {

	void DisplayWindow (GameOverEventArgs ev)
	{
		var colorhex = string.Format("#{0}{1}{2}", ((int)(ev.team.color.r * 255.0f)).ToString("X2"), ((int)(ev.team.color.g * 255)).ToString("X2"), ((int)(ev.team.color.b * 255)).ToString("X2"));
		gameObject.GetComponent<CanvasGroup> ().alpha = 1;
		gameObject.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		gameObject.GetComponentInChildren<Outline>().effectColor = Color.clear;
		gameObject.GetComponentInChildren<Text> ().text = "Game Over\nTeam " + "<color=" + colorhex + ">" + ev.team.name + "</color>" + "\nWins!";
	}

	void Start ()
	{
		GameManagerScript.instance.OnGameOver += DisplayWindow;
	}

	void OnDisable ()
	{
		GameManagerScript.instance.OnGameOver -= DisplayWindow;
	}
}
