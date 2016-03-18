using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeamCompositionUI : MonoBehaviour {
	void HideWindow ()
	{
		this.GetComponent<CanvasGroup> ().alpha = 0;
		this.GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	void Start ()
	{
		GameManagerScript.instance.inputManager.OnRightClicking += HideWindow;
	}

	void OnDisable() 
	{
		GameManagerScript.instance.inputManager.OnRightClicking -= HideWindow;
	}
}
