using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour, ICameraMovement {

	public CameraController		cameraController;

	#region ICameraMovement implementation


	public Vector3 Move (Vector3 position)
	{
		this.transform.position = position;
		return (transform.position);
	}


	public Quaternion Rotate (Quaternion rotation)
	{
		this.transform.rotation = rotation;
		return (this.transform.rotation);
	}


	public Vector3 LerpMove (Vector3 destination, float speed)
	{
		this.transform.position = Vector3.Lerp (transform.position, destination, speed * Time.deltaTime);
		return (transform.position);
	}


	#endregion

	/*
	public Transform			squareContentWindow;
	public Transform			teamCompositionWindow;
	public Transform			teamListWindow;

	void CheckLeftMouseClick()
	{
		if (Input.GetMouseButtonUp (0)) {
			SquareContentUI sCSript = squareContentWindow.GetComponent<SquareContentUI>();
			ISquare square = null;
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			bool mousingOver = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();

			if (sCSript.square != null)
				sCSript.square.Standard();
			if (Physics.Raycast(ray, out hit))
			{
				if ((hit.collider.gameObject.tag == "Floor1" || hit.collider.gameObject.tag == "Floor2") && mousingOver == false)
				{
					squareContentWindow.GetComponent<CanvasGroup> ().alpha = 1;
					squareContentWindow.GetComponent<CanvasGroup> ().blocksRaycasts = true;
					square = hit.collider.gameObject.GetComponent<SquareScript>() as ISquare;
					square.Highlighted();
					sCSript.square = square;
				}
			}
			else if (mousingOver == false)
			{
				squareContentWindow.GetComponent<CanvasGroup> ().alpha = 0;
				squareContentWindow.GetComponent<CanvasGroup> ().blocksRaycasts = false;
				sCSript.square = null;
			}
			if (Time.time - lastClickTime < catchTime && sCSript.square != null) {
				target = sCSript.square;
			}
			lastClickTime = Time.time;
		}
	}

	void CheckRightMouseClick()
	{
		if (Input.GetMouseButtonUp (1)) {
			SquareContentUI sCScript = squareContentWindow.GetComponent<SquareContentUI>();
			PlayerListUI 	pLScript = teamCompositionWindow.GetComponentInChildren<PlayerListUI>();
			Transform[]		pLChildren = pLScript.gameObject.GetComponentsInChildren<Transform>();

			if (sCScript.square != null)
				sCScript.square.Standard();
			sCScript.square = null;
			squareContentWindow.GetComponent<CanvasGroup> ().alpha = 0;
			squareContentWindow.GetComponent<CanvasGroup> ().blocksRaycasts = false;
	
			foreach(Transform trans in pLChildren)
			{
				if (trans != pLScript.transform)
					Destroy(trans.gameObject);
			}
			teamCompositionWindow.GetComponent<CanvasGroup> ().alpha = 0;
			teamCompositionWindow.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		}
	}
*/

	void Start () {
		cameraController = new CameraController(GameManagerScript.instance.inputManager, this, GameManagerScript.instance.grid.controller.startHeight, GameManagerScript.instance.grid.controller.startWidth);
	}

	void LateUpdate () {
		cameraController.LateUpdate();
	}
}
