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
		this.gameObject.GetComponent<InputField>().onEndEdit.AddListener (ConnectToPortOnAddress);
		this.gameObject.GetComponent<InputField>().caretPosition = 0;
		if (PlayerPrefs.HasKey("IpAddress"))
			GetComponent<InputField>().text = PlayerPrefs.GetString ("IpAddress");
		SocketManager.instance.OnConnectionCanceled += ConnectionCanceled;
	}

	void OnDisable()
	{
		SocketManager.instance.OnConnectionCanceled -= ConnectionCanceled;
		ConnectionCanceled ();
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

	void ConnectionCanceled()
	{
		StopAllCoroutines();
		quitButton.interactable = true;
		GetComponent<InputField>().interactable = true;
		connectingToServerText.text = "";
		errorText.text = "";
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
				StartCoroutine ("WaitForConnection", WaitForConnection());
				PlayerPrefs.SetString ("IpAddress", submit);
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

	void Update()
	{
		if (!SocketManager.instance.connected)
		{
			StopCoroutine ("WaitForConnection");
			quitButton.interactable = true;
			GetComponent<InputField>().interactable = true;
		}
	}
}
