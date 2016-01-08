using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class SquareScript : MonoBehaviour, ISquare
{
    public SquareContent	resources;
	public Color			originalColor;

	void Start()
	{
		originalColor = GetComponent<Renderer>().material.color;
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

	public void EnableVision (Color color)
	{
		GetComponent<Renderer>().material.color = color;
	}

	public void DisableVision ()
	{
		GetComponent<Renderer>().material.color = originalColor;
	}

    public SquareContent GetResources ()
    {
        return resources;
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
}
