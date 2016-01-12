using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerList: MonoBehaviour {

	public GameObject		prefab;

	public void DisplayDetails(Team team)
	{
		PlayerController[] players = GameManagerScript.instance.playerManager.GetPlayersInTeam(team);

		foreach(PlayerController player in players)
		{
			GameObject aPlayer = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;

			aPlayer.transform.SetParent(this.gameObject.transform);

			APlayer script = aPlayer.gameObject.GetComponent<APlayer> ();

			script.playerNumber.text = player.index.ToString();
			script.playerLvl.text = player.level.ToString();
			//script.linemate.text = player.
			//script.deraumere.text = player.
			//script.sibur.text = player.
			//script.mendiane.text = player.
			//script.phiras.text = player.
			//script.thystame.text = player.
		}
	}
}
