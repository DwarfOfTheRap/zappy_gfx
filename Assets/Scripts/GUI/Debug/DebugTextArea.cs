using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugTextArea : MonoBehaviour  {

	private RectTransform 	_debugObject;
	private Text			_text;

	public void DisplayNewDebug (string debug)
	{
		_text.text = _text.text + debug + "\n";
	}


	void Start ()
	{
		_debugObject = gameObject.GetComponent<RectTransform>();
		_text = gameObject.GetComponent<Text>();
	}

	void Update ()
	{
		_debugObject.sizeDelta = new Vector2(_debugObject.rect.width, _text.preferredHeight);
		if (_debugObject.rect.height > 360)
		{
			_debugObject.pivot = new Vector2(1.0f, 0.0f);
			_debugObject.anchorMin = new Vector2(1.0f, 0.0f);
			_debugObject.anchorMax = new Vector2(1.0f, 0.0f);
		}
		if (_debugObject.rect.height <= 360)
		{
			_debugObject.pivot = new Vector2(0.0f, 1.0f);
			_debugObject.anchorMin = new Vector2(0.0f, 1.0f);
			_debugObject.anchorMax = new Vector2(0.0f, 1.0f);
		}
	}
}
