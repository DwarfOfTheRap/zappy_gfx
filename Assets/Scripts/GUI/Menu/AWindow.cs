using UnityEngine;
using System.Collections;

public abstract class AWindow : MonoBehaviour {

	protected AInputManager		inputManager;
	protected abstract void		CheckKeyboardShortcut();

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

	protected virtual void Start () {
		inputManager = GameManagerScript.instance.inputManager;
	}
	
	void Update () {
		CheckKeyboardShortcut();
	}
}
