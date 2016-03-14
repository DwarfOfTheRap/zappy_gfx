using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class DebugTextArea : MonoBehaviour  {

	private RectTransform 	_debugObject;
	private Text			_textObject;
	private PlayerController	_player = null;
		
	void Start ()
	{
		_debugObject = gameObject.GetComponent<RectTransform>();
		_textObject = gameObject.GetComponent<Text>();
		_textObject.text = GameManagerScript.instance.debugManager.general_log;
		GameManagerScript.instance.inputManager.OnLeftClicking += OnLeftClick;
		GameManagerScript.instance.inputManager.OnRightClicking += OnRightClick;
	}

	void OnDisable()
	{
		GameManagerScript.instance.inputManager.OnLeftClicking -= OnLeftClick;
		GameManagerScript.instance.inputManager.OnRightClicking -= OnRightClick;
	}

	void OnLeftClick (ClickEventArgs ev)
	{
		_player = ev.target.IsPlayer () ? ((PlayerScript)ev.target).controller : null;
	}

	void OnRightClick ()
	{
		_player = null;
	}

	void Update ()
	{
		_debugObject.sizeDelta = new Vector2(_debugObject.rect.width, _textObject.preferredHeight);
		if (_debugObject.rect.height > 360)
		{
			_debugObject.pivot = new Vector2(1.0f, 0.0f);
			_debugObject.anchorMin = new Vector2(1.0f, 0.0f);
			_debugObject.anchorMax = new Vector2(1.0f, 0.0f);
		}
		else
		{
			_debugObject.pivot = new Vector2(0.0f, 1.0f);
			_debugObject.anchorMin = new Vector2(0.0f, 1.0f);
			_debugObject.anchorMax = new Vector2(0.0f, 1.0f);
		}
		if (_player != null && GameManagerScript.instance.debugManager.players_log.ContainsKey (_player))
			_textObject.text = GameManagerScript.instance.debugManager.players_log[_player];
		else
			_textObject.text = GameManagerScript.instance.debugManager.general_log;
	}


}
