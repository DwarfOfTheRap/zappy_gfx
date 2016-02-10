using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class SquareScript : MonoBehaviour, ISquare
{
    public SquareContent	resources;
	private Color			baseColor;
	public	Color			highlightedColor;
	public	GameObject		resourcePrefab;

	private float			resourceElevation = 0.63f;

	void Start ()
	{
		GetComponent<Renderer>().material.EnableKeyword ("_EMISSION");
		baseColor = GetComponent<Renderer>().material.GetColor ("_EmissionColor");
		InitResources();
	}

	void InitResources()
	{
		InitResource (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), new Color(76/255.0f, 12/255.0f, 43/255.0f));
		InitResource (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), new Color(119/255.0f, 13/255.0f, 80/255.0f));
		InitResource (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), new Color(242/255.0f, 29/255.0f, 68/255.0f));
		InitResource (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), new Color(255/255.0f, 137/255.0f, 48/255.0f));
		InitResource (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), new Color(255/255.0f, 212/255.0f, 53/255.0f));
		InitResource (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), Color.magenta);
	}

	GameObject InitResource(float x, float z, Color color)
	{
		GameObject clone = Instantiate (resourcePrefab);
		clone.transform.SetParent (this.transform);
		clone.transform.localPosition = new Vector3(x, resourceElevation, z);
		clone.GetComponentInChildren<Renderer>().material.color = new Color(color.r, color.g, color.b, clone.GetComponentInChildren<Renderer>().material.color.a);
		return clone;
	}

	public Vector3 GetPosition ()
	{
		return transform.position;
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

	public void EnableVision (Color color)
	{
		GetComponent<MeshRenderer>().material.color = color;
	}

	public void DisableVision ()
	{
		GetComponent<MeshRenderer>().material.color = baseColor;
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
		GetComponent<Renderer>().material.SetColor ("_EmissionColor", baseColor);
	}
}

[System.Serializable]
public class SquareContent
{
	public uint nourriture;
	public uint linemate;
	public uint deraumere;
	public uint sibur;
	public uint mendiane;
	public uint phiras;
	public uint thystame;
	public List<PlayerController> players = new List<PlayerController>();
}

public interface ISquare
{
    Vector3 GetPosition();
    float GetBoundX();
    float GetBoundY();
    float GetBoundZ();
    void EnableVision(Color color);
    void DisableVision();
    void Destroy();
    void DestroyImmediate();
    SquareContent GetResources();
    void Highlighted();
    void Standard();
}
