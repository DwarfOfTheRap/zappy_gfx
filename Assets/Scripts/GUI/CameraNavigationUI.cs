using UnityEngine;
using System.Collections;

public class CameraNavigationUI : MonoBehaviour {

	public Transform			squareContentWindow;
	public Transform			teamCompositionWindow;
	public Transform			teamListWindow;

	private float 				lastClickTime = 0.0f;
	private float 				catchTime = 0.25f;
	private float				moveSpeed = 0.5f;
	private float				scrollSpeed = 1.0f;
	private int					startHeight;
	private int					startWidth;

	void InitCameraPosition()
	{
		Camera.main.transform.position = new Vector3 (0.0f + 2.5f * (startWidth - 1), 25.0f + (1.875f * (startHeight - 10)), -33.0f - (1.675f * (startHeight - 10)));
		Camera.main.transform.rotation = Quaternion.Euler(new Vector3(28.0f, 0.0f, 0.0f));
	}

	void CheckLeftMouseClick()
	{
		if (Input.GetMouseButtonUp (0)) {
			Camera cam = Camera.main;
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
				cam.transform.position = new Vector3 (sCSript.square.GetPosition().x, sCSript.square.GetPosition().y + 16.5f, sCSript.square.GetPosition().z - 28.0f);
			}
			lastClickTime = Time.time;
		}
	}

	void CheckMouseScroll()
	{
		Camera cam = Camera.main;
		float deltaScroll = Input.GetAxis("Mouse ScrollWheel");
		bool mousingOver = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();

		if (deltaScroll > 0.0f && cam.transform.position.y > 3.5f && mousingOver == false)
		{
			cam.transform.position = new Vector3 (cam.transform.position.x, cam.transform.position.y - scrollSpeed, cam.transform.position.z);
			cam.transform.position = new Vector3 (cam.transform.position.x, cam.transform.position.y, cam.transform.position.z + scrollSpeed);
		}
		
		if (deltaScroll < 0.0f && cam.transform.position.y < 100.0f && mousingOver == false)
		{
			cam.transform.position = new Vector3 (cam.transform.position.x, cam.transform.position.y + scrollSpeed, cam.transform.position.z);
			cam.transform.position = new Vector3 (cam.transform.position.x, cam.transform.position.y, cam.transform.position.z - scrollSpeed);
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

	void CheckKeyboardInput()
	{
		Camera cam = Camera.main;
		if (Input.GetAxis("Vertical") > 0 /*&& cam.transform.position.z < 0.0f*/)
		{
			cam.transform.position = new Vector3 (cam.transform.position.x, cam.transform.position.y, cam.transform.position.z + (Input.GetAxis("Vertical") * moveSpeed));
		}
		if (Input.GetAxis("Horizontal") < 0 && cam.transform.position.x > -2.5f)
		{
			cam.transform.position = new Vector3 (cam.transform.position.x - moveSpeed, cam.transform.position.y, cam.transform.position.z);
		}
		if (Input.GetAxis("Vertical") < 0 /*&& cam.transform.position.z > (startHeight * 5.0f * -1.0f)*/)
		{
			cam.transform.position = new Vector3 (cam.transform.position.x, cam.transform.position.y, cam.transform.position.z + (Input.GetAxis("Vertical") * moveSpeed));
		}
		if (Input.GetAxis("Horizontal") > 0 && cam.transform.position.x < ((startWidth * 5.0f) - 2.5f))
		{
			cam.transform.position = new Vector3 (cam.transform.position.x + moveSpeed, cam.transform.position.y, cam.transform.position.z);
		}
		if (Input.GetKey(KeyCode.Q) && cam.transform.position.y < 100.0f)
		{
			cam.transform.position = new Vector3 (cam.transform.position.x, cam.transform.position.y + moveSpeed, cam.transform.position.z);
		}
		if (Input.GetKey(KeyCode.E) && cam.transform.position.y > 3.5f)
		{
			cam.transform.position = new Vector3 (cam.transform.position.x, cam.transform.position.y - moveSpeed, cam.transform.position.z);
		}
		if (Input.GetKeyUp(KeyCode.Space))
		{
			InitCameraPosition();
		}
		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			moveSpeed = 1.0f;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			moveSpeed = 0.5f;
		}
	}

	void Start () {
		startHeight = GameManagerScript.instance.grid.controller.startHeight;
		startWidth = GameManagerScript.instance.grid.controller.startWidth;
		InitCameraPosition();
	}

	void Update () {
		CheckLeftMouseClick();
		CheckMouseScroll();
		CheckRightMouseClick();
		CheckKeyboardInput();
	}
}
