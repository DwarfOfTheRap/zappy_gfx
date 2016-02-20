﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class SquareScript : MonoBehaviour, ISquare
{
    public	SquareContent	resources;
	private Color			baseColor;
	public	Color			highlightedColor;
	public	GameObject		resourcePrefab;
	public	GameObject		foodPrefab;
	private float			resourceElevation = 0.63f;

	void Start ()
	{
		GetComponent<Renderer>().material.EnableKeyword ("_EMISSION");
		baseColor = GetComponent<Renderer>().material.GetColor ("_EmissionColor");
		GameManagerScript.instance.inputManager.OnLeftClicking += SquareHighlighting;
		InitResources();
	}

	void InitResources()
	{
		resources.linemate = InitResource (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), Color.white, resourcePrefab);
		resources.deraumere = InitResource (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), new Color(119/255.0f, 13/255.0f, 80/255.0f), resourcePrefab);
		resources.sibur = InitResource (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), new Color(242/255.0f, 29/255.0f, 68/255.0f), resourcePrefab);
		resources.mendiane = InitResource (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), new Color(255/255.0f, 137/255.0f, 48/255.0f), resourcePrefab);
		resources.phiras = InitResource (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), new Color(255/255.0f, 212/255.0f, 53/255.0f), resourcePrefab);
		resources.thystame = InitResource (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), Color.gray, resourcePrefab);
		resources.nourriture = InitResource (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), Color.cyan, foodPrefab);
	}

ResourceController InitResource(float x, float z, Color color, GameObject prefab)
	{
		GameObject clone = Instantiate (prefab);
		clone.transform.SetParent (this.transform);
		clone.transform.localPosition = new Vector3(x, resourceElevation, z);
		clone.GetComponentInChildren<Renderer>().material.color = new Color(color.r, color.g, color.b, clone.GetComponentInChildren<Renderer>().material.color.a);
		clone.GetComponentInChildren<ResourceScript>().Init();
		return clone.GetComponentInChildren<ResourceScript>().controller;
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
		GetComponent<MeshRenderer>().material.SetColor ("_EmissionColor", baseColor);
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

	void SquareHighlighting (ClickEventArgs args)
	{
		if (args.square == (ISquare)this)
			Highlighted();
		else
			Standard();
	}
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
    float GetBoundX();
    float GetBoundY();
    float GetBoundZ();
    void EnableVision(Color color);
    void DisableVision();
    void Destroy();
    void DestroyImmediate();
    SquareContent GetResources();
	void SetResources(uint nourriture, uint linemate, uint deraumere, uint sibur, uint mendiane, uint phiras, uint thystame);
    void Highlighted();
    void Standard();
}
