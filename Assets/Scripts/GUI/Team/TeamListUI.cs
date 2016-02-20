using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class TeamListUI : MonoBehaviour {
	public GameObject		prefab;
	
	void OnEnable()
	{
		TeamManager.OnTeamAdded += OnTeamAdded;
	}
	
	void OnDisable()
	{
		TeamManager.OnTeamAdded -= OnTeamAdded;
	}
	
	void OnTeamAdded(Team team)
	{
		GameObject buttonObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
		
		buttonObject.transform.SetParent(this.gameObject.transform);
		buttonObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		buttonObject.name = team.name;
		
		TeamButton script = buttonObject.GetComponent<TeamButton> ();
		
		script.team = team;
		script.teamName.text = " " + team.name;
		script.teamName.color = team.color;
		script.teamCompletion.gameObject.GetComponent<TeamCompletion> ().team = team;
		script.teamCompletion.gameObject.GetComponent<TeamCompletion> ().text = script.teamCompletion;
	}
}