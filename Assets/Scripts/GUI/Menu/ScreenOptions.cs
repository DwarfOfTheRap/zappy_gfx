using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScreenOptions : AWindow {

	private Toggle			_toggle;
	private Dropdown		_dropdown;
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

	protected override void Start ()
	{
		_toggle = gameObject.GetComponentInChildren<Toggle>();
		_dropdown = gameObject.GetComponentInChildren<Dropdown>();

		base.Start();
		_toggle.isOn = Screen.fullScreen;
		_dropdown.interactable = !_toggle.isOn;
		_dropdown.captionText.text = Screen.currentResolution.ToString().Substring(0, 11);
		for (int i = 0; i < _resolutions.Length; ++i)
		{

		}
	}

}
