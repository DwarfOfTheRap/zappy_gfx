using UnityEngine;
using System.Collections;

public class CameraNavigationUI : MonoBehaviour {

	public Transform			squareContentWindow;
	public Transform			teamCompositionWindow;

	
	void CheckMouseInput()
	{
		if (Input.GetMouseButtonUp (0)) {
			SquareContentUI script = squareContentWindow.GetComponent<SquareContentUI>();
			if (script.square != null)
				script.square.Standard();
			script.square = script.OnLeftMouseClick (Input.mousePosition);
			
		}
		if (Input.GetMouseButtonUp (1)) {
			SquareContentUI script = squareContentWindow.GetComponent<SquareContentUI>();
			if (script.square != null)
				script.square.Standard();
			script.square = null;
			squareContentWindow.GetComponent<CanvasGroup> ().alpha = 0;
		}
		if (Input.GetMouseButtonUp (1)) {
				Transform[] children = gameObject.GetComponentsInChildren<Transform>();
				
				foreach(Transform trans in children)
				{
					if (trans != this.transform)
						Destroy(trans.gameObject);
				}
				window.alpha = 0;
		}
	}


	void Start () {
	
	}

	void Update () {
	
	}
}
