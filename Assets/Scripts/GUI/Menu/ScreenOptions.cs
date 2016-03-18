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
		if (Screen.width != _resolutions[value].width || Screen.height != _resolutions[value].height)
		{
			Screen.SetResolution(_resolutions[value].width, _resolutions[value].height, Screen.fullScreen);
			PlayerPrefs.SetInt("Resolution", _resolutions[value].width);
		}
	}

	public void OnQualityChangeEvent (int value)
	{
		GameManagerScript.instance.qualityManager.ChangeQuality(value);
		PlayerPrefs.SetInt("Quality", value);
	}

	public void OnFullScreenToggle (bool value)
	{
		Screen.fullScreen = value;
		PlayerPrefs.SetInt ("FullScreen", value ? 1 : 0);
	}

	protected void Awake ()
	{
		int i = 0;
		int resolutionWidth;
		_resolutions = Screen.resolutions;
		_toggle = gameObject.GetComponentInChildren<Toggle>();
		_qualityDropdown = GameObject.Find("QualitySettingsDropdown").GetComponent<Dropdown>();
		_resolutionDropdown = GameObject.Find("ResolutionsDropdown").GetComponent<Dropdown>();

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

		if (PlayerPrefs.HasKey("Resolution"))
			resolutionWidth = PlayerPrefs.GetInt("Resolution");
		else
			resolutionWidth = Screen.width;
		while (_resolutions != null && i < _resolutions.Length)
		{
			if (_resolutions[i].width == resolutionWidth)
				break ;
			i++;
		}
		_resolutionDropdown.value = i;
		_resolutionDropdown.captionText.text = _resolutions[i].width.ToString() + " x " + _resolutions[i].height.ToString();
	}

}
