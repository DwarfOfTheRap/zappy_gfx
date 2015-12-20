using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquareScript : MonoBehaviour, ISquare
{
	public Dictionary<SquareContent, int>	resources;

	void Start()
	{
		resources = new Dictionary<SquareContent, int> ()
		{
			{SquareContent.NOURRITURE, 0},
			{SquareContent.LINEMATE, 0},
			{SquareContent.DERAUMERE, 0},
			{SquareContent.SIBUR, 0},
			{SquareContent.MENDIANE, 0},
			{SquareContent.PHIRAS, 0},
			{SquareContent.THYSTAME, 0},
			{SquareContent.PLAYERS, 0}
		};
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

	public Dictionary<SquareContent, int> GetResources ()
	{
		return resources;
	}
}

public enum SquareContent
{
	NOURRITURE,
	LINEMATE,
	DERAUMERE,
	SIBUR,
	MENDIANE,
	PHIRAS,
	THYSTAME,
	PLAYERS
}

public interface ISquare
{
	Vector3 GetPosition();
	float GetBoundX();
	float GetBoundY();
	float GetBoundZ();
	void Destroy();
	void DestroyImmediate();
	Dictionary<SquareContent, int> GetResources();
}