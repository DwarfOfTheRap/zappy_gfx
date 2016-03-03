using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QualitySettingsMenu : MonoBehaviour {

	private AInputManager inputManager;
	public GameObject[] buttons;
	public Sprite defaultButton;
	public Sprite activatedButton;
	
	void CheckMenuKey()
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

	void Start ()
	{
		buttons[QualityManager.GetQualityLevel()].GetComponent<Image>().sprite = activatedButton;
		inputManager = GameManagerScript.instance.inputManager;
	}
	
	void Update ()
	{
		CheckMenuKey();
	}
}
