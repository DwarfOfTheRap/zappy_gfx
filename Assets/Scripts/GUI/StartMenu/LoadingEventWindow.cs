using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class LoadingEventWindow : MonoBehaviour {

	public bool hideOnLoad;
	// Use this for initialization
	void Start () {
		GameManagerScript.instance.OnLevelLoad += HandleOnLevelLoad;
	}

	void OnDisable (){
		GameManagerScript.instance.OnLevelLoad -= HandleOnLevelLoad;
	}

	void HandleOnLevelLoad ()
	{
		if (hideOnLoad)
		{
			GetComponent<CanvasGroup>().alpha = 0;
			GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
		else
		{
			GetComponent<CanvasGroup>().alpha = 1;
			GetComponent<CanvasGroup>().blocksRaycasts = true;
		}
	}
}
