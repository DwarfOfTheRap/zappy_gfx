using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerList: MonoBehaviour {

	public GameObject		prefab;
	public CanvasGroup		window;
	
	void CheckInput()
	{
		if (Input.GetMouseButtonUp (1)) {
			Transform[] children = gameObject.GetComponentsInChildren<Transform>();

			foreach(Transform trans in children)
			{
				if (trans != this.transform)
					Destroy(trans.gameObject);
			}
			window.alpha = 0;
		}
	}

	public void DisplayDetails(Team team)
	{
		PlayerController[] players = GameManagerScript.instance.playerManager.GetPlayersInTeam(team);

		if (players != null)
		{
			foreach(PlayerController player in players)
			{
				GameObject aPlayer = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;

				aPlayer.transform.SetParent(this.gameObject.transform);
				aPlayer.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
				aPlayer.name = "Player " + player.index.ToString();

				APlayer script = aPlayer.gameObject.GetComponent<APlayer> ();

				script.playerIndex.text = "Player " + player.index.ToString();
				script.playerLvl.text = "Lvl " + player.level.ToString();
				/* TO REPLACE BY RELEVENT VALUES */
				script.playerInventory.gameObject.GetComponent<PlayerInventory>().linemate.text = "1";
				script.playerInventory.gameObject.GetComponent<PlayerInventory>().deraumere.text = "2";
				script.playerInventory.gameObject.GetComponent<PlayerInventory>().sibur.text = "3";
				script.playerInventory.gameObject.GetComponent<PlayerInventory>().mendiane.text = "4";
				script.playerInventory.gameObject.GetComponent<PlayerInventory>().phiras.text = "5";
				script.playerInventory.gameObject.GetComponent<PlayerInventory>().thystame.text = "6";
				/* TO REPLACE BY RELEVENT VALUES */
			}
		}
	}

	void Update ()
	{
		CheckInput();
	}
}
