using UnityEngine;
using System.Collections;

public class MenuWindow : MonoBehaviour {

	private AInputManager inputManager;

	void CheckMenuKey()
	{
		if (inputManager.MenuKey())
		{
			gameObject.GetComponent<CanvasGroup>().alpha = (gameObject.GetComponent<CanvasGroup>().alpha > 0) ? 0 : 1;
			gameObject.GetComponent<CanvasGroup>().blocksRaycasts ^= true;
		}
	}

	public void DisplayWindow()
	{
		gameObject.GetComponent<CanvasGroup>().alpha = 1;
		gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
	}

	public void CloseWindow()
	{
		gameObject.GetComponent<CanvasGroup>().alpha = 0;
		gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	void Start ()
	{
		inputManager = GameManagerScript.instance.inputManager;
	}

	void Update () {
		CheckMenuKey();
	}
}
