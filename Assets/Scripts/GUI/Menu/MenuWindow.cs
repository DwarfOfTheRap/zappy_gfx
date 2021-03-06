﻿using UnityEngine;
using System.Collections;

public class MenuWindow : AWindow {

	protected override void CheckKeyboardShortcut()
	{
		if (inputManager.MenuKey())
		{
			gameObject.GetComponent<CanvasGroup>().alpha = (gameObject.GetComponent<CanvasGroup>().alpha > 0) ? 0 : 1;
			gameObject.GetComponent<CanvasGroup>().blocksRaycasts ^= true;
		}
	}
	
	public void ExitGame()
	{
		SocketManager.instance.ResetLevel();
	}
}
