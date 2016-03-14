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

				script.player = player;
				script.playerIndex.text = "Player " + player.index.ToString();
				script.playerLvl.text = "Lvl " + player.level.ToString();
				script.inventory.linemate.text = player.inventory.linemate.ToString();
				script.inventory.deraumere.text = player.inventory.deraumere.ToString ();
				script.inventory.sibur.text = player.inventory.sibur.ToString ();
				script.inventory.mendiane.text = player.inventory.mendiane.ToString ();
				script.inventory.phiras.text = player.inventory.phiras.ToString ();
				script.inventory.thystame.text = player.inventory.thystame.ToString ();
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
		GameManagerScript.instance.inputManager.OnRightClicking += DeletePlayers;
	}

	void OnDisable ()
	{
		GameManagerScript.instance.inputManager.OnRightClicking -= DeletePlayers;
	}
}
