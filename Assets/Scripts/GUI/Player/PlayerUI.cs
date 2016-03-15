using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUI : MonoBehaviour {

	public PlayerController		player;
	public Text 				playerIndex;
	public Text 				playerLvl;
	public PlayerInventoryUI	inventory { get; private set;}
    public Scrollbar            scrollbar;

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
		inventory = new PlayerInventoryUI();
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
        scrollbar = GameObject.Find("TeamScrollbar").GetComponent<Scrollbar>();
	}

	public void TargetPlayer()
	{
		GameManagerScript.instance.inputManager.OnLeftClick(player.playerMotorController as IClickTarget);
		Camera.main.GetComponent<CameraScript>().cameraController.target = player.playerMotorController as IClickTarget;
	}

	void Update()
	{
		if (player != null)
		{
			inventory.food.text = player.inventory.nourriture.ToString();
			inventory.linemate.text = player.inventory.linemate.ToString();
			inventory.deraumere.text = player.inventory.deraumere.ToString();
			inventory.sibur.text = player.inventory.sibur.ToString();
			inventory.mendiane.text = player.inventory.mendiane.ToString();
			inventory.phiras.text = player.inventory.phiras.ToString();
			inventory.thystame.text = player.inventory.thystame.ToString();
			playerLvl.text = "Lvl " + player.level.ToString();
		}
        if (player.dead)
        {
            Destroy(gameObject);
            scrollbar.value = 1.0f;
        }
	}
}