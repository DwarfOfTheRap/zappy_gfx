using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquareScript : MonoBehaviour, ISquare
{
	private Material standardMaterial;
	//private float standardEmission;
	//public float highlightedEmission;

	public Material highlightedMaterial;
	public SquareContent resources;

	void Start ()
	{
		//standardEmission = GetComponent<MeshRenderer> ().material.GetFloat("_EmissionColor");
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

	public void DestroyImmediate()
	{
		DestroyImmediate (gameObject);
	}

	public SquareContent GetResources ()
	{
		return resources;
	}

	public void Highlighted()
	{
		//gameObject.GetComponent<MeshRenderer> ().material.SetFloat("_EmissionColor", highlightedEmission);
		gameObject.GetComponent<MeshRenderer> ().material = highlightedMaterial;
	}

	public void Standard()
	{
		//gameObject.GetComponent<MeshRenderer> ().material.SetFloat("_EmissionColor", standardEmission);
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
	void Destroy();
	void DestroyImmediate();
	SquareContent GetResources();
	void Highlighted();
	void Standard();
}
