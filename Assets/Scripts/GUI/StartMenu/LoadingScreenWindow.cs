using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingScreenWindow : MonoBehaviour {
	public Text ConnectionText;
	public Text ProgressText;
	void Start () {
		StartCoroutine (WaitForConnection());
	}

	IEnumerator WaitForConnection()
	{
		while (true)
		{
			ConnectionText.text = "Loading";
			yield return new WaitForSeconds(0.5f);
			ConnectionText.text = "Loading.";
			yield return new WaitForSeconds(0.5f);
			ConnectionText.text = "Loading..";
			yield return new WaitForSeconds(0.5f);
			ConnectionText.text = "Loading...";
			yield return new WaitForSeconds(0.5f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		ProgressText.text = (GameManagerScript.instance.gridController.GetInitProgress() * 100.0f).ToString ("0.0") + "<size=10>%</size>";
	}
}
