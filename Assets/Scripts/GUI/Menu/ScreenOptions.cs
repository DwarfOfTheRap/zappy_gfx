using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScreenOptions : AWindow {

	private Toggle			_toggle;
	private Dropdown		_qualityDropdown;
	private string[]		_qualities = {"Fastest", "Fast", "Simple", "Good", "Beautiful", "Outrageous"};
	private Dropdown		_resolutionDropdown;
	private Resolution[] 	_resolutions = Screen.resolutions;

	protected override void CheckKeyboardShortcut ()
	{
		if (inputManager.MenuKey())
		{
			CloseWindow();
		}
	}

	public void OnScreenResolutionChangeEvent (int value)
	{
		Screen.SetResolution(_resolutions[value].width, _resolutions[value].height, false);
	}

	public void OnQualityChangeEvent (int value)
	{
		GameManagerScript.instance.qualityManager.ChangeQuality(value);
	}

	public void OnFullScreenToggle (bool value)
	{
		Screen.fullScreen = value;
		_resolutionDropdown.interactable = !value;
	}

	protected override void Start ()
	{
		int i = 0;
		_toggle = gameObject.GetComponentInChildren<Toggle>();
		_qualityDropdown = GameObject.Find("QualitySettingsDropdown").GetComponent<Dropdown>();
		_resolutionDropdown = GameObject.Find("ResolutionsDropdown").GetComponent<Dropdown>();

		base.Start();
		_toggle.isOn = Screen.fullScreen;
		_qualityDropdown.captionText.text = _qualities[QualityManager.GetQualityLevel()];
		_qualityDropdown.value = QualityManager.GetQualityLevel();
		_resolutionDropdown.interactable = !_toggle.isOn;
		_resolutionDropdown.captionText.text = Screen.width.ToString() + " x " + Screen.height.ToString();
		while (i < _resolutions.Length)
		{
			if (_resolutions[i].width == Screen.width)
				break ;
			i++;
		}
		_resolutionDropdown.value = i;
	}

}
