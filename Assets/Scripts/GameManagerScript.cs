using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {
	public static GameManagerScript instance;
	public GridScript				grid { get; private set; }

	void Awake()
	{
		instance = this;
		grid = GetComponentInChildren<GridScript>();
	}
}
