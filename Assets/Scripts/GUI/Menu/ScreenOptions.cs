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
		PlayerPrefs.SetInt("Resolution", _resolutions[value].width);
	}

	public void OnQualityChangeEvent (int value)
	{
		GameManagerScript.instance.qualityManager.ChangeQuality(value);
		PlayerPrefs.SetInt("Quality", value);
	}

	public void OnFullScreenToggle (bool value)
	{
		Screen.fullScreen = value;
		_resolutionDropdown.interactable = !value;
		if (value)
			PlayerPrefs.SetInt ("FullScreen", 1);
		else
			PlayerPrefs.SetInt ("FullScreen", 0);
	}

	protected override void Start ()
	{
		int i = 0;
		int resolutionWidth;
		_toggle = gameObject.GetComponentInChildren<Toggle>();
		_qualityDropdown = GameObject.Find("QualitySettingsDropdown").GetComponent<Dropdown>();
		_resolutionDropdown = GameObject.Find("ResolutionsDropdown").GetComponent<Dropdown>();

		base.Start();

		if (PlayerPrefs.HasKey("FullScreen"))
			_toggle.isOn = PlayerPrefs.GetInt("FullScreen") > 0 ? true : false;
		else
			_toggle.isOn = Screen.fullScreen;

		if (PlayerPrefs.HasKey("Quality"))
		{
			_qualityDropdown.value = PlayerPrefs.GetInt("Quality");
			_qualityDropdown.captionText.text = _qualities[PlayerPrefs.GetInt("Quality")];
		}
		else
		{
			_qualityDropdown.value = QualityManager.GetQualityLevel();
			_qualityDropdown.captionText.text = _qualities[QualityManager.GetQualityLevel()];
		}

		_resolutionDropdown.interactable = !_toggle.isOn;
		if (PlayerPrefs.HasKey("Resolution"))
			resolutionWidth = PlayerPrefs.GetInt("Resolution");
		else
			resolutionWidth = Screen.width;
		while (i < _resolutions.Length)
		{
			if (_resolutions[i].width == resolutionWidth)
				break ;
			i++;
		}
		_resolutionDropdown.value = i;
		_resolutionDropdown.captionText.text = _resolutions[i].width.ToString() + " x " + _resolutions[i].height.ToString();
	}

}
