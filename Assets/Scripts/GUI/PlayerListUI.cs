using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerListUI : MonoBehaviour {

	public GameObject		prefab;

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

				/* TO REPLACE BY RELEVENT VALUES */
				script.inventory.food.text = "0";
				script.inventory.linemate.text = "1";
				script.inventory.deraumere.text = "2";
				script.inventory.sibur.text = "3";
				script.inventory.mendiane.text = "4";
				script.inventory.phiras.text = "5";
				script.inventory.thystame.text = "6";
				/* TO REPLACE BY RELEVENT VALUES */
			}
		}
	}

	void DeletePlayers ()
	{
		Transform[]		pLChildren = this.GetComponentsInChildren<Transform>();

		foreach(Transform trans in pLChildren)
		{
			if (trans != this.transform)
				Destroy(trans.gameObject);
		}
	}

	void Start () {
		InputManager.OnRightClick += DeletePlayers;
	}
}
