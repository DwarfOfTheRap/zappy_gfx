using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class TeamsInfo : MonoBehaviour {

	public List<Team> 		teams;
	public GameObject		prefab;

	// Use this for initialization
	void Start () {
		if (teams != null) {
			foreach (Team team in teams) {
				GameObject buttonObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
				buttonObject.transform.SetParent(this.gameObject.transform);
				Text[] texts = buttonObject.gameObject.GetComponentsInChildren<Text> ();
				texts[0].text = team.name;
				texts[0].color = team.color;
				texts[1].gameObject.GetComponent<TeamCompletion> ().team = team;
				texts[1].gameObject.GetComponent<TeamCompletion> ().text = texts[1];
				buttonObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
				buttonObject.name = team.name;
			}
		}
	}
}
