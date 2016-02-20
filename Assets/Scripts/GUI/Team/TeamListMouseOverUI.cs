using UnityEngine;
using System.Collections;

public class TeamListMouseOverUI : MonoBehaviour {

	public bool			notMousingOver = true;

	void OnMouseEnter() {
		notMousingOver = false;
	}

	void OnMouseExit() {
		notMousingOver = true;
	}
}
