using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class TeamsInfo : MonoBehaviour {

	public List<Team> 		teams;
	public Font				font;

	void CreateTeamDetailsButton (GameObject buttonObject, Team team)
	{
		buttonObject.AddComponent<RectTransform> ();
		buttonObject.AddComponent<CanvasRenderer> ();
		buttonObject.AddComponent<Image> ();

		Button button = buttonObject.AddComponent<Button> ();

		LayoutElement layoutElem = buttonObject.AddComponent<LayoutElement> ();
		layoutElem.preferredHeight = 15;
		layoutElem.flexibleWidth = 1;

		HorizontalLayoutGroup hLayout = buttonObject.AddComponent<HorizontalLayoutGroup> ();
		hLayout.padding.right = 32;
	}

	void CreateTeamNameTextArea (GameObject buttonObject, Team team)
	{
		GameObject teamName = new GameObject (team.teamName);
		teamName.AddComponent<RectTransform> ();
		teamName.AddComponent<CanvasRenderer> ();

		Text text = teamName.AddComponent<Text> ();
		text.text = team.teamName;
		text.font = font;
		text.fontSize = 8;
		text.alignment = TextAnchor.MiddleLeft;
		text.resizeTextForBestFit = true;
		text.resizeTextMinSize = 6;
		text.resizeTextMaxSize = 8;
		text.color = team.teamColor;
	}

	void CreateTeamCompletionTextArea (GameObject buttonObject, Team team)
	{
		GameObject teamCompletion = new GameObject (team.teamName);
		LayoutElement layoutElem = teamCompletion.AddComponent<LayoutElement> ();
		layoutElem.ignoreLayout = true;

		RectTransform trans = teamCompletion.AddComponent<RectTransform> ();
		trans.anchorMin = new Vector2 (0.725f, 0.0f);
		trans.anchorMax = new Vector2 (1.0f, 1.0f);
		trans.pivot = new Vector2 (0.5f, 0.5f);

		teamCompletion.AddComponent<CanvasRenderer> ();
		
		Text text = teamCompletion.AddComponent<Text> ();
		text.font = font;
		text.fontSize = 8;
		text.alignment = TextAnchor.MiddleRight;
		text.resizeTextForBestFit = true;
		text.resizeTextMinSize = 6;
		text.resizeTextMaxSize = 8;
		text.color = new Color(255.0f, 255.0f, 255.0f, 255.0f);

		TeamCompletion script = teamCompletion.AddComponent<TeamCompletion> ();
		script.team = team;
		script.text = text;
	}
	
	// Use this for initialization
	void Start () {
		foreach(Team team in teams) {
			GameObject buttonObject = new GameObject(team.teamName);
			CreateTeamDetailsButton(buttonObject, team);
			CreateTeamNameTextArea(buttonObject, team);
			CreateTeamCompletionTextArea(buttonObject, team);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
