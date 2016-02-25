using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PlayerScript : MonoBehaviour, IAnimatorController, IPlayerMotorController, IClickTarget {
	public PlayerController 	controller;
	public Orientation			orientation;

	private const float			_highlight_width = 0.0025f;
	
	private void Awake()
	{

		controller.SetAnimatorController(this);
		controller.SetPlayerMovementController(this);
		controller.SetGridController(GameManagerScript.instance.grid.controller);
		controller.SetPlayerOrientation(orientation);
		controller.SetInputManager(GameManagerScript.instance.inputManager);
	}

	private void Start()
	{
		Slider slider = GameObject.Find ("Slider").GetComponent<Slider> ();

		slider.onValueChanged.AddListener (controller.ChangeAnimationSpeed);
	}

	#region IAnimatorController implementation

	public void SetBool (string name, bool value)
	{
		GetComponent<Animator>().SetBool (name, value);
	}

	public void SetFloat (string name, float value)
	{
		GetComponent<Animator>().SetFloat (name, value);
	}

	public void SetInteger (string name, int value)
	{
		GetComponent<Animator>().SetInteger (name, value);
	}

	public void SetTrigger (string name)
	{
		GetComponent<Animator>().SetTrigger (name);
	}

	#endregion

	#region IPlayerMotorController implementation

	public bool IsMoving (Vector3 destination)
	{
		return this.transform.position != destination;
	}

	public Vector3 SetDestination (Vector3 destination)
	{
		return new Vector3(destination.x, transform.position.y, destination.z);
	}

	public bool HasHitDestination(Vector3 destination)
	{
		return (this.transform.position == new Vector3(destination.x, destination.y, destination.z));
	}

	public void SetPosition (Vector3 vector3)
	{
		this.transform.position = new Vector3(vector3.x, this.transform.position.y, vector3.z);
	}

	public void SetRotation (Quaternion rotation)
	{
		this.transform.rotation = rotation;
	}

	public void MoveToDestination (Vector3 destination, float speed)
	{
		this.transform.position = Vector3.MoveTowards (this.transform.position, destination, Time.deltaTime * speed * GameManagerScript.instance.timeSpeed);
	}

	public void MoveToRotation (Quaternion rotation, float rotSpeed)
	{
		this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotation, Time.deltaTime * rotSpeed * GameManagerScript.instance.timeSpeed);
	}

	public bool HasHitRotation(Quaternion rotation)
	{
		return (this.transform.rotation == rotation);
	}

	public void StopExpulsion()
	{
	} 

	public void Expulsed(Orientation orientation)
	{
	}

	public void Broadcast (string message)
	{
		Debug.Log (message);
		var signal = this.transform.FindChild ("PlayerCanvas/Signal").gameObject;
		StartCoroutine (BroadcastSignal(signal));
	}

	IEnumerator BroadcastSignal (GameObject signal)
	{
		int countdown = 7;
		signal.GetComponent<Image>().enabled = true;
		while (countdown > 0)
		{
			int t = (int)GameManagerScript.instance.timeSpeed;
			if (t > 0)
			{
				yield return new WaitForSeconds(1.0f / t);
				countdown--;
			}
			else
				yield return new WaitForEndOfFrame();
		}
		signal.GetComponent<Image>().enabled = false;
	}

	public void EnableHighlight (Color color)
	{
		foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
		{
			renderer.materials[0].SetFloat ("_Outline", _highlight_width);
			renderer.materials[0].SetColor ("_OutlineColor", color);
		}
	}

	public void DisableHighlight ()
	{
		foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
		{
			renderer.materials[0].SetFloat ("_Outline", 0);
			renderer.materials[0].SetColor ("_OutlineColor", Color.black);
		}
	}

	public void SetTeamColor (Color color)
	{
		foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
		{
			renderer.materials[1].color = color;
		}
		GetComponentInChildren<Light>().color = color;
	}

	#endregion

	#region IClickTarget implementation

	public bool IsSquare ()
	{
		return false;
	}

	public bool IsPlayer ()
	{
		return true;
	}

	public Vector3 GetPosition ()
	{
		return this.transform.position;
	}

	#endregion

	void Update()
	{
		controller.Update (this.transform.position);
	}
}
