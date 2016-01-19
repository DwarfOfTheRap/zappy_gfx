using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUI : MonoBehaviour {

	public Text 				playerIndex;
	public Text 				playerLvl;
	public PlayerInventoryUI	inventory;

	[System.Serializable]
	public class PlayerInventoryUI
	{
		public Text 				linemate;
		public Text 				deraumere;
		public Text 				sibur;
		public Text 				mendiane;
		public Text 				phiras;
		public Text 				thystame;
	}

	void Awake()
	{
		inventory.linemate = transform.FindChild ("PlayerInventory/Linemate").GetComponent<Text>();
		inventory.deraumere = transform.FindChild ("PlayerInventory/Deraumere").GetComponent<Text>();
		inventory.sibur = transform.FindChild ("PlayerInventory/Sibur").GetComponent<Text>();
		inventory.mendiane = transform.FindChild ("PlayerInventory/Mendiane").GetComponent<Text>();
		inventory.phiras = transform.FindChild ("PlayerInventory/Phiras").GetComponent<Text>();
		inventory.thystame = transform.FindChild ("PlayerInventory/Thystame").GetComponent<Text>();
	}
}