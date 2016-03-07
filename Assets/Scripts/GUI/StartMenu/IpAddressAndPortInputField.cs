using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class IpAddressAndPortInputField : MonoBehaviour {

	public Text errorText;

	void Awake ()
	{
		this.gameObject.GetComponent<InputField>().onEndEdit.AddListener(ConnectToPortOnAddress);
	}

	IEnumerator DisplayError ()
	{
		errorText.color = new Color (errorText.color.r, errorText.color.g, errorText.color.b, 1.0f);
		yield return new WaitForSeconds(1.0f);
		while (errorText.color.a > 0.0f)
		{
			yield return new WaitForEndOfFrame();
			errorText.color = new Color(errorText.color.r, errorText.color.g, errorText.color.b, errorText.color.a - 0.01f);
		}
		this.gameObject.GetComponent<InputField>().OnPointerClick(new PointerEventData(EventSystem.current));
		EventSystem.current.SetSelectedGameObject(this.gameObject, new BaseEventData(EventSystem.current));
	}

    public void ConnectToPortOnAddress(string arg0)
    {
		string regex = @"((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?):(6553[0-5]|655[0-2][0-9]|65[0-4][0-9][0-9]|6[0-4][0-9][0-9][0-9]|[1-5][0-9][0-9][0-9][0-9]|[2-9][0-9][0-9][0-9]|1[1-9][0-9][0-9]|10[3-9][0-9]|102[5-9])";
		string[] split;

		Debug.Log(arg0);
		if (Regex.IsMatch(arg0, regex))
		{
			split = arg0.Split(':');
			SocketManager.instance.SetupConnection(split[0], int.Parse(split[1]));
		}
		else
			StartCoroutine(DisplayError());
    }
}
