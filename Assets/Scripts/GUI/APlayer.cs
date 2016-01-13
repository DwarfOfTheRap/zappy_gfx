using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class APlayer : MonoBehaviour {

	public Text 				playerNumber;
	public Text 				playerLvl;
	public Text 				linemate;
	public Text 				deraumere;
	public Text 				sibur;
	public Text 				mendiane;
	public Text 				phiras;
	public Text 				thystame;

	void CheckInput()
	{
		if (Input.GetMouseButtonUp (1)) {
			GetComponent<CanvasGroup> ().alpha = 0;
		}
	}

	void Start ()
	{
		linemate = GameObject.Find("linemate").gameObject.GetComponent<Text>();
		deraumere = GameObject.Find("deraumere").gameObject.GetComponent<Text>();
		sibur = GameObject.Find("sibur").gameObject.GetComponent<Text>();
		mendiane = GameObject.Find("mendiane").gameObject.GetComponent<Text>();
		phiras = GameObject.Find("phiras").gameObject.GetComponent<Text>();
		thystame = GameObject.Find("thystame").gameObject.GetComponent<Text>();
	}

	void Update ()
	{
		CheckInput();
	}

}
