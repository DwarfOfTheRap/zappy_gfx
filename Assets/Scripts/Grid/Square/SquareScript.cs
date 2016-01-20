using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class SquareScript : MonoBehaviour, ISquare
{
    public SquareContent	resources;
	public Color			originalColor;
	private Material standardMaterial;
	public Material highlightedMaterial;

	void Start ()
	{
        originalColor = GetComponent<Renderer>().material.color;
		standardMaterial = GetComponent<MeshRenderer> ().material;
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
		GetComponent<MeshRenderer>().material.color = originalColor;
	}

	public void DestroyImmediate()
	{
		DestroyImmediate (gameObject);
	}

	public void Highlighted()
	{
		gameObject.GetComponent<MeshRenderer> ().material = highlightedMaterial;
	}

	public void Standard()
	{
		gameObject.GetComponent<MeshRenderer> ().material = standardMaterial;
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
