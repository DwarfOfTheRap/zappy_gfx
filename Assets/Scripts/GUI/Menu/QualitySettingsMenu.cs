using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QualitySettingsMenu : AWindow {

	public GameObject[] buttons;
	public Sprite defaultButton;
	public Sprite activatedButton;
	
	protected override void CheckKeyboardShortcut()
	{
		if (inputManager.MenuKey())
		{
			CloseWindow();
		}
	}

	public void ChangeQuality(int quality)
	{
		int i = 0;

		foreach(GameObject button in buttons)
		{
			if (quality == i)
				button.GetComponent<Image>().sprite = activatedButton;
			else
				button.GetComponent<Image>().sprite = defaultButton;
			i++;
		}
		GameManagerScript.instance.qualityManager.ChangeQuality(quality);
	}

	protected override void Start ()
	{
		base.Start ();
		buttons[QualityManager.GetQualityLevel()].GetComponent<Image>().sprite = activatedButton;
	}
}
