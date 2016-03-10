using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUI : MonoBehaviour {

	public Text 				playerIndex;
	public Text 				playerLvl;
	public PlayerInventoryUI	inventory { get; private set;}

	[System.Serializable]
	public class PlayerInventoryUI
	{
		public Text 				food;
		public Text 				linemate;
		public Text 				deraumere;
		public Text 				sibur;
		public Text 				mendiane;
		public Text 				phiras;
		public Text 				thystame;
	}

	void Awake()
	{
		inventory.food = transform.FindChild ("PlayerInventory/Food").GetComponent<Text>();
		inventory.food.color = ResourceController.foodColor;
		inventory.linemate = transform.FindChild ("PlayerInventory/Linemate").GetComponent<Text>();
		inventory.linemate.color = ResourceController.linemateColor;
		inventory.deraumere = transform.FindChild ("PlayerInventory/Deraumere").GetComponent<Text>();
		inventory.deraumere.color = ResourceController.deraumereColor;
		inventory.sibur = transform.FindChild ("PlayerInventory/Sibur").GetComponent<Text>();
		inventory.sibur.color = ResourceController.siburColor;
		inventory.mendiane = transform.FindChild ("PlayerInventory/Mendiane").GetComponent<Text>();
		inventory.mendiane.color = ResourceController.mendianeColor;
		inventory.phiras = transform.FindChild ("PlayerInventory/Phiras").GetComponent<Text>();
		inventory.phiras.color = ResourceController.phirasColor;
		inventory.thystame = transform.FindChild ("PlayerInventory/Thystame").GetComponent<Text>();
		inventory.thystame.color = ResourceController.thystameColor;
	}
}