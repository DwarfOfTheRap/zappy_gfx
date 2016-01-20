using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerListUI : MonoBehaviour {

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

				PlayerUI script = aPlayer.gameObject.GetComponent<PlayerUI> ();

				script.playerIndex.text = "Player " + player.index.ToString();
				script.playerLvl.text = "Lvl " + player.level.ToString();
				script.inventory.linemate.text = player.inventory.linemate.ToString();
				script.inventory.deraumere.text = player.inventory.deraumere.ToString ();
				script.inventory.sibur.text = player.inventory.sibur.ToString ();
				script.inventory.mendiane.text = player.inventory.mendiane.ToString ();
				script.inventory.phiras.text = player.inventory.phiras.ToString ();
				script.inventory.thystame.text = player.inventory.thystame.ToString ();
				/* TO REPLACE BY RELEVENT VALUES */
			}
		}
	}

	void Update ()
	{
		CheckInput();
	}
}
