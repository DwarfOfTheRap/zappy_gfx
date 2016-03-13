using UnityEngine;
using System.Collections;

public class DebugWindow : AWindow {
	
	protected override void CheckKeyboardShortcut ()
	{
		if (inputManager.DebugKey())
		{
			gameObject.GetComponent<CanvasGroup>().alpha = (gameObject.GetComponent<CanvasGroup>().alpha > 0) ? 0 : 1;
			gameObject.GetComponent<CanvasGroup>().blocksRaycasts ^= true;
		}
	}

	void Update () {
		CheckKeyboardShortcut();
	}
}
