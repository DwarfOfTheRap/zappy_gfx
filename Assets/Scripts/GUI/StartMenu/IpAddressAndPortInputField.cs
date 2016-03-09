using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class IpAddressAndPortInputField : MonoBehaviour {

	public Text errorText;
	public Text connectingToServerText;
	public Button quitButton;

	void Awake ()
	{
		this.gameObject.GetComponent<InputField>().onEndEdit.AddListener(ConnectToPortOnAddress);
	}

	void OnDisable()
	{
		StopAllCoroutines ();
		errorText.text = "";
		connectingToServerText.text = "";
	}

	IEnumerator DisplayError (string error)
	{
		errorText.text = error;
		errorText.color = new Color (errorText.color.r, errorText.color.g, errorText.color.b, 1.0f);
		yield return new WaitForSeconds(1.0f);
		while (errorText.color.a > 0.0f)
		{
			yield return null;
			errorText.color = new Color(errorText.color.r, errorText.color.g, errorText.color.b, errorText.color.a - 0.01f);
		}
		this.gameObject.GetComponent<InputField>().OnPointerClick(new PointerEventData(EventSystem.current));
		EventSystem.current.SetSelectedGameObject(this.gameObject, new BaseEventData(EventSystem.current));
	}

	IEnumerator WaitForConnection ()
	{
		while (true)
		{
			connectingToServerText.text = "Connecting to server";
			yield return new WaitForSeconds(0.5f);
			connectingToServerText.text = "Connecting to server.";
			yield return new WaitForSeconds(0.5f);
			connectingToServerText.text = "Connecting to server..";
			yield return new WaitForSeconds(0.5f);
			connectingToServerText.text = "Connecting to server...";
			yield return new WaitForSeconds(0.5f);
		}
	}

    public void ConnectToPortOnAddress(string submit)
    {
		string regex = @"(([^:]+):(6553[0-5]|655[0-2][0-9]|65[0-4][0-9][0-9]|6[0-4][0-9][0-9][0-9]|[1-5][0-9][0-9][0-9][0-9]|[2-9][0-9][0-9][0-9]|1[1-9][0-9][0-9]|10[3-9][0-9]|102[5-9]))";
		string[] split;

		if (Regex.IsMatch(submit, regex))
		{
			split = submit.Split(':');
			try
			{
				SocketManager.instance.SetupConnection(split[0], int.Parse(split[1]));
				quitButton.interactable = false;
				GetComponent<InputField>().interactable = false;
				StartCoroutine (WaitForConnection());
			}
			catch (System.Exception e)
			{
				Debug.Log (e.Message);
				StartCoroutine(DisplayError(e.Message));
			}
		}
		else
			StartCoroutine(DisplayError("Wrong Parameters"));
    }
}
