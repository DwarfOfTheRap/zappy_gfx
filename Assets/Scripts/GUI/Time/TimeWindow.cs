using UnityEngine;
using System.Collections;

public class TimeWindow : MonoBehaviour {
	void DeactivateWindow (GameOverEventArgs ev)
	{
		gameObject.GetComponent<CanvasGroup> ().interactable = false;
	}

	void Start () {
		GameManagerScript.instance.OnGameOver += DeactivateWindow;
	}
}
