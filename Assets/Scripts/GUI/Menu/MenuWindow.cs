using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuWindow : MonoBehaviour {

	private AInputManager inputManager;

	void CheckMenuKey()
	{
		if (inputManager.OpenMenu())
		{
			gameObject.GetComponent<CanvasGroup>().alpha = (gameObject.GetComponent<CanvasGroup>().alpha > 0) ? 0 : 1;
			gameObject.GetComponent<CanvasGroup>().blocksRaycasts ^= true;
		}
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
