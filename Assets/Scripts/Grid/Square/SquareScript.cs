using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class SquareScript : MonoBehaviour, ISquare, IClickTarget
{
	// Prefabs
	public	GameObject		resourcePrefab;
	public	GameObject		foodPrefab;

	// Colors
	private Color			_baseColor;
	public	Color			highlightedColor;

	// Resources
	public	SquareContent	resources;
	private const float		_resourceElevation = 0.63f;

	void Start ()
	{
		GetComponent<Renderer>().material.EnableKeyword ("_EMISSION");
		_baseColor = GetComponent<Renderer>().material.GetColor ("_EmissionColor");
		GameManagerScript.instance.inputManager.OnLeftClicking += SquareHighlighting;
		GameManagerScript.instance.inputManager.OnRightClicking += Standard;
		InitResources();
	}

	void OnDisable()
	{
		GameManagerScript.instance.inputManager.OnLeftClicking -= SquareHighlighting;
		GameManagerScript.instance.inputManager.OnRightClicking -= Standard;
	}

	void InitResources()
	{
		resources.linemate = InitResource (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), ResourceController.linemateColor, resourcePrefab);
		resources.deraumere = InitResource (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), ResourceController.deraumereColor, resourcePrefab);
		resources.sibur = InitResource (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), ResourceController.siburColor, resourcePrefab);
		resources.mendiane = InitResource (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), ResourceController.mendianeColor, resourcePrefab);
		resources.phiras = InitResource (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), ResourceController.phirasColor, resourcePrefab);
		resources.thystame = InitResource (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), ResourceController.thystameColor, resourcePrefab);
		resources.nourriture = InitResource (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), ResourceController.foodColor, foodPrefab);
	}

	ResourceController InitResource(float x, float z, Color color, GameObject prefab)
	{
		GameObject clone = Instantiate (prefab);
		clone.transform.SetParent (this.transform);
		clone.transform.localPosition = new Vector3(x, _resourceElevation, z);
		clone.GetComponentInChildren<Renderer>().material.EnableKeyword ("_EMISSION");
		clone.GetComponentInChildren<Renderer>().material.color = new Color(color.r, color.g, color.b, clone.GetComponentInChildren<Renderer>().material.color.a);
		clone.GetComponentInChildren<Renderer>().material.SetColor ("_EmissionColor", new Color(color.r / 4.0f, color.g / 4.0f, color.b / 4.0f, 1));
		clone.GetComponentInChildren<ResourceScript>().Init();
		return clone.GetComponentInChildren<ResourceScript>().controller;
	}

	public Vector3 GetPosition ()
	{
		return transform.position;
	}

	public Vector3 GetSubPosition(int index)
	{
		switch (index)
		{
		case 0 : 
			return new Vector3(transform.position.x - GetBoundX() / 2, transform.position.y, transform.position.z - GetBoundZ() / 2);
		case 1 :
			return new Vector3(transform.position.x, transform.position.y, transform.position.z - GetBoundZ() / 2);
		case 2 :
			return new Vector3(transform.position.x + GetBoundX() / 2, transform.position.y, transform.position.z - GetBoundZ() / 2);
		case 3 :
			return new Vector3(transform.position.x - GetBoundX() / 2, transform.position.y, transform.position.z);
		case 4:
			return transform.position;
		case 5:
			return new Vector3(transform.position.x + GetBoundX() / 2, transform.position.y, transform.position.z);
		case 6:
			return new Vector3(transform.position.x - GetBoundX() / 2, transform.position.y, transform.position.z + GetBoundZ () / 2);
		case 7:
			return new Vector3(transform.position.x, transform.position.y, transform.position.z + GetBoundZ () / 2);
		case 8:
			return new Vector3(transform.position.x + GetBoundX() / 2, transform.position.y, transform.position.z + GetBoundZ () / 2);
		default:
			return transform.position;
		}
	}
	
	public float GetBoundX ()
	{
		return GetComponent<Renderer>().bounds.size.x;
	}
	
	public float GetBoundY ()
	{
		return GetComponent<Renderer>().bounds.size.y;
	}
	
	public float GetBoundZ ()
	{
		return GetComponent<Renderer>().bounds.size.z;
	}

	public void Destroy ()
	{
		Destroy (gameObject);
	}

	public SquareContent GetResources()
	{
		return this.resources;
	}

	public void SetResources (uint nourriture, uint linemate, uint deraumere, uint sibur, uint mendiane, uint phiras, uint thystame)
	{
		resources.nourriture.count = nourriture;
		resources.linemate.count = linemate;
		resources.deraumere.count = deraumere;
		resources.sibur.count = sibur;
		resources.mendiane.count = mendiane;
		resources.phiras.count = phiras;
		resources.thystame.count = thystame;
	}

	public void EnableVision (Color color)
	{
		GetComponent<MeshRenderer>().material.SetColor ("_EmissionColor", color);
	}

	public void DisableVision ()
	{
		GetComponent<MeshRenderer>().material.SetColor ("_EmissionColor", _baseColor);
	}

	public void DestroyImmediate()
	{
		DestroyImmediate (gameObject);
	}

	public void Highlighted(Color color)
	{
		GetComponent<Renderer>().material.SetColor ("_EmissionColor", color);
	}

	public void Highlighted()
	{
		Highlighted (highlightedColor);
	}

	public void Standard()
	{
		GetComponent<Renderer>().material.SetColor ("_EmissionColor", _baseColor);
	}

	void SquareHighlighting (ClickEventArgs args)
	{
		if (args.target == (IClickTarget)this)
			Highlighted();
		else
			Standard();
	}

	#region IClickTarget implementation

	public bool IsSquare ()
	{
		return true;
	}

	public bool IsPlayer ()
	{
		return false;
	}

	#endregion
}

[System.Serializable]
public class SquareContent
{
	public ResourceController nourriture;
	public ResourceController linemate;
	public ResourceController deraumere;
	public ResourceController sibur;
	public ResourceController mendiane;
	public ResourceController phiras;
	public ResourceController thystame;
	public List<PlayerController> players = new List<PlayerController>();
	public List<EggController> eggs = new List<EggController>();
}

public enum SquareResources
{
	LINEMATE,
	DEFAUMERE,
	SIBUR,
	MENDIANE,
	PHIRAS,
	THYSTAME,
	NOURRITURE
}

public interface ISquare
{
    Vector3 GetPosition();
	Vector3 GetSubPosition (int subPositionIndex);
    float GetBoundX();
    float GetBoundY();
    float GetBoundZ();
    void EnableVision(Color color);
    void DisableVision();
    void Destroy();
    void DestroyImmediate();
    SquareContent GetResources();
	void SetResources(uint nourriture, uint linemate, uint deraumere, uint sibur, uint mendiane, uint phiras, uint thystame);
	void Highlighted(Color color);
    void Highlighted();
    void Standard();
}
